using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlSerialization
{
    internal class Selector
    {
        public string? Id { get; set; } = null;

        public string? TagName { get; set; } = null;

        public List<string> Classes { get; set; } = new();

        public Selector? Parent { get; set; }

        public Selector? Child { get; set; }

        public static Selector FromString(string queryString)
        {
            var queries = queryString.Split(' ');

            var root = new Selector();
            var current = root;
            foreach (var query in queries)
            {
                var q = query.Replace('.', '&').Replace('#', '&');

                var idName = query.IndexOf('#');
                var className = query.IndexOf('.');

                if (query[0] != '#' && query[0] != '.')
                {
                    var endString = q.IndexOf('&');
                    current.TagName = query[0..(endString > 0 ? endString : ^0)];
                }

                if (idName >= 0)
                {
                    var endString = q.IndexOf('&', idName + 1);
                    current.Id = query[(idName + 1)..(endString > 0 ? endString : ^0)];
                }

                while (className >= 0)
                {
                    var endString = q.IndexOf('&', className + 1);
                    current.Classes.Add(query[(className + 1)..(endString > 0 ? endString : ^0)]);
                    className = query.IndexOf('.', className + 1);
                }

                current = new Selector() { Parent = current };
                current.Parent.Child = current;
            }
            current.Parent.Child = null;

            return root;
        }
    }
}
