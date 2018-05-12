using DateTimeTool.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DateTimeTool.ViewModels
{
    public class DateAndTimeViewModel : NotifyModel
    {
        public enum DateTimeActionType
        {
            Add = 1,
            Subtract = 2
        }

        #region Properties

        private IEnumerable<string> _dateTimeActionTypes;
        public IEnumerable<string> DateTimeActionTypes => _dateTimeActionTypes ?? (_dateTimeActionTypes = Enum.GetNames(typeof(DateTimeActionType)));

        private string _selectedAction = DateTimeActionType.Add.ToString();
        public string SelectedAction
        {
            get => _selectedAction;
            set
            {
                if (_selectedAction != value)
                {
                    _selectedAction = value;
                    Debug.WriteLine(_selectedAction);
                    OnPropertyChanged();
                }
            }
        }

        #region Current date and time

        private DateTime? _currentDate;
        public DateTime? CurrentDate
        {
            get => _currentDate;
            set
            {
                if (_currentDate != value)
                {
                    _currentDate = value;
                    OnPropertyChanged();
                }
            }
        }

        private double? _currentTime_hh;
        public double? CurrentTime_HH
        {
            get => _currentTime_hh;
            set
            {
                if (_currentTime_hh != value)
                {
                    _currentTime_hh = value;
                    OnPropertyChanged();
                }
            }
        }

        private double? _currentTime_mm;
        public double? CurrentTime_MM
        {
            get => _currentTime_mm;
            set
            {
                if (_currentTime_mm != value)
                {
                    _currentTime_mm = value;
                    OnPropertyChanged();
                }
            }
        }

        private double? _currentTime_ss;
        public double? CurrentTime_SS
        {
            get => _currentTime_ss;
            set
            {
                if (_currentTime_ss != value)
                {
                    _currentTime_ss = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region Offset

        private double? _offsetTime_dd;
        public double? OffsetTime_DD
        {
            get => _offsetTime_dd;
            set
            {
                if (_offsetTime_dd != value)
                {
                    _offsetTime_dd = value;
                    OnPropertyChanged();
                }
            }
        }

        private double? _offsetTime_hh;
        public double? OffsetTime_HH
        {
            get => _offsetTime_hh;
            set
            {
                if (_offsetTime_hh != value)
                {
                    _offsetTime_hh = value;
                    OnPropertyChanged();
                }
            }
        }

        private double? _offsetTime_mm;
        public double? OffsetTime_MM
        {
            get => _offsetTime_mm;
            set
            {
                if (_offsetTime_mm != value)
                {
                    _offsetTime_mm = value;
                    OnPropertyChanged();
                }
            }
        }

        private double? _offsetTime_ss;
        public double? OffsetTime_SS
        {
            get => _offsetTime_ss;
            set
            {
                if (_offsetTime_ss != value)
                {
                    _offsetTime_ss = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        private List<DateAndTimeResult> _results;
        public List<DateAndTimeResult> Results
        {
            get => _results;
            set
            {
                if (_results != value)
                {
                    _results = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region Cmd

        private ICommand _nowDateCmd;
        public ICommand NowDateCmd => _nowDateCmd ?? (_nowDateCmd = new RelayCommand(_ =>
        {
            var now = DateTime.Now;
            CurrentDate = now;
            CurrentTime_HH = now.Hour;
            CurrentTime_MM = now.Minute;
            CurrentTime_SS = now.Second;
        }));

        private ICommand _nowDateResetCmd;
        public ICommand NowDateResetCmd => _nowDateResetCmd ?? (_nowDateResetCmd = new RelayCommand(_ =>
        {
            CurrentDate = null;
            CurrentTime_HH = null;
            CurrentTime_MM = null;
            CurrentTime_SS = null;
        }));

        private ICommand _calculateCmd;
        public ICommand CalculateCmd => _calculateCmd ?? (_calculateCmd = new RelayCommand(_ =>
        {
            if (CurrentDate == null)
            {
                // msg
                return;
            }

            DateTime date = CurrentDate.Value.Date;

            if (CurrentTime_HH != null)
            {
                date = date.AddHours(CurrentTime_HH.Value);
            }
            if (CurrentTime_MM != null)
            {
                date = date.AddMinutes(CurrentTime_MM.Value);
            }
            if (CurrentTime_SS != null)
            {
                date = date.AddSeconds(CurrentTime_SS.Value);
            }

            DateTimeActionType actionType = (DateTimeActionType)Enum.Parse(typeof(DateTimeActionType), SelectedAction);

            if (OffsetTime_DD != null)
            {
                date = date.AddDays(actionType == DateTimeActionType.Add ? OffsetTime_DD.Value : -OffsetTime_DD.Value);
            }
            if (OffsetTime_HH != null)
            {
                date = date.AddHours(actionType == DateTimeActionType.Add ? OffsetTime_HH.Value : -OffsetTime_HH.Value);
            }
            if (OffsetTime_MM != null)
            {
                date = date.AddMinutes(actionType == DateTimeActionType.Add ? OffsetTime_MM.Value : -OffsetTime_MM.Value);
            }
            if (OffsetTime_SS != null)
            {
                date = date.AddSeconds(actionType == DateTimeActionType.Add ? OffsetTime_SS.Value : -OffsetTime_SS.Value);
            }

            var r = new DateAndTimeResult
            {
                DateAndTime = date.ToString("dd MMM yyyy HH:mm:ss")
            };

            Results = new List<DateAndTimeResult>
            {
                r
            };
        }));

        #endregion
    }
}
