using System;
using System.Linq;
using System.Reflection;
using System.Text;

namespace RestEasyClient.Extensions
{
    internal static class UrlBuilderExtensions
    {
        internal static string ToQueryString(this object Data)
        {
            if (Data == null) return "";
            return "?" + GetQueryStringParametersFromObject(Data);
        }

        private static string GetQueryStringParametersFromObject(object Data)
        {
            var queryString = new StringBuilder();
            Type dataType = Data.GetType();
            dataType
                .GetProperties()
                .OrderBy(x => x.Name)
                .ToList()
                .ForEach(f => AppendFieldToQueryString(f, queryString, Data));
            return queryString
                .Remove(queryString.Length - 1, 1)
                .ToString();
        }

        private static void AppendFieldToQueryString(PropertyInfo Field, StringBuilder QueryString, object Data)
        {
            var fieldValue = Field.GetValue(Data);
            if (fieldValue != null)
            {
                if (IsBasicQueryStringParameter(fieldValue))
                {
                    QueryString.Append(String.Format("{0}={1}&", Uri.EscapeDataString(Field.Name),
                        Uri.EscapeDataString(fieldValue.ToString())));
                }
                else QueryString.Append(GetQueryStringParametersFromObject(fieldValue));
            }
        }

        private static bool IsBasicQueryStringParameter(object Obj)
        {
            return Obj.GetType().IsPrimitive || Obj is DateTime || Obj is string || Obj is Guid;
        }
    }
}