using MvvmCross.Presenters.Attributes;

namespace SimTuning.WPFCore.Region
{
    // MvvmCross / MvvmCross / Windows / WindowsUWP / Views / MvxRegionAttribute.cs
    // [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class MvxRegionPresentationAttribute : MvxBasePresentationAttribute
    {
        //public MvxRegionPresentationAttribute(string regionName)
        //{
        //    this.Name = regionName;
        //}

        //public string Name { get; private set; }

        public string RegionName { get; set; }
        public string WindowIdentifier { get; set; }
    }
}