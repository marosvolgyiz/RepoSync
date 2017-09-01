using System.Collections.Generic;

namespace RepoSync.ContentExtensions
{
    public enum FieldNameBehavior
    {
        Whitelist,
        Blacklist
    }

    public static class ContentComparerExtension
    {
        public static bool CompareTo(this SyncContent source,
            SyncContent target,
            List<string> fieldNames = null,
            FieldNameBehavior fieldNameBehavior = FieldNameBehavior.Blacklist)
        {
            if (fieldNames == null)
            {
                fieldNames = new List<string> { "Id", "ParentId" };
            }

            if (target == null)
            {
                return false;
            }

            var cloned = target.Content2JSON().JSON2Content();

            if (fieldNameBehavior == FieldNameBehavior.Blacklist)
            {
                foreach (var currentField in source.Fields)
                {
                    if (!fieldNames.Contains(currentField.Key) && currentField.Value != target.Fields[currentField.Key])
                        return false;
                }
                return true;
            }

            foreach (var currentFieldName in fieldNames)
            {
                if (source.Fields[currentFieldName] != target.Fields[currentFieldName])
                {
                    return false;
                }
                return true;
            }

            return source.Content2JSON() == cloned.Content2JSON();
        }
    }
}
