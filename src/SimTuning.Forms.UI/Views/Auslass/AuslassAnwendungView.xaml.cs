// Copyright (c) 2021 tuke productions. All rights reserved.
using MvvmCross;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using MvvmCross.ViewModels;
using SimTuning.Forms.UI.ViewModels.Auslass;

namespace SimTuning.Forms.UI.Views.Auslass
{
    [MvxContentPagePresentation]
    public partial class AuslassAnwendungView : MvxContentView<AuslassAnwendungViewModel>
    {
        public AuslassAnwendungView()
        {
            InitializeComponent();

            // aufgrund von sharpnado tabs muss viewmodel manuell geladen werden.
            if (!(ViewModel is AuslassAnwendungViewModel))
            {
                if (Mvx.IoCProvider.TryResolve<AuslassAnwendungViewModel>(out var viewModel))
                {
                    ViewModel = viewModel;
                    return;
                }

                var _viewModelLoader = Mvx.IoCProvider.Resolve<IMvxViewModelLoader>();
                var request = new MvxViewModelInstanceRequest(typeof(AuslassAnwendungViewModel));
                request.ViewModelInstance = _viewModelLoader.LoadViewModel(request, null);
                ViewModel = request.ViewModelInstance as AuslassAnwendungViewModel;

                Mvx.IoCProvider.RegisterSingleton<AuslassAnwendungViewModel>(ViewModel);
            }
        }
    }
}