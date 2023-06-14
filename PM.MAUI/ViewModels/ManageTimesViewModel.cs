using PM.Library.Models;
using PM.Library.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PM.MAUI.ViewModels
{
    class ManageTimesViewModel : INotifyPropertyChanged
    {
        public Time SelectedTime { get; set; }
        public string Query { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Time> Times
        {
            get
            {
                return new ObservableCollection<Time>(TimeService.Current.Times);
            }
        }

        public void RefreshView()
        {
            NotifyPropertyChanged(nameof(Times));
        }

        public void EditTimeClick(Shell s)
        {
            var idParam = SelectedTime?.Id ?? 0;

            if(idParam == 0)
            {
                return;
            }

            s.GoToAsync($"//TimeDetail?timeId={idParam}");
        }

        public void Delete()
        {
            if(SelectedTime == null)
            {
                return;
            }

            TimeService.Current.DeleteTime(SelectedTime.Id);
            RefreshView();
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
