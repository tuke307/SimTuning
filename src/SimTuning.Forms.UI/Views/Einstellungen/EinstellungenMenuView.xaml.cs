// Copyright (c) 2021 tuke productions. All rights reserved.
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using SimTuning.Forms.UI.ViewModels.Einstellungen;

namespace SimTuning.Forms.UI.Views.Einstellungen
{
    /// <summary>
    /// EinstellungenMenuView.
    /// </summary>
    /// <seealso cref="MvvmCross.Forms.Views.MvxContentPage{SimTuning.Forms.UI.ViewModels.Einstellungen.EinstellungenMenuViewModel}" />
    [MvxContentPagePresentation(WrapInNavigationPage = true, NoHistory = false)]
    public partial class EinstellungenMenuView : MvxContentPage<EinstellungenMenuViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EinstellungenMenuView" /> class.
        /// </summary>
        public EinstellungenMenuView()
        {
            InitializeComponent();
        }
    }
}