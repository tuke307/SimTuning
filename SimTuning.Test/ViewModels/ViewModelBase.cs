// project=SimTuning.Test, file=ViewModelBase.cs, creation=2021:6:23 Copyright (c) 2021
// tuke productions. All rights reserved.
using MvvmCross.Commands;
using MvvmCross.Core;
using MvvmCross.Navigation;
using MvvmCross.Tests;
using NUnit.Framework;

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
            //navigationService.LoadRoutes(new[] { typeof(WPF.UI.ViewModels.MenuViewModel).Assembly });
            Ioc.RegisterSingleton<IMvxNavigationService>(navigationService);

            // for commands
            var commandHelper = new MvxUnitTestCommandHelper();
            Ioc.RegisterSingleton<IMvxCommandHelper>(commandHelper);
        }
    }
}