using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlSerialiser
{
    internal class Selector
    {
        public string? Id { get; set; }

        public string? TagName { get; set; }

        public List<string> Classes { get; set; } = new();

        public Selector? Parent { get; set; }

        public Selector? Child { get; set; }

        public static Selector ConvertToSelector(string queryString)
        {
            var queries = queryString.Split(' ');

            var root = new Selector();
            var current = root;
            foreach (var query in queries)
            {
                var subQueries = query.Split('#', '.');
                int sd = 0, sl = 0;
                foreach (var subQuery in subQueries)
                {
                    var dot = query.IndexOf('.', sd);
                    var ladder = query.IndexOf('#', sl);

                    if (dot > ladder && dot > 0)
                        current.Id = subQuery;
                    else if (ladder > 0)
                        current.Classes.Add(subQuery);
                    else
                        current.TagName = subQuery;

                    sd = dot > 0 ? dot : query.Length - 1;
                    sl = ladder > 0 ? ladder : query.Length - 1;
                }

                current = new Selector() { Parent = current };
                current.Parent.Child = current;
            }

            return root;
        }
    }
}
