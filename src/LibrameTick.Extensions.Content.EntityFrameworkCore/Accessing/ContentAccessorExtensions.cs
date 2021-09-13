#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.Extensions.Content.Storing;
using Librame.Extensions.Core;
using Librame.Extensions.Data;

namespace Librame.Extensions.Content.Accessing
{
    /// <summary>
    /// <see cref="IContentAccessor"/> 静态扩展。
    /// </summary>
    public static class ContentAccessorExtensions
    {

        /// <summary>
        /// 添加单元集合（同时连带更新关联集合）。
        /// </summary>
        /// <param name="accessor">给定的 <see cref="IContentAccessor"/>。</param>
        /// <param name="units">给定的单元集合。</param>
        /// <param name="idGeneratorFactory">给定的 <see cref="IIdentificationGeneratorFactory"/>。</param>
        /// <param name="clock">给定的 <see cref="IClock"/>。</param>
        /// <param name="createdBy">给定的创建者。</param>
        /// <param name="paneIds">给定的窗格标识集合。</param>
        /// <param name="claims">给定的声明元组集合。</param>
        /// <param name="tagNames">给定的标签名称集合。</param>
        public static void AddUnits(this IContentAccessor accessor, IEnumerable<Unit> units,
            IIdentificationGeneratorFactory idGeneratorFactory, IClock clock, string createdBy,
            IEnumerable<int>? paneIds = null, IEnumerable<(int ClaimId, string ClaimValue)>? claims = null,
            IEnumerable<string>? tagNames = null)
        {
            foreach (var unit in units)
            {
                if (paneIds != null)
                    accessor.AddPaneUnit(unit, paneIds, clock, createdBy);

                if (claims != null)
                    accessor.AddUnitClaims(unit, claims, idGeneratorFactory, clock, createdBy);

                if (tagNames != null)
                    accessor.AddUnitTags(unit, tagNames, idGeneratorFactory, clock, createdBy);

                accessor.Units.Add(unit);
                accessor.AddUnitVisitCount(unit);
            }
        }

        /// <summary>
        /// 添加窗格单元。
        /// </summary>
        /// <param name="accessor">给定的 <see cref="IContentAccessor"/>。</param>
        /// <param name="unit">给定的 <see cref="Unit"/>。</param>
        /// <param name="paneIds">给定的窗格标识集合。</param>
        /// <param name="clock">给定的 <see cref="IClock"/>。</param>
        /// <param name="createdBy">给定的创建者。</param>
        /// <param name="unitIdsMaxLength">给定的单元标识集合最大长度（可选）。</param>
        public static void AddPaneUnit(this IContentAccessor accessor, Unit unit, IEnumerable<int> paneIds,
            IClock clock, string? createdBy, int unitIdsMaxLength = 3900)
        {
            foreach (var paneId in paneIds)
            {
                var paneUnits = accessor.PaneUnits.Where(p => p.PaneId == paneId && p.UnitIds.Length < unitIdsMaxLength).ToList();
                if (paneUnits.Count > 0)
                {
                    // 附加
                    paneUnits[0].AppendUnitId(unit.Id);
                }
                else
                {
                    // 新增
                    var paneUnit = new PaneUnit
                    {
                        PaneId = paneId
                    };

                    paneUnit.AppendUnitId(unit.Id);
                    paneUnit.PopulateCreation(createdBy, clock.GetUtcNow());

                    accessor.PaneUnits.Add(paneUnit);
                }
            }
        }

        /// <summary>
        /// 添加单元声明集合。
        /// </summary>
        /// <param name="accessor">给定的 <see cref="IContentAccessor"/>。</param>
        /// <param name="unit">给定的 <see cref="Unit"/>。</param>
        /// <param name="claims">给定的声明元组集合。</param>
        /// <param name="idGeneratorFactory">给定的 <see cref="IIdentificationGeneratorFactory"/>。</param>
        /// <param name="clock">给定的 <see cref="IClock"/>。</param>
        /// <param name="createdBy">给定的创建者。</param>
        public static void AddUnitClaims(this IContentAccessor accessor, Unit unit,
            IEnumerable<(int ClaimId, string ClaimValue)> claims,
            IIdentificationGeneratorFactory idGeneratorFactory, IClock clock, string? createdBy)
        {
            foreach (var claim in claims)
            {
                var unitClaim = new UnitClaim
                {
                    Id = idGeneratorFactory.GetNewId<long>(),
                    UnitId = unit.Id,
                    ClaimId = claim.ClaimId,
                    ClaimValue = claim.ClaimValue
                };

                unitClaim.PopulateCreation(createdBy, clock.GetUtcNow());
                accessor.UnitClaims.Add(unitClaim);
            }
        }

        /// <summary>
        /// 添加单元标签集合（如果标签不存在会自动添加）。
        /// </summary>
        /// <param name="accessor">给定的 <see cref="IContentAccessor"/>。</param>
        /// <param name="unit">给定的 <see cref="Unit"/>。</param>
        /// <param name="tagNames">给定的标签名称集合。</param>
        /// <param name="idGeneratorFactory">给定的 <see cref="IIdentificationGeneratorFactory"/>。</param>
        /// <param name="clock">给定的 <see cref="IClock"/>。</param>
        /// <param name="createdBy">给定的创建者。</param>
        public static void AddUnitTags(this IContentAccessor accessor, Unit unit, IEnumerable<string> tagNames,
            IIdentificationGeneratorFactory idGeneratorFactory, IClock clock, string? createdBy)
        {
            foreach (var tagName in tagNames)
            {
                var tag = accessor.Tags.FirstOrDefault(p => p.Name == tagName);
                if (tag == null)
                {
                    tag = new Tag
                    {
                        Name = tagName
                    };

                    tag.PopulateCreation(createdBy, clock.GetUtcNow());
                    accessor.Tags.Add(tag);
                }

                var unitTag = new UnitTag
                {
                    Id = idGeneratorFactory.GetNewId<long>(),
                    UnitId = unit.Id,
                    TagId = tag.Id
                };

                unitTag.PopulateCreation(createdBy, clock.GetUtcNow());
                accessor.UnitTags.Add(unitTag);
            }
        }

        /// <summary>
        /// 添加单元访问计数。
        /// </summary>
        /// <param name="accessor">给定的 <see cref="IContentAccessor"/>。</param>
        /// <param name="unit">给定的 <see cref="Unit"/>。</param>
        public static void AddUnitVisitCount(this IContentAccessor accessor, Unit unit)
            => accessor.UnitVisitCounts.Add(new UnitVisitCount() { UnitId = unit.Id });

    }
}
