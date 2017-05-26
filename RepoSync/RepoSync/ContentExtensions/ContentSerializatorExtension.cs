using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SenseNet.Client;

namespace RepoSync.ContentExtensions
{
    public static class ContentSerializatorExtension
    {
        public static SyncContent Content2SyncContent(this Content c)
        {
            return c.Content2JSON().JSON2Content();
        }
        /// <summary>
        /// This method create a json string from SyncContent
        /// </summary>
        /// <param name="sc">SyncContent</param>
        /// <returns></returns>
        public static string Content2JSON(this SyncContent sc)
        {
            return JsonConvert.SerializeObject(sc);
        }
        /// <summary>
        /// This method create json string from SenseNet.Client.Content
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static string Content2JSON(this Content c)
        {
            
            var resultContent = new SyncContent();
            var fieldNames = c.GetFieldNames();
           
            foreach (var item in fieldNames)
            {
                //http://wiki.sensenet.com/Client_library#Reference_fields
                //TODO: resolve references!
                resultContent.Fields.Add(item, c[item].ToString());
                
            }

            //TODO: Set Permissions
            resultContent.Path = c.Path;
            resultContent.Name = c.Name;
            resultContent.Id = c.Id;
            return resultContent.Content2JSON();
        }
        /// <summary>
        /// This method create a SyncContent from json string.
        /// </summary>
        /// <param name="json">This is the json string</param>
        /// <returns></returns>
        public static SyncContent JSON2Content(this string json)
        {
            SyncContent result = new SyncContent();
            result = JsonConvert.DeserializeObject<SyncContent>(json);
            return result;
        }
        /// <summary>
        /// This method fill the SenseNet.Client.Content field with SyncContent fields
        /// </summary>
        /// <param name="c">SenseNet.Client.Content</param>
        /// <param name="sc">SyncContent</param>
        public static void FillContentWithSyncContent(this Content c, SyncContent sc)
        {
            foreach (var field in sc.Fields)
            {
                c[field.Key] = field.Value;
            }

            //TODO: Implement permission Filling
        }
        /// <summary>
        /// This method return all fields of a SenseNet.Client.Content. 
        /// </summary>
        /// <param name="c">SenseNet.Client.Content</param>
        /// <returns>String array which contains all fieldname</returns>
        public static string[] GetFieldNames(this SenseNet.Client.Content c)
        {
            List<string> result = new List<string>();
            var fields = GetInstanceField(typeof(SenseNet.Client.Content), c, "_fields") as IDictionary<string, object>;
            dynamic _responseContent = GetInstanceField(typeof(SenseNet.Client.Content), c, "_responseContent") as dynamic;
            foreach (var item in (_responseContent as JObject))
            {
                result.Add(item.Key);
            }
            result.AddRange(fields.Keys);
            return result.Distinct().ToArray();
        }

        /// <summary>
        /// Uses reflection to get the field value from an object.
        /// </summary>
        ///
        /// <param name="type">The instance type.</param>
        /// <param name="instance">The instance object.</param>
        /// <param name="fieldName">The field's name which is to be fetched.</param>
        ///
        /// <returns>The field value from the object.</returns>
        internal static object GetInstanceField(Type type, object instance, string fieldName)
        {
            BindingFlags bindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic
                | BindingFlags.Static;
            FieldInfo field = type.GetField(fieldName, bindFlags);
            return field.GetValue(instance);
        }
    }
}
