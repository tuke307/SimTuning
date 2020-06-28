using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using SimTuning.ModuleLogic;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;

namespace SimTuning.ViewModels.Motor
{
    public class SteuerdiagrammViewModel : BaseViewModel
    {
        public SteuerdiagrammViewModel()
        {
            //Lokale liste kreieren
            MotorSteuerzeiten = new ObservableCollection<MotorModel>(){
                new MotorModel()
                {
                    Name = "sehr kurz",
                    Einlass = new EinlassModel() { SteuerzeitSZ = 100 },
                    Auslass = new AuslassModel() { SteuerzeitSZ = 125 },
                    Ueberstroemer = new UeberstroemerModel() { SteuerzeitSZ = 100 }
                },
                new MotorModel()
                {
                    Name = "kurz",
                    Einlass = new EinlassModel() { SteuerzeitSZ = 120 },
                    Auslass = new AuslassModel() { SteuerzeitSZ = 145 },
                    Ueberstroemer = new UeberstroemerModel() { SteuerzeitSZ = 110 }
                },
                new MotorModel()
                {
                    Name = "mittel",
                    Einlass = new EinlassModel() { SteuerzeitSZ = 140 },
                    Auslass = new AuslassModel() { SteuerzeitSZ = 165 },
                    Ueberstroemer = new UeberstroemerModel() { SteuerzeitSZ = 120 }
                },
                new MotorModel()
                {
                    Name = "lang",
                    Einlass = new EinlassModel() { SteuerzeitSZ = 160 },
                    Auslass = new AuslassModel() { SteuerzeitSZ = 185 },
                    Ueberstroemer = new UeberstroemerModel() { SteuerzeitSZ = 130 }
                },
                new MotorModel()
                {
                    Name = "sehr lang",
                    Einlass = new EinlassModel() { SteuerzeitSZ = 180 },
                    Auslass = new AuslassModel() { SteuerzeitSZ = 205 },
                    Ueberstroemer = new UeberstroemerModel() { SteuerzeitSZ = 140 }
                }
            };

            using (var db = new DatabaseContext())
            {
                IList<VehiclesModel> vehicList = db.Vehicles
                    .Include(vehicles => vehicles.Motor)
                    .Include(vehicles => vehicles.Motor.Einlass)
                    .Include(vehicles => vehicles.Motor.Auslass)
                    .Include(vehicles => vehicles.Motor.Ueberstroemer)
                    .ToList();

                Vehicles = new ObservableCollection<VehiclesModel>(vehicList);
            }
        }

        protected EngineLogic steuerzeit = new EngineLogic();

        public ObservableCollection<VehiclesModel> Vehicles
        {
            get => Get<ObservableCollection<VehiclesModel>>();
            set => Set(value);
        }

        public VehiclesModel Vehicle
        {
            get => Get<VehiclesModel>();
            set => Set(value);
        }

        public Data.Models.MotorModel MotorSteuerzeit
        {
            get => Get<Data.Models.MotorModel>();
            set => Set(value);
        }

        public ObservableCollection<Data.Models.MotorModel> MotorSteuerzeiten
        {
            get => Get<ObservableCollection<Data.Models.MotorModel>>();
            set => Set(value);
        }

        protected virtual Stream Refresh_Steuerzeit()
        {
            if (Einlass_Steuerzeit.HasValue)
            {
                Einlass_Steuerwinkel_oeffnen = steuerzeit.Steuerwinkel_oeffnet(Einlass_Steuerzeit.Value, 0, 0);
                Einlass_Steuerwinkel_schließen = steuerzeit.Steuerwinkel_schließt(Einlass_Steuerzeit.Value, 0, 0);
            }
            if (Auslass_Steuerzeit.HasValue)
            {
                Auslass_Steuerwinkel_oeffnen = steuerzeit.Steuerwinkel_oeffnet(0, Auslass_Steuerzeit.Value, 0);
                Auslass_Steuerwinkel_schließen = steuerzeit.Steuerwinkel_schließt(0, Auslass_Steuerzeit.Value, 0);
            }
            if (Ueberstroemer_Steuerzeit.HasValue)
            {
                Ueberstroemer_Steuerwinkel_oeffnen = steuerzeit.Steuerwinkel_oeffnet(0, 0, Ueberstroemer_Steuerzeit.Value);
                Ueberstroemer_Steuerwinkel_schließen = steuerzeit.Steuerwinkel_schließt(0, 0, Ueberstroemer_Steuerzeit.Value);
            }

            if (Ueberstroemer_Steuerzeit.HasValue && Auslass_Steuerzeit.HasValue)
            {
                Vorauslass_Steuerzeit = steuerzeit.Vorauslass(Auslass_Steuerzeit.Value, Ueberstroemer_Steuerzeit.Value);
            }

            if (Einlass_Steuerzeit.HasValue && Ueberstroemer_Steuerzeit.HasValue && Auslass_Steuerzeit.HasValue)
            {
                Stream stream = SimTuning.Business.Converts.SKBitmapToStream(steuerzeit.Steuerzeit_Rad(Einlass_Steuerzeit.Value, Auslass_Steuerzeit.Value, Ueberstroemer_Steuerzeit.Value));
                return stream;
            }
            else { return null; }
            
        }

        public virtual double? Einlass_Steuerzeit
        {
            get => Get<double?>();
            set => Set(value);
        }

        public virtual double? Auslass_Steuerzeit
        {
            get => Get<double?>();
            set => Set(value);
        }

        public virtual double? Ueberstroemer_Steuerzeit
        {
            get => Get<double?>();
            set => Set(value);
        }

        public double? Einlass_Steuerwinkel_oeffnen
        {
            get => Get<double?>();
            set => Set(value);
        }

        public double? Auslass_Steuerwinkel_oeffnen
        {
            get => Get<double?>();
            set => Set(value);
        }

        public double? Ueberstroemer_Steuerwinkel_oeffnen
        {
            get => Get<double?>();
            set => Set(value);
        }

        public double? Einlass_Steuerwinkel_schließen
        {
            get => Get<double?>();
            set => Set(value);
        }

        public double? Auslass_Steuerwinkel_schließen
        {
            get => Get<double?>();
            set => Set(value);
        }

        public double? Ueberstroemer_Steuerwinkel_schließen
        {
            get => Get<double?>();
            set => Set(value);
        }

        public double? Vorauslass_Steuerzeit
        {
            get => Get<double?>();
            set => Set(value);
        }

        //group box 2
        public ICommand InsertVehicleCommand { get; set; }

        protected void InsertVehicle(object parameter)
        {
            Einlass_Steuerzeit = Vehicle.Motor.Einlass.SteuerzeitSZ;
            Auslass_Steuerzeit = Vehicle.Motor.Auslass.SteuerzeitSZ;
            Ueberstroemer_Steuerzeit = Vehicle.Motor.Ueberstroemer.SteuerzeitSZ;
        }

        //Group box 3
        public ICommand InsertReferenceCommand { get; set; }

        protected void InsertReference(object parameter)
        {
            Einlass_Steuerzeit = MotorSteuerzeit.Einlass.SteuerzeitSZ;
            Auslass_Steuerzeit = MotorSteuerzeit.Auslass.SteuerzeitSZ;
            Ueberstroemer_Steuerzeit = MotorSteuerzeit.Ueberstroemer.SteuerzeitSZ;
        }
    }
}