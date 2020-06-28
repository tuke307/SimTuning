namespace SimTuning.mobile.ViewModels.Motor
{
    public class Motor_HubraumViewModel : SimTuning.ViewModels.Motor.HubraumViewModel
    {
        //private readonly EngineLogic hubraum;

        public Motor_HubraumViewModel()
        {
            //hubraum = new EngineLogic();

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

            //Einbauspiele = new List<double>() { 3, 3.5, 4 };
        }

        //public ObservableCollection<VehiclesModel> Vehicles
        //{
        //    get => Get<ObservableCollection<VehiclesModel>>();
        //    set => Set(value);
        //}

        //public VehiclesModel Vehicle
        //{
        //    get => Get<VehiclesModel>();
        //    set
        //    {
        //        Set(value);

        //        if (value.Motor.Bohrung.HasValue)
        //            GrindingDiameters = hubraum.GetGrindingDiameters(value.Motor.Bohrung.Value);
        //    }
        //}

        //public GrindingDiametersModel GrindingDiameters
        //{
        //    get => Get<GrindingDiametersModel>();
        //    set => Set(value);
        //}

        //public double? Einbauspiel
        //{
        //    get => Get<double?>();
        //    set { Set(value); Refresh_all(); }
        //}

        //public List<double> Einbauspiele
        //{
        //    get => Get<List<double>>();
        //    set => Set(value);
        //}

        //public double? kolben_durchmesser
        //{
        //    get => Get<double?>();
        //    set => Set(value);
        //}

        //public double? bohrung_durchmesser
        //{
        //    get => Get<double?>();
        //    set => Set(value);
        //}

        //public double? hub
        //{
        //    get => Get<double?>();
        //    set { Set(value); Refresh_all(); }
        //}

        //public double? HubraumVolumen
        //{
        //    get => Get<double?>();
        //    set { Set(value); Refresh_all(); }
        //}

        //private void Refresh_all()
        //{
        //    if (hub.HasValue && HubraumVolumen.HasValue)
        //    {
        //        if (!Einbauspiel.HasValue)
        //            Einbauspiel = 0.03;

        //        bohrung_durchmesser = hubraum.Get_BohrungsDurchmesser(HubraumVolumen.Value, hub.Value);
        //        kolben_durchmesser = hubraum.Get_KolbenDurchmesser(bohrung_durchmesser.Value, Einbauspiel.Value);
        //    }
        //}
    }
}