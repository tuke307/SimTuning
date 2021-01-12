using MvvmCross.Commands;
using MvvmCross.Core;
using MvvmCross.Navigation;
using MvvmCross.Tests;

namespace SimTuning.Test
{
    public class ViewModelBase : MvxTest
    {
        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();

            // for navigation parsing
            Ioc.RegisterSingleton<IMvxStringToTypeParser>(new MvxStringToTypeParser());

            // for navigation
            MvxNavigationService navigationService = new MvxNavigationService(null, null)
            {
                ViewDispatcher = MockDispatcher,
            }
            ;
            navigationService.LoadRoutes(new[] { typeof(WPF.UI.ViewModels.MenuViewModel).Assembly });
            Ioc.RegisterSingleton<IMvxNavigationService>(navigationService);

            // for commands
            var commandHelper = new MvxUnitTestCommandHelper();
            Ioc.RegisterSingleton<IMvxCommandHelper>(commandHelper);

            // for theme
            //var themeService = new WPF.UI.Services.ThemeService();
            //Ioc.RegisterSingleton<WPF.UI.Services.IThemeService>(themeService);
        }
    }
}