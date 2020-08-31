// project=SimTuning.Forms.WPFCore, file=EinstellungenMainView.xaml.cs, creation=2020:7:31
// Copyright (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.Forms.WPFCore.Views.Einstellungen
{
    using MvvmCross.Platforms.Wpf.Views;
    using SimTuning.Forms.WPFCore.Region;
    using SimTuning.Forms.WPFCore.ViewModels.Einstellungen;

    /// <summary>
    /// EinstellungenMainView.
    /// </summary>
    /// <seealso cref="MvvmCross.Platforms.Wpf.Views.MvxWpfView{SimTuning.Forms.WPFCore.ViewModels.Einstellungen.EinstellungenMainViewModel}" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    [MvxWpfPresenter("PageContent", mvxViewPosition.NewOrExsist)]
    public partial class EinstellungenMainView : MvxWpfView<EinstellungenMainViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EinstellungenMainView" /> class.
        /// </summary>
        public EinstellungenMainView()
        {
            InitializeComponent();
        }
    }
}