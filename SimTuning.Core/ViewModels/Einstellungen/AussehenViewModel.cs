using Data;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using SimTuning.Core.Models;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;

namespace SimTuning.Core.ViewModels.Einstellungen
{
    public class AussehenViewModel : MvxNavigationViewModel<UserModel>
    {
        public UserModel User;
        protected ResourceManager rm;

        public AussehenViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            this.rm = new ResourceManager(typeof(SimTuning.Core.resources));
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
    }
}