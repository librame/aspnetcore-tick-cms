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

namespace Librame.Extensions.Portal.Storing;

/// <summary>
/// <see cref="IServiceProvider"/>、<see cref="IStore{T}"/> 静态扩展。
/// </summary>
public static class PortalServiceProviderStoreExtensions
{

    /// <summary>
    /// 获取指定用户标识集合对应的编者字典集合。
    /// </summary>
    /// <param name="services">给定的 <see cref="IServiceProvider"/>。</param>
    /// <param name="userIds">给定的用户标识集合。</param>
    /// <returns>返回 <see cref="Dictionary{Pane, Units}"/>。</returns>
    public static Dictionary<string, Editor> GetEditorsByUserIds(this IServiceProvider services,
        IEnumerable<string> userIds)
    {
        var editorStore = services.GetStore<Editor>();

        var dict = new Dictionary<string, Editor>();

        foreach (var userId in userIds)
        {
            var editor = editorStore.GetQueryable().FirstOrDefault(p => p.UserId == userId);
            if (editor is null)
                continue;

            dict.Add(userId, editor);
        }

        return dict;
    }

}
