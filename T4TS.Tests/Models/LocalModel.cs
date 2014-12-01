using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T4TS.Tests.Models
{
    [TypeScriptInterface]
    public class LocalModel
    {
        public int Id { get; set; }

        [TypeScriptMember(Optional = true, CamelCase=true)]
        public string Optional { get; set; }
    }


    [TypeScriptInterface]
    public class ParentModel
    {
        public int Id { get; set; }
        public ChildModel Child { get; set; }
        public ChildModel[] Children { get; set; }
    }

    [TypeScriptInterface]
    public class ChildModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
    }
}
