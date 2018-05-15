using DateTimeTool.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DateTimeTool.ViewModels
{
    public class DateTimeViewModel : NotifyModel
    {
        #region Values

        private DateTime? _date;
        public DateTime? Date
        {
            get => _date;
            set
            {
                if (_date != value)
                {
                    _date = value;
                    OnPropertyChanged();
                }
            }
        }

        private double? _time_hh;
        public double? Time_HH
        {
            get => _time_hh;
            set
            {
                if (_time_hh != value)
                {
                    _time_hh = value;
                    OnPropertyChanged();
                }
            }
        }

        private double? _time_mm;
        public double? Time_MM
        {
            get => _time_mm;
            set
            {
                if (_time_mm != value)
                {
                    _time_mm = value;
                    OnPropertyChanged();
                }
            }
        }

        private double? _time_ss;
        public double? Time_SS
        {
            get => _time_ss;
            set
            {
                if (_time_ss != value)
                {
                    _time_ss = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region Commands

        private ICommand _nowCmd;
        public ICommand NowCmd => _nowCmd ?? (_nowCmd = new RelayCommand(_ =>
        {
            var now = DateTime.Now;
            Date = now;
            Time_HH = now.Hour;
            Time_MM = now.Minute;
            Time_SS = now.Second;
        }));

        private ICommand _resetCmd;
        public ICommand ResetCmd => _resetCmd ?? (_resetCmd = new RelayCommand(_ =>
        {
            Date = null;
            Time_HH = null;
            Time_MM = null;
            Time_SS = null;
        }));

        #endregion

        #region Utils

        public DateTime Compose()
        {
            if (Date == null)
            {
                throw new InvalidOperationException("The DateTime value is not set.");
            }

            DateTime date = Date.Value.Date;

            if (Time_HH != null)
            {
                date = date.AddHours(Time_HH.Value);
            }
            if (Time_MM != null)
            {
                date = date.AddMinutes(Time_MM.Value);
            }
            if (Time_SS != null)
            {
                date = date.AddSeconds(Time_SS.Value);
            }

            return date;
        }

        #endregion
    }
}
