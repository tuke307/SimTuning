using MvvmCross.ViewModels;
using SimTuning.Core.Models;
using System.Threading.Tasks;

namespace SimTuning.Core.ViewModels
{
    public class MainPage : MvxViewModel
    {
        public MainPage()
        {
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

        protected virtual void ApplicationLoad()
        {
        }

        #endregion Commands

        #region Values

        private UserModel _user;

        public UserModel User
        {
            get => _user;
            set { SetProperty(ref _user, value); }
        }

        #endregion Values
    }
}