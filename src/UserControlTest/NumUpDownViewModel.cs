using System.ComponentModel;

namespace UserControlTest
{
    public class NumUpDownViewModel : INotifyPropertyChanged
    {
        public float NumValue
        {
            get => _value;
            set
            {
                if (_value == value) return;

                _value = value;
                OnPropertyChanged("NumValue");
            }
        }
        private float _value = 0;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
