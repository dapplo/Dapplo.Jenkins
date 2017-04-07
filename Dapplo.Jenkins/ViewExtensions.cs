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
using System.Xml.Linq;
using Dapplo.HttpExtensions;
using Dapplo.Jenkins.Domains;
using Dapplo.Jenkins.Entities;
using Dapplo.Jenkins.Internal;
using Dapplo.Log;

namespace Dapplo.Jenkins
{
    /// <summary>
    /// Extensions for the View domain
    /// </summary>
    public static class ViewExtensions
    {
        private static readonly LogSource Log = new LogSource();

        /// <summary>
        ///     Get the details for a View
        /// </summary>
        /// <param name="jenkinsClient">IViewDomain to bind the extension method to</param>
        /// <param name="viewName">the name of the view</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>ViewDetails</returns>
        public static async Task<ViewDetails> GetAsync(this IViewDomain jenkinsClient, string viewName, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (viewName == null)
            {
                throw new ArgumentNullException(nameof(viewName));
            }
            Log.Debug().WriteLine("Retrieving view details for {0}", viewName);
            var viewDetailsUri = jenkinsClient.JenkinsBaseUri.AppendSegments("view", viewName, "api", "json");
            jenkinsClient.Behaviour.MakeCurrent();

            var response = await viewDetailsUri.GetAsAsync<HttpResponse<ViewDetails>>(cancellationToken).ConfigureAwait(false);
            return response.HandleErrors();
        }

        /// <summary>
        ///     Get the XML for a view
        /// </summary>
        /// <param name="jenkinsClient">IViewDomain to bind the extension method to</param>
        /// <param name="viewName">the name of the view</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>XDocument</returns>
        public static async Task<XDocument> GetXmlAsync(this IViewDomain jenkinsClient, string viewName, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (viewName == null)
            {
                throw new ArgumentNullException(nameof(viewName));
            }
            Log.Debug().WriteLine("Retrieving view XML for {0}", viewName);
            var viewXmlUri = jenkinsClient.JenkinsBaseUri.AppendSegments("view", viewName, "config.xml");
            jenkinsClient.Behaviour.MakeCurrent();

            var response = await viewXmlUri.GetAsAsync<HttpResponse<XDocument>>(cancellationToken).ConfigureAwait(false);
            return response.HandleErrors();
        }

        /// <summary>
        /// Delete view by name
        /// </summary>
        /// <param name="jenkinsClient">IViewDomain to bind the extension method to</param>
        /// <param name="viewName">name of the view to delete</param>
        /// <param name="cancellationToken">CancellationToken</param>
        public static async Task DeleteAsync(this IViewDomain jenkinsClient, string viewName, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (viewName == null)
            {
                throw new ArgumentNullException(nameof(viewName));
            }
            Log.Debug().WriteLine("Deleting view {0}", viewName);

            var deleteViewUri = jenkinsClient.JenkinsBaseUri.AppendSegments("view", viewName, "doDelete");
            jenkinsClient.Behaviour.MakeCurrent();

            var response = await deleteViewUri.PostAsync<HttpResponse>(null, cancellationToken).ConfigureAwait(false);
            response.HandleStatusCode();
        }

        /// <summary>
        /// Create view
        /// </summary>
        /// <param name="jenkinsClient">IViewDomain to bind the extension method to</param>
        /// <param name="viewName">name of the view to create</param>
        /// <param name="viewXmlDocument">XDocument with config.xml for the view</param>
        /// <param name="cancellationToken">CancellationToken</param>
        public static async Task CreateAsync(this IViewDomain jenkinsClient, string viewName, XDocument viewXmlDocument, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (viewName == null)
            {
                throw new ArgumentNullException(nameof(viewName));
            }
            Log.Debug().WriteLine("Creating view {0}", viewName);

            var createViewUri = jenkinsClient.JenkinsBaseUri.AppendSegments("createView").ExtendQuery("name", viewName);
            jenkinsClient.Behaviour.MakeCurrent();

            var response = await createViewUri.PostAsync<HttpResponse>(viewXmlDocument, cancellationToken).ConfigureAwait(false);
            response.HandleStatusCode();
        }
    }
}
