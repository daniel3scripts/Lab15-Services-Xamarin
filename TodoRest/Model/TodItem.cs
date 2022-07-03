using System;
using System.Collections.Generic;
using System.Text;

namespace TodoRest.Model
{
    public class TodItem
    {
        public string ID { get; set; }
        public string Name { get; set; }

        public string Notes { get; set; }
        public string Done { get; set; }
    }
}
