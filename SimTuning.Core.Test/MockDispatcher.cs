﻿using MvvmCross.Base;
using MvvmCross.ViewModels;
using MvvmCross.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimTuning.Test
{
    public class MockDispatcher
     : MvxMainThreadDispatcher
     , IMvxViewDispatcher
    {
        public readonly List<MvxPresentationHint> Hints = new List<MvxPresentationHint>();
        public readonly List<MvxViewModelRequest> Requests = new List<MvxViewModelRequest>();

        public override bool IsOnMainThread => throw new NotImplementedException();

        Task<bool> IMvxViewDispatcher.ChangePresentation(MvxPresentationHint hint)
        {
            Hints.Add(hint);
            return Task.FromResult(true);
        }

        public Task ExecuteOnMainThreadAsync(Action action, bool maskExceptions = true)
        {
            action();
            return Task.FromResult(true);
        }

        public Task ExecuteOnMainThreadAsync(Func<Task> action, bool maskExceptions = true)
        {
            action();
            return Task.FromResult(true);
        }

        public override bool RequestMainThreadAction(Action action, bool maskExceptions = true)
        {
            action();
            return true;
        }

        Task<bool> IMvxViewDispatcher.ShowViewModel(MvxViewModelRequest request)
        {
            Requests.Add(request);
            return Task.FromResult(true);
        }
    }
}