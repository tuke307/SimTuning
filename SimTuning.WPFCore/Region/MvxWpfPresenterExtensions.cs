using System;
using System.Linq;

namespace SimTuning.WPFCore.Region
{
    public static class MvxWpfPresenterExtensions
    {
        public static bool HasRegionAttribute(this Type view)
        {
            var attributes = view.GetCustomAttributes(typeof(MvxRegionPresentationAttribute), true);
            return attributes.Any();
        }

        public static string GetRegionName(this Type view)
        {
            var attributes = view.GetCustomAttributes(typeof(MvxRegionPresentationAttribute), true);

            if (!attributes.Any())
            {
                throw new InvalidOperationException("The MvxWpfView has no region attribute");
            }

            return ((MvxRegionPresentationAttribute)attributes.First()).RegionName;
        }
    }
}