using DateTimeTool.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DateTimeTool.ViewModels
{
    public class OffsetViewModel : NotifyModel
    {
        public enum DateTimeActionType
        {
            Add = 1,
            Subtract = 2
        }

        #region Nested VMs

        private DateTimeViewModel _startDateVM;
        public DateTimeViewModel StartDateVM => _startDateVM ?? (_startDateVM = new DateTimeViewModel());

        #endregion

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

        private ObservableCollection<OffsetTabResult> _results;
        public ObservableCollection<OffsetTabResult> Results => _results ?? (_results = new ObservableCollection<OffsetTabResult>());

        #endregion

        #region Cmd

        private ICommand _calculateCmd;
        public ICommand CalculateCmd => _calculateCmd ?? (_calculateCmd = new RelayCommand(_ =>
        {
            DateTime date;

            try
            {
                date = _startDateVM.Compose();
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("The start date is not set.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
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

            var r = new OffsetTabResult(date);
            Results.Clear();
            Results.Add(r);
        }));

        private ICommand _copyToClipboard;
        public ICommand CopyToClipboard => _copyToClipboard ?? (_copyToClipboard = new RelayCommand(_ =>
        {
            var r = Results[0];
            Clipboard.SetText($"Local: {r.LocalTime}\nUTC: {r.UtcTime}\nSystem.CurrentMills: {r.CurrentMillis}");            
        }));

        #endregion
    }
}
