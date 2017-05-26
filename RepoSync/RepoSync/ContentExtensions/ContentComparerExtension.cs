using System;
using SenseNet.Client;

namespace RepoSync.ContentExtensions
{
    public static class ContentComparerExtension
    {
        public static bool EqualsWithoutIds(this Content source, Content target)
        {
            if (target == null)
            {
                return false;
            }

            var cloned = target.Content2JSON().JSON2Content();
            if (cloned.Fields["Id"] != null && (long)cloned.Fields["Id"] != source.Id)
            {
                cloned.Fields["Id"] = source.Id;
            }

            if (cloned.Fields["ParentId"] != null && (long)cloned.Fields["ParentId"] != source.ParentId)
            {
                cloned.Fields["ParentId"] = source.ParentId;
            }

            return source.Content2JSON() == cloned.Content2JSON();
        }
    }
}
