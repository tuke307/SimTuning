using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MvvmCross;
using MvvmCross.Platforms.Wpf.Presenters;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.Presenters;
using MvvmCross.ViewModels;
using MvvmCross.Views;

namespace SimTuning.WPFCore.Region
{
    public class CustomViewPresenter : MvxWpfViewPresenter/*MvxMultiRegionWpfViewPresenter*/
    {
        private ContentControl _contentControl;

        public CustomViewPresenter(ContentControl contentControl) : base(contentControl)
        {
            _contentControl = contentControl;
        }

        protected virtual Task<bool> ShowRegionContentView(FrameworkElement element, MvxRegionPresentationAttribute attribute, MvxViewModelRequest request)
        {
            //var contentControl = FrameworkElementsDictionary.Keys.FirstOrDefault(w => (w as MvxWindow)?.Identifier == attribute.WindowIdentifier) ?? FrameworkElementsDictionary.Keys.Last();
            var viewFinder = Mvx.IoCProvider.Resolve<IMvxViewsContainer>();
            var viewType = viewFinder.GetViewType(request.ViewModelType);
            if (viewType.HasRegionAttribute())
            {
                var loader = Mvx.IoCProvider.Resolve<IMvxWpfViewLoader>();
                var view = loader.CreateView(request);

                var region = viewType.GetRegionName();

                var containerView = LogicalTreeHelper.FindLogicalNode(_contentControl, region) as Frame;
                if (containerView != null)
                {
                    containerView.Navigate(view);
                    return Task.FromResult(true);
                }
            }
            return Task.FromResult(false);
        }

        //public override Task<bool> ChangePresentation(MvxPresentationHint hint)
        //{
        //    if (hint is MvxPanelPopToRootPresentationHint)
        //    {
        //        var mainView = _contentControl.Content as MainView;
        //        if (mainView != null)
        //        {
        //            mainView.PopToRoot();
        //        }
        //    }

        //    base.ChangePresentation(hint);

        //    return Task.FromResult(false);
        //}

        public override void RegisterAttributeTypes()
        {
            //AttributeTypesToActionsDictionary.Add(
            // typeof(MvxRegionPresentationAttribute),
            // new MvxRegionPresentationAttribute
            // {
            //     ShowAction = (viewType, attribute, request) =>
            //     {
            //         var view = WpfViewLoader.CreateView(request);
            //         ShowWindow(view, (MyCustomModePresentationAttribute)attribute, request);
            //     },
            //     CloseAction = (viewModel, attribute) => CloseWindow(viewModel)
            // });

            AttributeTypesToActionsDictionary.Register<MvxRegionPresentationAttribute>(
                   (viewType, attribute, request) =>
                   {
                       var view = WpfViewLoader.CreateView(request);
                       return ShowRegionContentView(view, (MvxRegionPresentationAttribute)attribute, request);
                   },
                   (viewModel, attribute) => CloseContentView(viewModel));
        }
    }
}