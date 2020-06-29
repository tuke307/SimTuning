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
        public Einlass_VergaserViewModel()
        {
            InsertDataCommand = new ActionCommand(InsertData);
        }
    }
}