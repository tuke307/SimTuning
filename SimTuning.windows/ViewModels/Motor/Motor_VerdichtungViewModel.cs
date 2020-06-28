using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using SimTuning.ModuleLogic;
using SimTuning.windows.Business;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace SimTuning.windows.ViewModels.Motor
{
    public class Motor_VerdichtungViewModel : SimTuning.ViewModels.Motor.VerdichtungViewModel
    {
        //private EngineLogic verdichtung;

        public Motor_VerdichtungViewModel()
        {
            InsertDataCommand = new ActionCommand(InsertData);
            //verdichtung = new EngineLogic();

            //Zielverdichtungen = new List<double?>() { 8, 8.5, 9, 9.5, 10, 10.5, 11, 11.5, 12 };
            //Zielverdichtung = Zielverdichtungen[5];

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

        //private void InsertData(object parameter)
        //{
        //    hubraum = Vehicle.Motor.HubraumV;
        //    brennraum = Vehicle.Motor.BrennraumV;
        //    durchmesser = Vehicle.Motor.Bohrung;
        //}

        //private void Refresh_verdichtung()
        //{
        //    if (hubraum.HasValue && brennraum.HasValue && durchmesser.HasValue)
        //    {
        //        derzeitige_verdichtung = verdichtung.Get_Verdichtung(hubraum.Value, brennraum.Value, durchmesser.Value);
        //    }
        //}

        //private void Refresh_zielverdichtung()
        //{
        //    if (hubraum.HasValue && brennraum.HasValue && durchmesser.HasValue && Zielverdichtung != 0)
        //    {
        //        abdrehen_mm = verdichtung.Get_Abdrehen_mm(hubraum.Value, brennraum.Value, durchmesser.Value, Zielverdichtung.Value);
        //    }
        //}

        //public double? hubraum
        //{
        //    get => Get<double?>();
        //    set { Set(value); Refresh_verdichtung(); }
        //}

        //public double? brennraum
        //{
        //    get => Get<double?>();
        //    set { Set(value); Refresh_verdichtung(); }
        //}

        //public double? durchmesser
        //{
        //    get => Get<double?>();
        //    set { Set(value); Refresh_verdichtung(); }
        //}

        //public double? derzeitige_verdichtung
        //{
        //    get => Get<double?>();
        //    set => Set(value);
        //}

        //public double? abdrehen_mm
        //{
        //    get => Get<double?>();
        //    set => Set(value);
        //}

        //public List<double?> Zielverdichtungen
        //{
        //    get => Get<List<double?>>();
        //    set { Set(value); Refresh_zielverdichtung(); }
        //}

        //public double? Zielverdichtung
        //{
        //    get => Get<double?>();
        //    set { Set(value); Refresh_zielverdichtung(); }
        //}
    }
}