// Copyright (c) 2021 tuke productions. All rights reserved.
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using SimTuning.Forms.UI.ViewModels.Einstellungen;

namespace SimTuning.Forms.UI.Views.Einstellungen
{
    /// <summary>
    /// EinstellungenKontoView.
    /// </summary>
    /// <seealso cref="MvvmCross.Forms.Views.MvxContentPage{SimTuning.Forms.UI.ViewModels.Einstellungen.EinstellungenKontoViewModel}" />
    [MvxContentPagePresentation(WrapInNavigationPage = true, NoHistory = false)]
    public partial class EinstellungenKontoView : MvxContentPage<EinstellungenKontoViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EinstellungenKontoView" /> class.
        /// </summary>
        public EinstellungenKontoView()
        {
            InitializeComponent();
        }
    }
}