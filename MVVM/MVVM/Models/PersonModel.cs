using SQLite;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MVVM.Models
{
    public class PersonModel : INotifyPropertyChanged, INotifyCollectionChanged
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        //public string Name { get; set; }
        private string _name;
        public string Surname { get; set; }
        public string Name { get { return _name; } set { _name = value; OnPropetyChanged("Name"); } }

        public event PropertyChangedEventHandler PropertyChanged;
        public event NotifyCollectionChangedEventHandler CollectionChanged;
        void RaiseCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            var handler = CollectionChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        void OnPropetyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Xamarin.Forms.Device.BeginInvokeOnMainThread(() => { RaiseCollectionChanged(e); });
        }
    }
}