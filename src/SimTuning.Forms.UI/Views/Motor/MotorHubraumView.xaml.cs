// Copyright (c) 2021 tuke productions. All rights reserved.
using MvvmCross;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using MvvmCross.ViewModels;
using SimTuning.Forms.UI.ViewModels.Motor;

namespace SimTuning.Forms.UI.Views.Motor
{
    [MvxContentPagePresentation]
    public partial class MotorHubraumView : MvxContentView<MotorHubraumViewModel>
    {
        public MotorHubraumView()
        {
            InitializeComponent();

            // aufgrund von sharpnado tabs muss viewmodel manuell geladen werden.
            if (!(ViewModel is MotorHubraumViewModel))
            {
                if (Mvx.IoCProvider.TryResolve<MotorHubraumViewModel>(out var viewModel))
                {
                    ViewModel = viewModel;
                    return;
                }

                var _viewModelLoader = Mvx.IoCProvider.Resolve<IMvxViewModelLoader>();
                var request = new MvxViewModelInstanceRequest(typeof(MotorHubraumViewModel));
                request.ViewModelInstance = _viewModelLoader.LoadViewModel(request, null);
                ViewModel = request.ViewModelInstance as MotorHubraumViewModel;

                Mvx.IoCProvider.RegisterSingleton<MotorHubraumViewModel>(ViewModel);
            }
        }
    }
}