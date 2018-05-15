using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeTool.ViewModels
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            DateAndTimeContext = new DateAndTimeViewModel();
        }

        public DateAndTimeViewModel DateAndTimeContext { get; set; }

        private RangeViewModel _ragenVM;
        public RangeViewModel RangeVM => _ragenVM ?? (_ragenVM = new RangeViewModel());
    }
}
