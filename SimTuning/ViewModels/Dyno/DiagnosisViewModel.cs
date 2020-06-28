using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using OxyPlot;
using SimTuning.ModuleLogic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace SimTuning.ViewModels.Dyno
{
    public class DiagnosisViewModel : BaseViewModel
    {
        protected DynoLogic dynoLogic;

        public DiagnosisViewModel()
        {
            dynoLogic = new DynoLogic();

            using (var db = new DatabaseContext())
            {
                IList<VehiclesModel> vehicList = db.Vehicles.ToList();
                Vehicles = new ObservableCollection<VehiclesModel>(vehicList);

                IList<EnvironmentModel> environmList = db.Environment.ToList();
                Environments = new ObservableCollection<EnvironmentModel>(environmList);

                try
                {
                    Dyno = db.Dyno.Where(d => d.Active == true).Include(v => v.Audio).First();
                    NewEnvironment();
                }
                catch { }
            }
        }

        public ICommand RefreshPlotCommand { get; set; }
        public ICommand InsertVehicleCommand { get; set; }
        public ICommand InsertEnvironmentCommand { get; set; }
        public ICommand ShowSaveCommand { get; set; }

        protected virtual void RefreshPlot()
        {
            dynoLogic.CalculateStrengthPlot(Dyno, out List<DynoPSModel> ps, out List<DynoNmModel> nm);
            Dyno.DynoPS = ps;
            Dyno.DynoNm = nm;

            using (var db = new DatabaseContext())
            {
                //in Datenbank einfügen
                db.Dyno.Attach(Dyno);
                db.SaveChanges();
            }
        }

        public PlotModel PlotStrength
        {
            get { return dynoLogic.PlotStrength; }
            set => Set(value);
        }

        public DynoModel Dyno
        {
            get => Get<DynoModel>();
            set => Set(value);
        }

        public double? gesamtuebersetzung
        {
            get => Get<double?>();
            set => Set(value);
        }

        public double? gewicht
        {
            get => Get<double?>();
            set => Set(value);
        }

        public double? frontflaeche
        {
            get => Get<double?>();
            set => Set(value);
        }

        public double? cw_wert
        {
            get => Get<double?>();
            set => Set(value);
        }

        public double? temperatur
        {
            get => Get<double?>();
            set => Set(value);
        }

        public double? luftdruck
        {
            get => Get<double?>();
            set => Set(value);
        }

        public ObservableCollection<Data.Models.EnvironmentModel> Environments
        {
            get => Get<ObservableCollection<Data.Models.EnvironmentModel>>();
            set => Set(value);
        }

        public Data.Models.EnvironmentModel Environment
        {
            get => Get<Data.Models.EnvironmentModel>();
            set => Set(value);
        }

        public ObservableCollection<Data.Models.VehiclesModel> Vehicles
        {
            get => Get<ObservableCollection<Data.Models.VehiclesModel>>();
            set => Set(value);
        }

        public Data.Models.VehiclesModel Vehicle
        {
            get => Get<Data.Models.VehiclesModel>();
            set => Set(value);
        }

        public void InsertVehicle(object parameter)
        {
            if (Vehicle.Gewicht.HasValue)
                Dyno.Vehicle.Gewicht = Vehicle.Gewicht;

            if (Vehicle.Cw.HasValue)
                Dyno.Vehicle.Cw = Vehicle.Cw;

            if (Vehicle.FrontA.HasValue)
                Dyno.Vehicle.FrontA = Vehicle.FrontA;

            OnPropertyChanged("Dyno");
        }

        public void InsertEnvironment(object parameter)
        {
            if (Environment.LuftdruckP.HasValue)
                Dyno.Environment.LuftdruckP = Environment.LuftdruckP;

            if (Environment.TemperaturT.HasValue)
                Dyno.Environment.TemperaturT = Environment.TemperaturT;

            OnPropertyChanged("Dyno");
        }

        private void NewEnvironment()
        {
            if (Dyno.Environment == null)
            {
                Dyno.Environment = new EnvironmentModel()
                {
                    Name = "Automatisch erstellt " + DateTime.Now,
                };
                OnPropertyChanged("Dyno");
            }
        }
    }
}