#region Dapplo 2017 - GNU Lesser General Public License

// Dapplo - building blocks for .NET applications
// Copyright (C) 2016-2017 Dapplo
// 
// For more information see: http://dapplo.net/
// Dapplo repositories are hosted on GitHub: https://github.com/dapplo
// 
// This file is part of Dapplo.Jenkins
// 
// Dapplo.Jenkins is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Dapplo.Jenkins is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have a copy of the GNU Lesser General Public License
// along with Dapplo.Jenkins. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

#endregion

using System;
using System.Threading;
using System.Threading.Tasks;
using Dapplo.HttpExtensions;
using Dapplo.Jenkins.Domains;
using Dapplo.Jenkins.Entities;

namespace Dapplo.Jenkins
{
    /// <summary>
    ///     This is the interface which describes the Jenkins client
    /// </summary>
    public interface IJenkinsClient
    {
        /// <summary>
        /// Get the overview of what the Jenkins server has
        /// </summary>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Overview</returns>
        Task<Overview> GetOverviewAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        ///     Store the specific HttpBehaviour, which contains a IHttpSettings and also some additional logic for making a HttpClient which works with Jenkins
        /// </summary>
        IHttpBehaviour Behaviour { get; }

        /// <summary>
        ///     The IHttpSettings used for every request, made available so they can be modified
        /// </summary>
        IHttpSettings Settings { get; }

        /// <summary>
        ///     The base URI for your Jenkins server
        /// </summary>
        Uri JenkinsBaseUri { get; }

        /// <summary>
        ///     Set Basic Authentication for the current client
        /// </summary>
        /// <param name="user">username</param>
        /// <param name="password">password</param>
        void SetBasicAuthentication(string user, string password);

        /// <summary>
        ///     This initializes the Jenkins client, in this case retrieves the crumbIssuer, which makes it possible to start calling the server
        /// </summary>
        /// <param name="cancellationToken">CancellationToken</param>
        Task InitializeAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        ///     Job domain
        /// </summary>
        IJobDomain Job { get; }

        /// <summary>
        ///     View domain
        /// </summary>
        IViewDomain View { get; }

    }
}