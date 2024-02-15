using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlSerialization
{
    internal static class HtmlElementExtensions
    {
        public static IEnumerable<HtmlElement> Query(this HtmlElement element, Selector selector)
        {
            var res = new HashSet<HtmlElement>();
            QueryRecursion(element, selector, res);
            return res;
        }

        private static void QueryRecursion(HtmlElement element, Selector? selector, HashSet<HtmlElement> response)
        {
            if (selector is null)
            {
                response.Add(element);
                return;
            }

            foreach (var child in element.Descendants().Where(e => e.MeetsRequirement(selector)))
                QueryRecursion(child, selector.Child, response);
        }

        private static bool MeetsRequirement(this HtmlElement element, Selector? selector) =>
            (selector is not null) &&
            (selector.TagName is null || selector.TagName == element.Name) &&
            (selector.Id is null || selector.Id == element.Id) &&
            (selector.Classes.Count == 0 || !selector.Classes.Any(s => !element.Classes.Contains(s)));

    }
}
