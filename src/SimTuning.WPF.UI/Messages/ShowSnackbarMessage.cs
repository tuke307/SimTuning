// Copyright (c) 2021 tuke productions. All rights reserved.
using MvvmCross.Plugin.Messenger;

namespace SimTuning.WPF.UI.Messages
{
    public class ShowSnackbarMessage
        : MvxMessage
    {
        public string Message { get; }

        public ShowSnackbarMessage(object sender, string message)
            : base(sender)
        {
            Message = message;
        }
    }
}