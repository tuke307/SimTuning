// Copyright (c) 2021 tuke productions. All rights reserved.
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using MvvmCross.ViewModels;
using SimTuning.Data;
using SimTuning.Data.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SimTuning.Core.ViewModels.Tuning
{
    /// <summary>
    /// InputViewModel.
    /// </summary>
    /// <seealso cref="MvvmCross.ViewModels.MvxViewModel" />
    public class InputViewModel : MvxViewModel
    {
        /// <summary> Initializes a new instance of the <see cref="InputViewModel"/>
        /// class. </summary> <param name="logger"><inheritdoc cref="ILogger"
        /// path="/summary/node()" /></param> <param name="navigationService"><inheritdoc
        /// cref="IMvxNavigationService" path="/summary/node()" /></param <param
        /// name="messenger"><inheritdoc cref="IMvxMessenger" path="/summary/node()"
        /// /></param>
        public InputViewModel(
            ILogger<InputViewModel> logger,
            IMvxNavigationService navigationService,
            IMvxMessenger messenger)
        {
            this._logger = logger;
            this._messenger = messenger;
        }

        #region Commands

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns>Initilisierung.</returns>
        public override Task Initialize()
        {
            using (var db = new DatabaseContext())
            {
                try
                {
                    Tuning = LoadTuning(db.Tuning.Where(d => d.Active == true).First());
                }
                catch { }
            }

            return base.Initialize();
        }

        /// <summary>
        /// Prepares this instance. called after construction.
        /// </summary>
        public override void Prepare()
        {
        }

        protected Data.Models.TuningModel LoadTuning(Data.Models.TuningModel tuning)
        {
            try
            {
                using (var data = new Data.DatabaseContext())
                {
                    // Vehicle+Dyno laden
                    return data.Tuning
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

        protected readonly IMvxMessenger _messenger;
        private readonly ILogger<InputViewModel> _logger;
        private TuningModel _tuning;

        public TuningModel Tuning
        {
            get => _tuning;
            set { SetProperty(ref _tuning, value); }
        }

        #endregion Values
    }
}