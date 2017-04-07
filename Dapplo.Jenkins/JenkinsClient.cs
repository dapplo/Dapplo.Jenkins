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

#region Usings

using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Dapplo.HttpExtensions;
using Dapplo.HttpExtensions.JsonSimple;
using Dapplo.Jenkins.Domains;
using Dapplo.Jenkins.Entities;
using Dapplo.Jenkins.Internal;
using Dapplo.Log;

#endregion

namespace Dapplo.Jenkins
{
    /// <summary>
    ///     Jenkins Client, using Dapplo.HttpExtensions
    /// </summary>
    public class JenkinsClient : IJobDomain, IViewDomain
    {
        private static readonly LogSource Log = new LogSource();

        /// <inheritdoc />
        public IHttpBehaviour Behaviour { get; }

        private string _apiToken;

        private CrumbIssuer _crumbIssuer;

        private string _user;

        /// <summary>
        ///     Factory method to create the jira client
        /// </summary>
        public static IJenkinsClient Create(Uri baseUri, IHttpSettings httpSettings = null)
        {
            return new JenkinsClient(baseUri, httpSettings);
        }

        /// <summary>
        ///     Create the JenkinsApi object, here the HttpClient is configured
        /// </summary>
        /// <param name="baseUri">Base URL, e.g. https://yourjenkinsserver</param>
        /// <param name="httpSettings">IHttpSettings or null for default</param>
        private JenkinsClient(Uri baseUri, IHttpSettings httpSettings = null)
        {
            if (baseUri == null)
            {
                throw new ArgumentNullException(nameof(baseUri));
            }
            JenkinsBaseUri = baseUri;

            Settings = httpSettings ?? HttpExtensionsGlobals.HttpSettings.ShallowClone();
            Settings.PreAuthenticate = true;

            Behaviour = new HttpBehaviour
            {
                HttpSettings = Settings,
                JsonSerializer = new SimpleJsonSerializer(),
                OnHttpRequestMessageCreated = httpMessage =>
                {
                    if (_crumbIssuer != null)
                    {
                        httpMessage?.Headers.TryAddWithoutValidation(_crumbIssuer.CrumbRequestField, _crumbIssuer.Crumb);
                    }
                    if (!string.IsNullOrEmpty(_user) && _apiToken != null)
                    {
                        httpMessage?.SetBasicAuthorization(_user, _apiToken);
                    }
                    return httpMessage;
                }
            };
        }

        /// <inheritdoc />
        public IHttpSettings Settings { get; }

        /// <inheritdoc />
        public Uri JenkinsBaseUri { get; }

        /// <inheritdoc />
        public void SetBasicAuthentication(string user, string apiToken)
        {
            _user = user;
            _apiToken = apiToken;
        }

        /// <summary>
        ///     This retrieves the crumbIssuer, which makes it possible to start calling the API
        /// </summary>
        /// <param name="cancellationToken">CancellationToken</param>
        public async Task InitializeAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            if (_crumbIssuer == null)
            {
                Behaviour.MakeCurrent();
                var crumbIssuerUri = JenkinsBaseUri.AppendSegments("crumbIssuer", "api", "json");
                var response = await crumbIssuerUri.GetAsAsync<HttpResponse<CrumbIssuer, string>>(cancellationToken).ConfigureAwait(false);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    _crumbIssuer = response.Response;
                    Log.Info().WriteLine("Using crumb header for XSS prevention.");
                }
                else
                {
                    // No crumbIssuer available, this is probably not active, so skip it
                    Log.Warn().WriteLine("CrumbIssuer not found, probably not active (this might be okay). Error was: {0}", response.StatusCode);
                    Log.Warn().WriteLine(response.ErrorResponse);
                }
            }
        }

        /// <inheritdoc />
        public IJobDomain Job => this;

        /// <inheritdoc />
        public IViewDomain View => this;

        /// <summary>
        ///     Retrieve the Jenkins overview (jobs etc)
        /// </summary>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>ProjectList</returns>
        public async Task<Overview> GetOverviewAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            Log.Debug().WriteLine("Retrieving overview");
            var overviewUri = JenkinsBaseUri.AppendSegments("api", "json");
            Behaviour.MakeCurrent();

            var response = await overviewUri.GetAsAsync<HttpResponse<Overview>>(cancellationToken).ConfigureAwait(false);
            return response.HandleErrors();
       }
    }
}