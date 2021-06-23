// project=SimTuning.Core, file=InputViewModel.cs, creation=2020:7:31 Copyright (c) 2020
// tuke productions. All rights reserved.
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SimTuning.Core.ViewModels.Tuning
{
    public class InputViewModel : MvxNavigationViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InputViewModel" /> class.
        /// </summary>
        /// <param name="logFactory">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="messenger">The messenger.</param>
        public InputViewModel(ILoggerFactory logFactory, IMvxNavigationService navigationService, MvvmCross.Plugin.Messenger.IMvxMessenger messenger)
            : base(logFactory, navigationService)
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

        /// <summary>
        /// Prepares this instance. called after construction.
        /// </summary>

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns>Initilisierung.</returns>
        public override Task Initialize()
        {
            //messages

            return base.Initialize();
        }

        public override void Prepare()
        {
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