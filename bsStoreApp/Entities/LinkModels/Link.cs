using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.LinkModels
{
    public class Link
    {
        public string? hyperReference{ get; set; }
        public string? Relation { get; set; }
        public string? Method { get; set; }

        public Link()
        {
            
        }

        public Link(string? hyperReference, string? relation, string? method)
        {
            this.hyperReference = hyperReference;
            Relation = relation;
            Method = method;
        }
    }
    public class LinkResourceBase
    {
        public LinkResourceBase()
        {
            
        }
        public List<Link> Links { get; set; } = new List<Link>();
    }
}
