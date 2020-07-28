using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MvvmCross;
using MvvmCross.Platforms.Wpf.Presenters;
using MvvmCross.Platforms.Wpf.Presenters.Attributes;
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

        protected virtual Task<bool> ShowRegionContentView(MvxRegionPresentationAttribute attribute, MvxViewModelRequest request)
        {
            //_contentControl = FrameworkElementsDictionary.Keys.FirstOrDefault(w => (w as MvxWindow)?.Identifier == attribute.WindowIdentifier) ?? FrameworkElementsDictionary.Keys.Last();
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
                    var result = containerView.Navigate(view);
                    return Task.FromResult(result);
                }
            }
            return Task.FromResult(false);
        }

        //protected virtual void ShowViewWithNoContainer(MvxViewModelRequest request, MvxContentPresentationAttribute attribute)
        //{
        //    var view = WpfViewLoader.CreateView(request);
        //    base.ShowContentView(view, new MvxContentPresentationAttribute(), request);
        //}

        public override void RegisterAttributeTypes()
        {
            AttributeTypesToActionsDictionary.Add(
                typeof(MvxRegionPresentationAttribute),
                new MvxPresentationAttributeAction()
                {
                    ShowAction = (viewType, attribute, request) => ShowRegionContentView((MvxRegionPresentationAttribute)attribute, request),
                    CloseAction = (viewModel, attribute) => CloseContentView(viewModel)
                });

            base.RegisterAttributeTypes();
        }
    }
}