using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLayer.Dto
{
    public class CommonDto<T>
    {
        public List<T> Data { get; set; }
        public int TotalCount { get; set; }
        public int Page { get; set; }
    }
    public class SeleltDto
    {
        public int Value { get; set; }
        public string Label { get; set; }
    }
}
