using System;

namespace SimTuning.mobile.LazyLoading
{
    internal interface IActiveAware
    {
        bool IsActive { get; set; }

        event EventHandler IsActiveChanged;
    }
}