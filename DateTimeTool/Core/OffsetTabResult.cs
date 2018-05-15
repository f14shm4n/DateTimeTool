using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeTool.Core
{
    public class OffsetTabResult
    {
        private const string DateTimeFormat = "dd MMM yyyy HH:mm:ss";
                
        public OffsetTabResult(DateTime date)
        {
            Value = date;
        }

        public DateTime Value { get; set; }
        public string LocalTime => Value.ToString(DateTimeFormat);
        public string UtcTime => Value.ToUniversalTime().ToString(DateTimeFormat);
        public long CurrentMillis => TimeHelper.CurrentTimeMillis(Value);
    }
}
