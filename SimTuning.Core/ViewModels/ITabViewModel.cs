using System;
using System.Collections.Generic;
using System.Text;

namespace SimTuning.Core.ViewModels
{
    /// <summary>
    /// ITabViewModel.
    /// </summary>
    public interface ITabViewModel
    {
        /// <summary>
        /// Gets a value indicating whether [should reload before show].
        /// </summary>
        /// <value>
        /// <c>true</c> if [should reload before show]; otherwise, <c>false</c>.
        /// </value>
        // public bool ShouldReloadBeforeShow { get; }

        /// <summary>
        /// Reloads the DB-data.
        /// </summary>
        void ReloadData();
    }
}