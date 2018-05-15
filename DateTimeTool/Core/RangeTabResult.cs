using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeTool.Core
{
    public class RangeTabResult
    {
        public RangeTabResult(TimeSpan timeSpan)
        {
            TimeSpan = timeSpan;            
        }

        public TimeSpan TimeSpan { get; set; }
    }
}
