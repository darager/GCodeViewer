using System.ComponentModel;

namespace MultithreadingBindingTest
{
    public class TextViewModel : INotifyPropertyChanged
    {
        private string labeltext;

        public string text
        {
            get { return labeltext; }
            set
            {
                if (labeltext != value)
                {
                    labeltext = value;
                    OnPropertyChanged("text");
                }
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
