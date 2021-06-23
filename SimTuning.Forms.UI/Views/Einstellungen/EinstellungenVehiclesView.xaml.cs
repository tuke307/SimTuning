// project=SimTuning.Forms.UI, file=EinstellungenVehiclesView.xaml.cs, creation=2020:6:30
// Copyright (c) 2021 tuke productions. All rights reserved.
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using SimTuning.Forms.UI.ViewModels.Einstellungen;
using Xamarin.Forms;

namespace SimTuning.Forms.UI.Views.Einstellungen
{
    /// <summary>
    /// EinstellungenVehiclesView.
    /// </summary>
    /// <seealso cref="MvvmCross.Forms.Views.MvxContentPage{SimTuning.Forms.UI.ViewModels.Einstellungen.EinstellungenVehiclesViewModel}" />
    [MvxContentPagePresentation(WrapInNavigationPage = true, NoHistory = false)]
    public partial class EinstellungenVehiclesView : MvxContentPage<EinstellungenVehiclesViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EinstellungenVehiclesView" />
        /// class.
        /// </summary>
        public EinstellungenVehiclesView()
        {
            InitializeComponent();
        }
    }
}