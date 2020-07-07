using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System.Threading.Tasks;

namespace SimTuning.Core.ViewModels.Demo
{
    public class DemoMainViewModel : MvxViewModel
    {
        public DemoMainViewModel()
        {
        }

        public IMvxCommand OpenWebsiteCommand { get; set; }

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

        protected virtual void OpenWebsite()
        {
            //Business.Functions.GoToSite("https://www.tuke-productions.de");
        }

        #endregion Commands
    }
}