//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace SimTuning.Forms.WPFCore
//{
//    public class CustomTabControlReloader : Dragablz.TabablzControl
//    {
//        private readonly IMvxViewsContainer _mvxViewsContainer;
//        private readonly IMvxViewModelLoader _mvxViewModelLoader;

// public CustomTabControlReloader() { SelectionChanged += TabControl_SelectionChanged;

// _mvxViewsContainer = Mvx.IoCProvider.Resolve<IMvxViewsContainer>(); _mvxViewModelLoader
// = Mvx.IoCProvider.Resolve<IMvxViewModelLoader>(); }

// private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e) {
// var selectedTab = (TabPage)SelectedItem;

//            if (selectedTab.ViewModelInstance != null && selectedTab.ViewModelInstance.ShouldReloadBeforeShow)
//                selectedTab.ViewModelInstance.ReloadModel();
//        }
//    }
//}