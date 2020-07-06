using MvvmCross.ViewModels;
using System.Threading.Tasks;

namespace SimTuning.Core.ViewModels
{
    public class Menu : MvxViewModel
    {
        public Menu()
        {
        }

        public override void Prepare()
        {
            // This is the first method to be called after construction
        }

        public override Task Initialize()
        {
            // Async initialization

            return base.Initialize();
        }
    }
}