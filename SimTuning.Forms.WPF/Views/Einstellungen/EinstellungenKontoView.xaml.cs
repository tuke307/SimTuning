﻿using MvvmCross.Platforms.Wpf.Views;

namespace SimTuning.Forms.WPF.Views.Einstellungen
{
    public partial class EinstellungenKontoView : MvxWpfView/*<EinstellungenKontoViewModel>*/
    {
        public EinstellungenKontoView(/*MainWindowViewModel mainWindowViewModel*/)
        {
            InitializeComponent();

            //DataContext = new EinstellungenKontoViewModel(mainWindowViewModel);
        }
    }
}