using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Presenters.Attributes;

namespace SimTuning.WPFCore.Region
{
    // MvvmCross / MvvmCross / Windows / WindowsUWP / Views / MvxRegionAttribute.cs
    // [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class MvxRegionPresentationAttribute : MvxBasePresentationAttribute
    {
        //public string RegionName { get; private set; }

        //public MvxRegionPresentationAttribute(string regionName)
        //{
        //    RegionName = regionName;
        //}

        public string RegionName { get; set; }
        public string WindowIdentifier { get; set; }
    }
}