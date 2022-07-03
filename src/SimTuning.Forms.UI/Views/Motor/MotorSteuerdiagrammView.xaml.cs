// Copyright (c) 2021 tuke productions. All rights reserved.
using MvvmCross;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using MvvmCross.ViewModels;
using SimTuning.Forms.UI.ViewModels.Motor;

namespace SimTuning.Forms.UI.Views.Motor
{
    [MvxContentPagePresentation]
    public partial class MotorSteuerdiagrammView : MvxContentView<MotorSteuerdiagrammViewModel>
    {
        public MotorSteuerdiagrammView()
        {
            InitializeComponent();

            // aufgrund von sharpnado tabs muss viewmodel manuell geladen werden.
            if (!(ViewModel is MotorSteuerdiagrammViewModel))
            {
                if (Mvx.IoCProvider.TryResolve<MotorSteuerdiagrammViewModel>(out var viewModel))
                {
                    ViewModel = viewModel;
                    return;
                }

                var _viewModelLoader = Mvx.IoCProvider.Resolve<IMvxViewModelLoader>();
                var request = new MvxViewModelInstanceRequest(typeof(MotorSteuerdiagrammViewModel));
                request.ViewModelInstance = _viewModelLoader.LoadViewModel(request, null);
                ViewModel = request.ViewModelInstance as MotorSteuerdiagrammViewModel;

                Mvx.IoCProvider.RegisterSingleton<MotorSteuerdiagrammViewModel>(ViewModel);
            }
        }
    }
}