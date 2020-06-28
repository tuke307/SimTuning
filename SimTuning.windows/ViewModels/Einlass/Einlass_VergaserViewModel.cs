using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using SimTuning.ModuleLogic;
using SimTuning.windows.Business;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace SimTuning.windows.ViewModels.Einlass
{
    public class Einlass_VergaserViewModel : SimTuning.ViewModels.Einlass.VergaserViewModel
    {
        //private EinlassLogic einlass = new EinlassLogic();
        //public ObservableCollection<UnitListItem> LengthQuantityUnits { get; }
        //public ObservableCollection<UnitListItem> VolumeQuantityUnits { get; }

        public Einlass_VergaserViewModel()
        {
            InsertDataCommand = new ActionCommand(InsertData);

            //using (var db = new DatabaseContext())
            //{
            //    IList<VehiclesModel> vehicList = db.Vehicles
            //        .Include(vehicles => vehicles.Motor)
            //        .ToList();

            //    Vehicles = new ObservableCollection<VehiclesModel>(vehicList);
            //}
        }

        //public ICommand InsertDataCommand { get; set; }

        //private void InsertData(object obj)
        //{
        //    Hubvolumen = Vehicle.Motor.HubraumV;
        //    Resonanzdrehzahl = Vehicle.Motor.ResonanzU;
        //}

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

        //private void Refresh_Vergasergroeße()
        //{
        //    if (Hubvolumen.HasValue && Resonanzdrehzahl.HasValue)
        //    {
        //        Vergasergroeße = einlass.Get_Vergasergroeße(Hubvolumen.Value, Resonanzdrehzahl.Value);
        //    }
        //}

        //private void Refresh_Hauptduesendurchmesser()
        //{
        //    if (Vergasergroeße.HasValue)
        //    {
        //        Hauptduesendurchmesser = einlass.Get_Hauptduesendurchmesser(Vergasergroeße.Value);
        //    }
        //}

        //public double? Hauptduesendurchmesser
        //{
        //    get => Get<double?>();
        //    set => Set(value);
        //}

        //public double? Resonanzdrehzahl
        //{
        //    get => Get<double?>();
        //    set { Set(value); Refresh_Vergasergroeße(); }
        //}

        //public double? Hubvolumen
        //{
        //    get => Get<double?>();
        //    set { Set(value); Refresh_Vergasergroeße(); }
        //}

        //public double? Vergasergroeße
        //{
        //    get => Get<double?>();
        //    set { Set(value); Refresh_Hauptduesendurchmesser(); }
        //}
    }
}