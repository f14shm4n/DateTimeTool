using DateTimeTool.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DateTimeTool.ViewModels
{
    public class RangeViewModel : NotifyModel
    {
        public RangeViewModel()
        {
        }

        #region Nested VMs

        private DateTimeViewModel _startDateVM;
        public DateTimeViewModel StartDateVM => _startDateVM ?? (_startDateVM = new DateTimeViewModel());

        private DateTimeViewModel _endDateVM;
        public DateTimeViewModel EndDateVM => _endDateVM ?? (_endDateVM = new DateTimeViewModel());

        #endregion

        #region Values

        private ObservableCollection<RangeTabResult> _results;
        public ObservableCollection<RangeTabResult> Results => _results ?? (_results = new ObservableCollection<RangeTabResult>());

        #endregion

        #region Commands

        private ICommand _calculateCmd;
        public ICommand CalculateCmd => _calculateCmd ?? (_calculateCmd = new RelayCommand(_ =>
        {
            DateTime startDate;
            DateTime endDate;

            try
            {
                startDate = _startDateVM.Compose();
                endDate = _endDateVM.Compose();
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("The start or end dates is not set.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            Results.Clear();
            Results.Add(new RangeTabResult(endDate - startDate));
        }));

        private ICommand _copyToClipboard;
        public ICommand CopyToClipboard => _copyToClipboard ?? (_copyToClipboard = new RelayCommand(_ =>
        {
            var r = Results[0];
            Clipboard.SetText($"Time span: {r.TimeSpan.ToString("dd")} days {r.TimeSpan.ToString("hh\\:mm\\:ss")}\nTotalDays: {r.TimeSpan.TotalDays}\nTotalHours: {r.TimeSpan.TotalHours}\nTotalMinutes: {r.TimeSpan.TotalMinutes}\nTotalSeconds: {r.TimeSpan.TotalSeconds}\nTotalMilliseconds: {r.TimeSpan.TotalMilliseconds}");
        }));

        #endregion
    }
}
