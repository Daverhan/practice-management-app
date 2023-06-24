using PM.Library.Models;
using PM.Library.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PM.MAUI.ViewModels
{
    class ManageTimesViewModel : INotifyPropertyChanged
    {
        public string Query { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<TimeViewModel> Times
        {
            get
            {
                if (string.IsNullOrEmpty(Query))
                {
                    return new ObservableCollection<TimeViewModel>(TimeService.Current.Times.Select(t => new TimeViewModel(t)).ToList());
                }
                return new ObservableCollection<TimeViewModel>(TimeService.Current.Search(Query).Select(t => new TimeViewModel(t)).ToList());
            }
        }

        public void RefreshView()
        {
            NotifyPropertyChanged(nameof(Times));
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
