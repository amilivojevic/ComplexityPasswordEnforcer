using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordEnforcer.model
{
    public class Topology
    {
        public String name { get; set; }
        public String regex { get; set; }
        public bool default_topology { get; set; }
        public bool common_topology { get; set; }
        public int length { get; set; }

        public Topology(String name, String regex, bool default_topology, bool common_topology, int length)
        {
            this.name = name;
            this.regex = regex;
            this.default_topology = default_topology;
            this.common_topology = common_topology;
            this.length = length;
        }

        public String toString()
        {
            return "Name: " + name + "\tregex: " + regex + "\tdefault: " + default_topology + "\tcommon: " + common_topology + "\tlength: " + length;
        }

    }
}
