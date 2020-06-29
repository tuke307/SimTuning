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
    public class EinlassVergaserViewModel : SimTuning.ViewModels.Einlass.VergaserViewModel
    {
        public EinlassVergaserViewModel()
        {
            InsertDataCommand = new ActionCommand(InsertData);
        }
    }
}