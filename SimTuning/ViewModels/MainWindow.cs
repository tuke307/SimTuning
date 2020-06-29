using MvvmCross.ViewModels;
using System.Threading.Tasks;

namespace SimTuning.ViewModels
{
    public class MainWindow : MvxViewModel
    {
        public MainWindow()
        {
            Application_load();
        }

        public override void Prepare()
        {
            // This is the first method to be called after construction
        }

        public override Task Initialize()
        {
            // Async initialization

            return base.Initialize();
        }

        #region Commands

        protected virtual void Application_load()
        {
        }

        #endregion Commands

        #region Values

        private bool _userValid;

        public bool UserValid
        {
            get => _userValid;
            set { SetProperty(ref _userValid, value); }
        }

        private bool _licenseValid;

        public bool LicenseValid
        {
            get => _licenseValid;
            set { SetProperty(ref _licenseValid, value); }
        }

        #endregion Values
    }
}