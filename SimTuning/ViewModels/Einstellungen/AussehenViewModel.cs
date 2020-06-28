using Data;
using System.Linq;
using System.Windows.Input;

namespace SimTuning.ViewModels.Einstellungen
{
    public class AussehenViewModel : BaseViewModel
    {
        public AussehenViewModel()
        {
            using (var db = new DatabaseContext())
                ToogleDarkmode = db.Settings.ToList().Last().DarkMode;
        }

        public ICommand ApplyPrimaryCommand { get; set; }
        public ICommand ApplyAccentCommand { get; set; }

        protected virtual void ApplyPrimary()
        {
            //color.SetPrimary(parameter);
        }

        protected virtual void ApplyAccent()
        {
            //color.SetAccent(parameter);
        }

        protected virtual void ApplyBaseTheme()
        {
            //color.SetBaseTheme(ToogleDarkmode);
        }

        public bool ToogleDarkmode
        {
            get => Get<bool>();
            set
            {
                Set(value);
                ApplyBaseTheme();
            }
        }
    }
}