using Xamarin.Forms;

namespace SimTuning.mobile.ViewModels
{
    public class Motor_UmrechnungViewModel : SimTuning.ViewModels.Motor.UmrechnungViewModel
    {
        public Motor_UmrechnungViewModel()
        {
            InsertDataCommand = new Command(InsertData);

            //using (var db = new DatabaseContext())
            //{
            //    IList<VehiclesModel> vehicList = db.Vehicles
            //        .Include(vehicles => vehicles.Motor)
            //        .Include(vehicles => vehicles.Motor.Einlass)
            //        .Include(vehicles => vehicles.Motor.Auslass)
            //        .Include(vehicles => vehicles.Motor.Ueberstroemer)
            //        .ToList();

            //    Vehicles = new ObservableCollection<VehiclesModel>(vehicList);
            //}
        }

        //private SimTuning.ModuleLogic.EngineLogic steuerzeit = new SimTuning.ModuleLogic.EngineLogic();

        //public ObservableCollection<Data.Models.VehiclesModel> Vehicles
        //{
        //    get => Get<ObservableCollection<Data.Models.VehiclesModel>>();
        //    set => Set(value);
        //}

        //public Data.Models.VehiclesModel Vehicle
        //{
        //    get => Get<Data.Models.VehiclesModel>();
        //    set => Set(value);
        //}

        //public void InsertData(object parameter)
        //{
        //    hub = Vehicle.Motor.HubL;
        //    pleullaenge = Vehicle.Motor.Pleullaenge;
        //    deachsierung = Vehicle.Motor.DeachsierungL;
        //}

        ////Textboxen Inhalt wird gesetzt und gleichzeitig an das Objekt gegeben
        //public double? hub
        //{
        //    get => Get<double?>();
        //    set { Set(value); Refresh_hubradius(); }
        //}

        //public double? pleullaenge
        //{
        //    get => Get<double?>();
        //    set { Set(value); Refresh_hubradius(); }
        //}

        //public double? deachsierung
        //{
        //    get => Get<double?>();
        //    set { Set(value); Refresh_hubradius(); }
        //}

        //private void Refresh_hubradius()
        //{
        //    if (hub.HasValue && pleullaenge.HasValue && deachsierung.HasValue)
        //        hubradius = steuerzeit.Get_hubradius(hub.Value, pleullaenge.Value, deachsierung.Value);
        //}

        //public double? hubradius
        //{
        //    get => Get<double?>();
        //    set => Set(value);
        //}

        //public double? kwgrad
        //{
        //    get => Get<double?>();
        //    set { Set(value); Refresh_kwgrad(); }
        //}

        //public double? mmvorot
        //{
        //    get => Get<double?>();
        //    set => Set(value);
        //}

        //private void Refresh_kwgrad()
        //{
        //    if (pleullaenge.HasValue && hubradius.HasValue && deachsierung.HasValue && kwgrad.HasValue)
        //        mmvorot = steuerzeit.Get_mmvorot(pleullaenge.Value, hubradius.Value, deachsierung.Value, kwgrad.Value);
        //}

        //public ICommand InsertDataCommand { get; set; }

        ////erhöhen
        //public double? vorher_steuerzeit
        //{
        //    get => Get<double?>();
        //    set { Set(value); Refresh_Unterschied(); }
        //}

        //public double? nachher_steuerzeit
        //{
        //    get => Get<double?>();
        //    set { Set(value); Refresh_Unterschied(); }
        //}

        //public double? vorher_steuerwinkel_oeffnet
        //{
        //    get => Get<double?>();
        //    set => Set(value);
        //}

        //public double? nachher_steuerwinkel_oeffnet
        //{
        //    get => Get<double?>();
        //    set => Set(value);
        //}

        //public double? vorher_steuerwinkel_schließt
        //{
        //    get => Get<double?>();
        //    set => Set(value);
        //}

        //public double? nachher_steuerwinkel_schließt
        //{
        //    get => Get<double?>();
        //    set => Set(value);
        //}

        //public bool kolbenunterkante_checked
        //{
        //    get => Get<bool>();
        //    set { Set(value); Refresh_Unterschied(); }
        //}

        //public bool kolbenoberkante_checked
        //{
        //    get => Get<bool>();
        //    set { Set(value); Refresh_Unterschied(); }
        //}

        //public double? unterschied_grad
        //{
        //    get => Get<double?>();
        //    set => Set(value);
        //}

        //public double? unterschied_mm
        //{
        //    get => Get<double?>();
        //    set => Set(value);
        //}

        //private void Refresh_Unterschied()
        //{
        //    if (vorher_steuerzeit.HasValue && nachher_steuerzeit.HasValue && pleullaenge.HasValue && hubradius.HasValue && deachsierung.HasValue && (kolbenoberkante_checked || kolbenunterkante_checked))
        //    {
        //        List<double> steuerwinkel = new List<double>();
        //        steuerwinkel.AddRange(steuerzeit.Get_steuerwinkel(vorher_steuerzeit.Value, nachher_steuerzeit.Value, kolbenoberkante_checked, kolbenunterkante_checked));

        //        vorher_steuerwinkel_oeffnet = steuerwinkel[0];
        //        vorher_steuerwinkel_schließt = steuerwinkel[1];
        //        nachher_steuerwinkel_oeffnet = steuerwinkel[2];
        //        nachher_steuerwinkel_schließt = steuerwinkel[3];

        //        unterschied_grad = steuerzeit.Get_Unterschied_grad(vorher_steuerzeit.Value, nachher_steuerzeit.Value);
        //        unterschied_mm = steuerzeit.Get_Unterschied_mm(vorher_steuerwinkel_oeffnet.Value, nachher_steuerwinkel_oeffnet.Value, pleullaenge.Value, hubradius.Value, deachsierung.Value); //verbessern und durschnitt aus öffnen und schließen bildne
        //    }
        //}
    }
}