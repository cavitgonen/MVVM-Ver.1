using MVVM.Droid;
using MVVM.Models;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace MVVM.ViewModels
{
    public class PersonViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<PersonModel> _person;
        ICommand deleteCommand, refreshCommand;
        private bool _isRefreshing;
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropetyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void BindData()
        {
            this.IsRefreshing = true;
            SQLiteManager manager = new SQLiteManager();
            Person = new ObservableCollection<PersonModel>(manager.GetAll().ToList());
            this.IsRefreshing = false;
        }

        public PersonViewModel()
        {
            BindData();
            deleteCommand = new Command(OnDelete);
            refreshCommand = new Command(OnRefresh);
        }

        public ICommand DeleteCommand
        {
            get
            {
                return deleteCommand;
            }
            set
            {
                if (deleteCommand == null)
                    return;
                deleteCommand = value;
            }
        }
        public ICommand RefreshCommand
        {
            get
            {
                return refreshCommand;
            }
            set
            {
                if (refreshCommand == null)
                    return;
                refreshCommand = value;
            }
        }

        public ObservableCollection<PersonModel> Person
        {
            get
            {
                if (_person == null)
                {
                    _person = new ObservableCollection<PersonModel>();
                }
                return _person;
            }
            set
            {
                _person = value;
            }
        }

        public bool IsRefreshing
        {
            get
            {
                return _isRefreshing;
            }
            set
            {
                _isRefreshing = value;
                OnPropetyChanged();
            }
        }
        private void OnDelete(object param)
        {
            PersonModel selectModel = (PersonModel)param;
            if (selectModel != null)
                Person.Remove(selectModel);
        }
        private void OnRefresh(object param)
        {
            BindData();
        }
    }
}