// Copyright (c) 2021 tuke productions. All rights reserved.
using System;
using System.Threading.Tasks;

namespace SimTuning.Core.Services
{
    public interface IBrowserService
    {
        /// <summary>
        /// Opens the browser.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        Task OpenBrowser(string url);
    }
}