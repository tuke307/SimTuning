using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace SimTuning.ViewModels.Tuning
{
    public class InputViewModel : BaseViewModel
    {
        public InputViewModel()
        {
            using(var db = new DatabaseContext())
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
    }
}
