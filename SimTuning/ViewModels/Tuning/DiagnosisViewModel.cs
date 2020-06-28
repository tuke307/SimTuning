using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using OxyPlot;
using SimTuning.ModuleLogic;
using System;
using System.Linq;

namespace SimTuning.ViewModels.Tuning
{
    public class DiagnosisViewModel : BaseViewModel
    {
        private readonly TuningLogic tunigLogic = new TuningLogic();

        public DiagnosisViewModel()
        {
            tunigLogic.DefinePlot();
            tunigLogic.OriginalSeries();
            //OnPropertyChanged("PlotTuning");

            Enable_Zoom = false;
            Enable_verschieben = false;
            Enable_Tracker = true;

            using (var db = new DatabaseContext())
            {
                try
                {
                    Tuning = LoadTuning(db.Tuning.Where(d => d.Active == true).First());
                }
                catch { }
            }
        }

        protected Data.Models.TuningModel LoadTuning(Data.Models.TuningModel tuning)
        {
            try
            {
                using (var Data = new Data.DatabaseContext())
                {
                    //Vehicle+Dyno laden
                    return Data.Tuning
                      .Where(v => v.Id == tuning.Id)
                      .Include(v => v.Vehicle)
                      .Include(v => v.Tuning)
                      .First();

                }
            }
            catch (Exception)
            {
                return tuning;
                throw;
            }
        }


        public TuningModel Tuning
        {
            get => Get<TuningModel>();
            set => Set(value);
        }

        public PlotModel PlotTuning
        {
            get { return tunigLogic.PlotTuning; }
            set => Set(value);
        }

        public PlotController PlotTuningController
        {
            get { return tunigLogic.PlotTuningController; }
            set => Set(value);
        }

        public bool Enable_Zoom
        {
            get => Get<bool>();
            set
            {
                Set(value);

                if (value == true) { tunigLogic.Controller_zoom_on(); }
                else { tunigLogic.Controller_zoom_off(); }
            }
        }

        public bool Enable_verschieben
        {
            get => Get<bool>();
            set
            {
                Set(value);

                if (value == true) { tunigLogic.Controller_pan_on(); }
                else { tunigLogic.Controller_pan_off(); }
            }
        }

        public bool Enable_Tracker
        {
            get => Get<bool>();
            set
            {
                Set(value);

                if (value == true) { tunigLogic.Controller_tracker_on(); }
                else { tunigLogic.Controller_tracker_off(); }
            }
        }
    }
}