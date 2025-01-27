using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Projekt
{
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
    }

    public interface IAddOnClick
    {
        void AddOnClick(double value);
    }
    public interface IAddOnTick
    {
        void AddOnTick(double value);
    }
}
