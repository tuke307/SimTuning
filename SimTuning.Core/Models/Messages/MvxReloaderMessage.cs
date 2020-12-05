// project=SimTuning.Core, file=MvxReloaderMessage.cs, creation=2020:8:25 Copyright (c)
// 2020 tuke productions. All rights reserved.
using Data.Models;
using MvvmCross.Plugin.Messenger;

namespace SimTuning.Core.Models
{
    /// <summary>
    /// MvxReloaderMessage.
    /// </summary>
    /// <seealso cref="MvvmCross.Plugin.Messenger.MvxMessage" />
    public class MvxReloaderMessage
      : MvxMessage
    {
        public DynoModel Dyno
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MvxReloaderMessage" /> class.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="dyno">The dyno.</param>
        public MvxReloaderMessage(object sender, DynoModel dyno)
            : base(sender)
        {
            this.Dyno = dyno;
        }
    }
}