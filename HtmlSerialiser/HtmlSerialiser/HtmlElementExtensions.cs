using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlSerialiser
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

            List<HtmlElement> children = element.Descendants().Where(e => e.MeetsSelectorRequirement(selector)).ToList();
            Console.WriteLine("fdfdf");
            foreach (var child in children)
                QueryRecursion(child, selector.Child, response);

        }

        //private static bool MeetsSelectorRequirement(this HtmlElement element, Selector selector) =>
        //    (selector.TagName == null || selector.TagName == element.Name) &&
        //    (selector.Id == null || selector.Id == element.Id) &&
        //    (selector.Classes.Count == 0 || !selector.Classes.Any(s => !element.Classes.Contains(s)));

        private static bool MeetsSelectorRequirement(this HtmlElement element, Selector selector)
        {
            bool res =
            (selector.TagName == null || selector.TagName == element.Name) &&
            (selector.Id == null || selector.Id == element.Id) &&
            (selector.Classes.Count == 0 || !selector.Classes.Any(s => !element.Classes.Contains(s)));

            if (res == false)
            {
                if (element.Classes.Contains("md-header"))
                    foreach (var item in element.Classes)
                    {
                        Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                        Console.WriteLine("class of element: " + item);
                    }
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine(element);
                Console.WriteLine("name: " + selector.TagName + " id: " + selector.Id);
                foreach (var item in selector.Classes)
                {
                    Console.WriteLine("class: " + item);
                }
            }
            return res;
        }

    }
}
