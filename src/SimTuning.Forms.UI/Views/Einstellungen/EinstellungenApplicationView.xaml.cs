// project=SimTuning.Forms.UI, file=EinstellungenApplicationView.xaml.cs,
// creation=2020:10:19 Copyright (c) 2021 tuke productions. All rights reserved.
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using SimTuning.Forms.UI.ViewModels.Einstellungen;

namespace SimTuning.Forms.UI.Views.Einstellungen
{
    /// <summary>
    /// EinstellungenApplicationView.
    /// </summary>
    /// <seealso cref="MvvmCross.Forms.Views.MvxContentPage{SimTuning.Forms.UI.ViewModels.Einstellungen.EinstellungenApplicationViewModel}" />
    [MvxContentPagePresentation(WrapInNavigationPage = true, NoHistory = false)]
    public partial class EinstellungenApplicationView : MvxContentPage<EinstellungenApplicationViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EinstellungenApplicationView" />
        /// class.
        /// </summary>
        public EinstellungenApplicationView()
        {
            InitializeComponent();
        }
    }
}