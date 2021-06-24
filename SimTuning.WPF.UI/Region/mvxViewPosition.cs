// project=SimTuning.WPF.UI, file=mvxViewPosition.cs, creation=2020:9:2 Copyright (c) 2021
// tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.Region
{
    /// <summary>
    /// View position.
    /// </summary>
    public enum mvxViewPosition
    {
        /// <summary>
        /// Your view will be displayed in a new holder always.
        /// </summary>
        New,

        /// <summary>
        /// Your view will be displayed in the active holder within the container, if the
        /// container is not a Selector the view will be displayed in the last holder, and
        /// if you do not have any holders inside your container, the view will be
        /// displayed in a new holder.
        /// </summary>
        NewOrExsist,

        /// <summary>
        /// The presenter will search for any matching visible view, and if it found one
        /// it will bring its holder to the top, otherwise it will display the view in a
        /// new holder. The view searching is done using ViewId function of the
        /// MvxWpfPresenterAttribute.
        /// </summary>
        NewOrHistoryExsist,

        /// <summary>
        /// The presenter will search for the matching view in every holder and in the
        /// navigation stack of every holder, if it found a matching view, its holder will
        /// be displayed, otherwise it will display the view in a new holder.
        /// </summary>
        Active,
    }
}