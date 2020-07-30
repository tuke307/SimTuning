using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MvvmCross;
using MvvmCross.Platforms.Wpf.Presenters;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.ViewModels;
using MvvmCross.Views;

namespace SimTuning.Forms.WPFCore.Region
{
    public class MvxMultiRegionWpfViewPresenter : MvxWpfViewPresenter
    {
        private readonly ContentControl Root;

        public MvxMultiRegionWpfViewPresenter(ContentControl root) : base(root) => Root = root;

        public override Task<bool> Show(MvxViewModelRequest request)
        {
            var viewType = GetViewType(request);

            if (viewType.HasRegionAttribute())
            {
                var loader = Mvx.IoCProvider.Resolve<IMvxWpfViewLoader>();
                var view = loader.CreateView(request);

                var containerView = FindChild<Frame>(Root, viewType.GetRegionName());

                if (containerView != null)
                {
                    containerView.Navigate(view);
                    return Task.FromResult(false);
                }
            }

            base.Show(request);

            return Task.FromResult(false);
        }

        private static Type GetViewType(MvxViewModelRequest request)
        {
            var viewFinder = Mvx.IoCProvider.Resolve<IMvxViewsContainer>();
            return viewFinder.GetViewType(request.ViewModelType);
        }

        public override Task<bool> ChangePresentation(MvxPresentationHint hint)
        {
            // deal with popToRoot
            base.ChangePresentation(hint);

            return Task.FromResult(false);
        }

        // Implementation from: http://stackoverflow.com/a/1759923/80186
        internal static T FindChild<T>(DependencyObject reference, string childName) where T : DependencyObject
        {
            // Confirm parent and childName are valid.
            if (reference == null) return null;

            var foundChild = default(T);
            var nextPhase = new List<DependencyObject>();

            var childrenCount = VisualTreeHelper.GetChildrenCount(reference);
            for (var index = 0; index < childrenCount; index++)
            {
                var child = VisualTreeHelper.GetChild(reference, index);

                // If the child is not of the request child type child
                T childType = child as T;
                if (childType == null)
                {
                    // recursively drill down the tree
                    foundChild = FindChild<T>(child, childName);

                    // If the child is found, break so we do not overwrite the found child.
                    if (foundChild != null) break;
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    var frameworkElement = child as FrameworkElement;
                    // If the child's name is set for search
                    if (frameworkElement != null && frameworkElement.Name == childName)
                    {
                        // if the child's name is of the request name
                        foundChild = (T)child;
                        break;
                    }
                    else
                    {
                        // keep for searching inside this frame
                        nextPhase.Add(child);
                    }
                }
                else
                {
                    // child element found.
                    foundChild = (T)child;
                    break;
                }
            }

            // if failed to find the child, search inside the frames we found
            if (foundChild == null)
            {
                foreach (var item in nextPhase)
                {
                    // recursively drill down the tree
                    foundChild = FindChild<T>(item, childName);

                    // If the child is found, break so we do not overwrite the found child.
                    if (foundChild != null) break;
                }
            }

            return foundChild;
        }
    }
}