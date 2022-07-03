// Copyright (c) 2021 tuke productions. All rights reserved.
using MvvmCross;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using MvvmCross.ViewModels;
using SimTuning.Forms.UI.ViewModels.Motor;

namespace SimTuning.Forms.UI.Views.Motor
{
    [MvxContentPagePresentation]
    public partial class MotorUmrechnungenView : MvxContentView<MotorUmrechnungenViewModel>
    {
        public MotorUmrechnungenView()
        {
            InitializeComponent();

            // aufgrund von sharpnado tabs muss viewmodel manuell geladen werden.
            if (!(ViewModel is MotorUmrechnungenViewModel))
            {
                if (Mvx.IoCProvider.TryResolve<MotorUmrechnungenViewModel>(out var viewModel))
                {
                    ViewModel = viewModel;
                    return;
                }

                var _viewModelLoader = Mvx.IoCProvider.Resolve<IMvxViewModelLoader>();
                var request = new MvxViewModelInstanceRequest(typeof(MotorUmrechnungenViewModel));
                request.ViewModelInstance = _viewModelLoader.LoadViewModel(request, null);
                ViewModel = request.ViewModelInstance as MotorUmrechnungenViewModel;

                Mvx.IoCProvider.RegisterSingleton<MotorUmrechnungenViewModel>(ViewModel);
            }
        }
    }
}