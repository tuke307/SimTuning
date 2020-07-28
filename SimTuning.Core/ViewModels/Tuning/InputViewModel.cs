using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading.Tasks;

namespace SimTuning.Core.ViewModels.Tuning
{
    public class InputViewModel : MvxNavigationViewModel
    {
        protected ResourceManager rm;

        public InputViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            using (var db = new DatabaseContext())
            {
                try
                {
                    Tuning = LoadTuning(db.Tuning.Where(d => d.Active == true).First());
                }
                catch { }
            }
        }

        public override void Prepare()
        {
            // This is the first method to be called after construction
        }

        public override Task Initialize()
        {
            // Async initialization

            //messages
            this.rm = new ResourceManager(typeof(SimTuning.Core.resources));

            return base.Initialize();
        }

        #region Commands

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

        #endregion Commands

        #region Values

        private TuningModel _tuning;

        public TuningModel Tuning
        {
            get => _tuning;
            set { SetProperty(ref _tuning, value); }
        }

        #endregion Values
    }
}