using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using SimTuning.Core.Models;

namespace SimTuning.WPF.Base.ViewModels.Einstellungen
{
    public class EinstellungenVehiclesViewModel : SimTuning.Core.ViewModels.Einstellungen.VehiclesViewModel
    {
        //private readonly MainWindowViewModel mainWindowViewModel;

        public EinstellungenVehiclesViewModel/*MainWindowViewModel mainWindowViewModel*/(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            //this.mainWindowViewModel = mainWindowViewModel;

            NewVehicleCommand = new MvxCommand(NewVehicle, CanExecute);
            DeleteVehicleCommand = new MvxCommand(DeleteVehicle, CanExecute);
            SaveVehicleCommand = new MvxCommand(SaveVehicle, CanExecute);
        }

        public override void Prepare(UserModel _user)
        {
            base.Prepare(_user);
        }

        public override Task Initialize()
        {
            return base.Initialize();
        }

        private bool CanExecute()
        {
            if (!User.LicenseValid)
            {
                //mainWindowViewModel.NotificationSnackbar.Enqueue("Kaufe die Pro Version um Presets zu ändern");
            }
            return User.LicenseValid;
        }

        protected override void NewVehicle()
        {
            try
            {
                base.NewVehicle();
            }
            catch
            {
                //mainWindowViewModel.NotificationSnackbar.Enqueue("Fehler beim erstellen");
            }
        }

        protected override void DeleteVehicle()
        {
            try
            {
                base.DeleteVehicle();
            }
            catch
            {
                //mainWindowViewModel.NotificationSnackbar.Enqueue("Fehler beim löschen");
            }
        }

        protected override void SaveVehicle()
        {
            try
            {
                base.SaveVehicle();
            }
            catch
            {
                //mainWindowViewModel.NotificationSnackbar.Enqueue("Fehler beim speichern");
            }
        }
    }
}