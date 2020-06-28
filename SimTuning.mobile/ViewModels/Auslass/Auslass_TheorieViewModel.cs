using Xamarin.Forms;

namespace SimTuning.mobile.ViewModels.Auslass
{
    public class Auslass_TheorieViewModel : SimTuning.ViewModels.Auslass.TheorieViewModel
    {
        //private readonly AuslassLogic auslass;

        public Auslass_TheorieViewModel()
        {
            //auslass = new AuslassLogic();

            InsertDataCommand = new Command(InsertData);

            //using (var db = new DatabaseContext())
            //{
            //    IList<VehiclesModel> vehicList = db.Vehicles
            //        .Include(vehicles => vehicles.Motor)
            //        .Include(vehicles => vehicles.Motor.Auslass)
            //        .ToList();

            //    Vehicles = new ObservableCollection<VehiclesModel>(vehicList);
            //}
        }

        //private void InsertData(object obj)
        //{
        //    AuslassA = Vehicle.Motor.Auslass.Flaeche;
        //    ResonanzDrehzahl = Vehicle.Motor.ResonanzU;
        //    AusslassSteuerwinkel = Vehicle.Motor.Auslass.SteuerzeitSZ;
        //}

        //public ICommand InsertDataCommand { get; set; }

        //public ObservableCollection<VehiclesModel> Vehicles
        //{
        //    get => Get<ObservableCollection<VehiclesModel>>();
        //    set => Set(value);
        //}

        //public VehiclesModel Vehicle
        //{
        //    get => Get<VehiclesModel>();
        //    set => Set(value);
        //}

        //public string KruemmerSpannneD
        //{
        //    get => Get<string>();
        //    set => Set(value);
        //}

        //public double? AuslassA
        //{
        //    get => Get<double?>();
        //    set { Set(value); Refresh_KruemmerD(); }
        //}

        //public double? KruemmerD
        //{
        //    get => Get<double?>();
        //    set { Set(value); Refresh_KruemmerL(); }
        //}

        //public double? DrehmomentF
        //{
        //    get => Get<double?>();
        //    set { Set(value); Refresh_KruemmerL(); }
        //}

        //public double? KruemmerL
        //{
        //    get => Get<double?>();
        //    set => Set(value);
        //}

        //public double? ModAbgasT
        //{
        //    get => Get<double?>();
        //    set { Set(value); Refresh_AuspuffGeschwindigkeit(); }
        //}

        //public double? AbgasV
        //{
        //    get => Get<double?>();
        //    set => Set(value);
        //}

        //public double? AbgasT
        //{
        //    get => Get<double?>();
        //    set { Set(value); Refresh_Resonanzlaenge(); }
        //}

        //public double? AusslassSteuerwinkel
        //{
        //    get => Get<double?>();
        //    set { Set(value); Refresh_Resonanzlaenge(); }
        //}

        //public double? ResonanzDrehzahl
        //{
        //    get => Get<double?>();
        //    set { Set(value); Refresh_Resonanzlaenge(); }
        //}

        //public double? ResonanzL
        //{
        //    get => Get<double?>();
        //    set => Set(value);
        //}

        //private void Refresh_KruemmerL()
        //{
        //    if (KruemmerD.HasValue)
        //    {
        //        if (!DrehmomentF.HasValue)
        //            DrehmomentF = 9;

        //        KruemmerL = auslass.Get_Kruemmerlaenge(KruemmerD.Value, DrehmomentF.Value, 0);
        //    }
        //}

        //private void Refresh_KruemmerD()
        //{
        //    if (AuslassA.HasValue)
        //        KruemmerSpannneD = auslass.Get_KruemmerDurchmesserSpanne(AuslassA.Value);
        //}

        //private void Refresh_AuspuffGeschwindigkeit()
        //{
        //    if (ModAbgasT.HasValue)
        //        AbgasV = auslass.Get_Abgasgeschwindigkeit(ModAbgasT.Value);
        //}

        //private void Refresh_Resonanzlaenge()
        //{
        //    if (AusslassSteuerwinkel.HasValue && AbgasT.HasValue && ResonanzDrehzahl.HasValue)
        //        ResonanzL = auslass.Get_Resonanzlaenge(AusslassSteuerwinkel.Value, AbgasT.Value, ResonanzDrehzahl.Value);
        //}
    }
}