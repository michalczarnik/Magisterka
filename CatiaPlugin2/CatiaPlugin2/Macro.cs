using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatiaPlugin2
{
    class Macro
    {
        public string DirectoryName { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
        public string FullName {
            get
            {
                return DirectoryName + "\\" + FileName;
            }
            private set { }
        }
        public List<Parameter> Parameters = new List<Parameter>();
        public Dictionary<string, string> Images = new Dictionary<string, string>();
    }

    class Parameter
    {
        public string ParameterName { get; set; }
        public string DisplayName { get; set; }
        public string Type { get; set; }
        public string DefaultValue { get; set; }
    }
}
