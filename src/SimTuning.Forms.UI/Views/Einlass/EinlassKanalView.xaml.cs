// Copyright (c) 2021 tuke productions. All rights reserved.
using MvvmCross;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using MvvmCross.ViewModels;
using SimTuning.Forms.UI.ViewModels.Einlass;

namespace SimTuning.Forms.UI.Views.Einlass
{
    [MvxContentPagePresentation]
    public partial class EinlassKanalView : MvxContentView<EinlassKanalViewModel>
    {
        public EinlassKanalView()
        {
            InitializeComponent();

            // aufgrund von sharpnado tabs muss viewmodel manuell geladen werden.
            if (!(ViewModel is EinlassKanalViewModel))
            {
                if (Mvx.IoCProvider.TryResolve<EinlassKanalViewModel>(out var viewModel))
                {
                    ViewModel = viewModel;
                    return;
                }

                var _viewModelLoader = Mvx.IoCProvider.Resolve<IMvxViewModelLoader>();
                var request = new MvxViewModelInstanceRequest(typeof(EinlassKanalViewModel));
                request.ViewModelInstance = _viewModelLoader.LoadViewModel(request, null);
                ViewModel = request.ViewModelInstance as EinlassKanalViewModel;

                Mvx.IoCProvider.RegisterSingleton<EinlassKanalViewModel>(ViewModel);
            }
        }
    }
}