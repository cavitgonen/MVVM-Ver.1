using MVVM.Droid;
using MVVM.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MVVM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListViewPage1 : ContentPage
    {

        public ListViewPage1()
        {
            InitializeComponent();
        }

        private void OnMenuAyar(Object sender, EventArgs e)
        {
            //Navigation.PushAsync(new SettingsPage());
        }
    }
}
