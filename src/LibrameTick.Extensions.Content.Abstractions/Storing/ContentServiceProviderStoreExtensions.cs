#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.Extensions.Data.Storing;

namespace Librame.Extensions.Content.Storing;

/// <summary>
/// <see cref="IServiceProvider"/>、<see cref="IStore{T}"/> 静态扩展。
/// </summary>
public static class ContentServiceProviderStoreExtensions
{

    /// <summary>
    /// 获取系统各版块单元列表的字典集合。
    /// </summary>
    /// <param name="services">给定的 <see cref="IServiceProvider"/>。</param>
    /// <param name="count">给定的单元列表条数。</param>
    /// <param name="userIds">输出用户标识集合。</param>
    /// <returns>返回 <see cref="Dictionary{Pane, Units}"/>。</returns>
    public static Dictionary<Pane, List<Unit>> GetPaneUnits(this IServiceProvider services,
        int count, out List<string> userIds)
    {
        var allUserIds = new List<string>();
        var dict = new Dictionary<Pane, List<Unit>>();

        var categoryStore = services.GetStore<Category>();
        var visitStore = services.GetStore<UnitVisitCount>();

        var paneStore = services.GetStore<Pane>();
        var paneUnitStore = services.GetStore<PaneUnit>();
        var unitStore = services.GetStore<Unit>();

        var paneIds = paneUnitStore
            .GetQueryable()
            .Select(s => s.PaneId)
            .Distinct()
            .ToList();

        foreach (var paneId in paneIds)
        {
            var pane = paneStore.FindById(paneId);
            if (pane is null)
                continue;

            var unitIds = paneUnitStore
                .GetQueryable()
                .Where(p => p.PaneId == paneId)
                .Select(p => p.UnitId)
                .Distinct()
                .Take(count)
                .ToList();

            var units = new List<Unit>();
            foreach (var unitId in unitIds)
            {
                var unit = unitStore.FindById(unitId);
                if (unit is null)
                    continue;

                if (unit.VisitCount is null)
                    unit.VisitCount = visitStore.FindById(unitId);

                if (unit.Category is null)
                    unit.Category = categoryStore.FindById(unit.CategoryId);

                if (!string.IsNullOrEmpty(unit.CreatedBy) && !allUserIds.Contains(unit.CreatedBy))
                    allUserIds.Add(unit.CreatedBy);

                units.Add(unit);
            }

            dict.Add(pane, units);
        }

        userIds = allUserIds;
        return dict;
    }

}
