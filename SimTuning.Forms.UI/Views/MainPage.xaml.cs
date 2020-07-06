using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using SimTuning.Forms.UI.ViewModels;

namespace SimTuning.Forms.UI.Views
{
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Root, WrapInNavigationPage = false, NoHistory = true)]
    public partial class MainPage : MvxMasterDetailPage<MainPageViewModel>
    {
        private bool _firstTime = true;

        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            if (_firstTime)
            {
                ViewModel.ShowMenuViewModelCommand.Execute(null);
                ViewModel.ShowHomeViewModelCommand.Execute(null);

                _firstTime = false;
            }

            base.OnAppearing();
        }
    }
}