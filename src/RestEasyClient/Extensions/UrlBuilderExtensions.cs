using System;
using System.Linq;
using System.Text;

namespace RestEasyClient.Extensions
{
    internal static class UrlBuilderExtensions
    {
        internal static string ToQueryString(this object data)
        {
            if (data == null) return "";
            return "?" + GetQueryStringParametersFromObject(data);
        }

        private static string GetQueryStringParametersFromObject(object data)
        {
            var queryString = new StringBuilder();
            Type dataType = data.GetType();
            dataType
                .GetProperties()
                .OrderBy(x => x.Name)
                .ToList()
                .ForEach(field =>
                {
                    var fieldValue = field.GetValue(data);
                    if (fieldValue != null)
                    {
                        if (IsBasicQueryStringParameter(fieldValue))
                        {
                            queryString.Append(String.Format("{0}={1}&", Uri.EscapeDataString(field.Name),
                                Uri.EscapeDataString(fieldValue.ToString())));
                        }
                        else queryString.Append(GetQueryStringParametersFromObject(fieldValue));
                    }
                });
            return queryString
                .Remove(queryString.Length - 1, 1)
                .ToString();
        }

        private static bool IsBasicQueryStringParameter(object obj)
        {
            return obj.GetType().IsPrimitive || obj is DateTime || obj is string || obj is Guid;
        }
    }
}