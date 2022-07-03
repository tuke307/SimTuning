// Copyright (c) 2021 tuke productions. All rights reserved.
using MvvmCross;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using MvvmCross.ViewModels;
using SimTuning.Forms.UI.ViewModels.Motor;

namespace SimTuning.Forms.UI.Views.Motor
{
    [MvxContentPagePresentation]
    public partial class MotorVerdichtungView : MvxContentView<MotorVerdichtungViewModel>
    {
        public MotorVerdichtungView()
        {
            InitializeComponent();

            // aufgrund von sharpnado tabs muss viewmodel manuell geladen werden.
            if (!(ViewModel is MotorVerdichtungViewModel))
            {
                if (Mvx.IoCProvider.TryResolve<MotorVerdichtungViewModel>(out var viewModel))
                {
                    ViewModel = viewModel;
                    return;
                }

                var _viewModelLoader = Mvx.IoCProvider.Resolve<IMvxViewModelLoader>();
                var request = new MvxViewModelInstanceRequest(typeof(MotorVerdichtungViewModel));
                request.ViewModelInstance = _viewModelLoader.LoadViewModel(request, null);
                ViewModel = request.ViewModelInstance as MotorVerdichtungViewModel;

                Mvx.IoCProvider.RegisterSingleton<MotorVerdichtungViewModel>(ViewModel);
            }
        }
    }
}