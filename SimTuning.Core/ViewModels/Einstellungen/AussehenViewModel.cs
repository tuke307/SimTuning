using Data;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using SimTuning.Core.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SimTuning.Core.ViewModels.Einstellungen
{
    public class AussehenViewModel : MvxNavigationViewModel<UserModel>
    {
        public UserModel User;

        public AussehenViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            using (var db = new DatabaseContext())
                ToogleDarkmode = db.Settings.ToList().Last().DarkMode;
        }

        public IMvxCommand ApplyPrimaryCommand { get; set; }
        public IMvxCommand ApplyAccentCommand { get; set; }

        public override void Prepare(UserModel _user)
        {
            // This is the first method to be called after construction

            User = _user;
        }

        public override Task Initialize()
        {
            // Async initialization

            return base.Initialize();
        }

        #region Commands

        protected virtual void ApplyPrimary()
        {
        }

        protected virtual void ApplyAccent()
        {
        }

        protected virtual void ApplyBaseTheme()
        {
        }

        #endregion Commands

        #region Values

        private bool _toogleDarkmode;

        public bool ToogleDarkmode
        {
            get => _toogleDarkmode;
            set
            {
                SetProperty(ref _toogleDarkmode, value);
                ApplyBaseTheme();
            }
        }

        #endregion Values
    }
}