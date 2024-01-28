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
            if (selector == null)
            {
                response.Add(element);
                return;
            }

            var children = element.Descendants().Where(e => e.MeetsSelectorRequirement(selector));
            
            foreach (var child in children)
                QueryRecursion(child, selector.Child, response);
        }

        private static bool MeetsSelectorRequirement(this HtmlElement element, Selector selector) =>
            (selector.TagName == null || selector.TagName == element.Name) &&
            (selector.Id == null || selector.Id == element.Id) &&
            (selector.Classes.Count == 0 || !selector.Classes.Any(s => !element.Classes.Contains(s)));

    }
}
