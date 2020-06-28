using SimTuning.mobile.ViewModels;
using System;
using Xamarin.Forms;

namespace SimTuning.mobile.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();

            BindingContext = new MainWindowViewModel(this.Navigation);

            //masterPage.listFree.ItemSelected += OnItemSelected;
            //masterPage.listPro.ItemSelected += OnProItemSelected;
            //masterPage.listSettings.ItemSelected += OnItemSelected;
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterPageItem;
            if (item != null)
            {
                //Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                //this.menuList.SelectedItem = null;
                IsPresented = false;
            }
        }

        //private void OnProItemSelected(object sender, SelectedItemChangedEventArgs e)
        //{
        //    var item = e.SelectedItem as MasterPageItem;
        //    if (item != null)
        //    {
        //        Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
        //        masterPage.listPro.SelectedItem = null;
        //        IsPresented = false;
        //    }
        //}
    }
}