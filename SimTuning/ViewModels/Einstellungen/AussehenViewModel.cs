using Data;
using MvvmCross.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimTuning.ViewModels.Einstellungen
{
    public class AussehenViewModel : MvxViewModel
    {
        public AussehenViewModel()
        {
            using (var db = new DatabaseContext())
                ToogleDarkmode = db.Settings.ToList().Last().DarkMode;
        }

        public ICommand ApplyPrimaryCommand { get; set; }
        public ICommand ApplyAccentCommand { get; set; }

        public override void Prepare()
        {
            // This is the first method to be called after construction
        }

        public override Task Initialize()
        {
            // Async initialization

            return base.Initialize();
        }

        #region Commands

        protected virtual void ApplyPrimary()
        {
        }

        protected virtual void ApplyAccent()
        {
        }

        protected virtual void ApplyBaseTheme()
        {
        }

        #endregion Commands

        #region Values

        private bool _toogleDarkmode;

        public bool ToogleDarkmode
        {
            get => _toogleDarkmode;
            set
            {
                SetProperty(ref _toogleDarkmode, value);
                ApplyBaseTheme();
            }
        }

        #endregion Values
    }
}