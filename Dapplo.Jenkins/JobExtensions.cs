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
    /// Extensions for the Job domain
    /// </summary>
    public static class JobExtensions
    {
        private static readonly LogSource Log = new LogSource();

        /// <summary>
        ///     Get the details for a job
        /// </summary>
        /// <param name="jenkinsClient">IJobDomain to bind the extension method to</param>
        /// <param name="jobName">the name of the job</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>JobDetails</returns>
        public static async Task<JobDetails> GetAsync(this IJobDomain jenkinsClient, string jobName, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (jobName == null)
            {
                throw new ArgumentNullException(nameof(jobName));
            }
            Log.Debug().WriteLine("Retrieving job details for {0}", jobName);
            var jobDetailsUri = jenkinsClient.JenkinsBaseUri.AppendSegments("job", jobName, "api", "json");
            jenkinsClient.Behaviour.MakeCurrent();

            var response = await jobDetailsUri.GetAsAsync<HttpResponse<JobDetails>>(cancellationToken).ConfigureAwait(false);
            return response.HandleErrors();
        }

        /// <summary>
        ///     Get the XML for a job
        /// </summary>
        /// <param name="jenkinsClient">IJobDomain to bind the extension method to</param>
        /// <param name="jobName">the name of the job</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>XDocument</returns>
        public static async Task<XDocument> GetXmlAsync(this IJobDomain jenkinsClient, string jobName, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (jobName == null)
            {
                throw new ArgumentNullException(nameof(jobName));
            }
            Log.Debug().WriteLine("Retrieving job XML for {0}", jobName);
            var jobXmlUri = jenkinsClient.JenkinsBaseUri.AppendSegments("job", jobName, "config.xml");
            jenkinsClient.Behaviour.MakeCurrent();

            var response = await jobXmlUri.GetAsAsync<HttpResponse<XDocument>>(cancellationToken).ConfigureAwait(false);
            return response.HandleErrors();
        }

        /// <summary>
        /// Delete job by name
        /// </summary>
        /// <param name="jenkinsClient">IJobDomain to bind the extension method to</param>
        /// <param name="jobName">name of the job to delete</param>
        /// <param name="cancellationToken">CancellationToken</param>
        public static async Task DeleteAsync(this IJobDomain jenkinsClient, string jobName, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (jobName == null)
            {
                throw new ArgumentNullException(nameof(jobName));
            }
            Log.Debug().WriteLine("Deleting job {0}", jobName);

            var deleteJobUri = jenkinsClient.JenkinsBaseUri.AppendSegments("job", jobName, "doDelete");
            jenkinsClient.Behaviour.MakeCurrent();

            var response = await deleteJobUri.PostAsync<HttpResponse>(null, cancellationToken).ConfigureAwait(false);
            response.HandleStatusCode();
        }

        /// <summary>
        /// Enable job by name
        /// </summary>
        /// <param name="jenkinsClient">IJobDomain to bind the extension method to</param>
        /// <param name="jobName">name of the job to enable</param>
        /// <param name="cancellationToken">CancellationToken</param>
        public static async Task EnableAsync(this IJobDomain jenkinsClient, string jobName, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (jobName == null)
            {
                throw new ArgumentNullException(nameof(jobName));
            }
            Log.Debug().WriteLine("Enabling job {0}", jobName);

            var enableJobUri = jenkinsClient.JenkinsBaseUri.AppendSegments("job", jobName, "enable");
            jenkinsClient.Behaviour.MakeCurrent();

            var response = await enableJobUri.PostAsync<HttpResponse>(null, cancellationToken).ConfigureAwait(false);
            response.HandleStatusCode();
        }

        /// <summary>
        /// Disable job by name
        /// </summary>
        /// <param name="jenkinsClient">IJobDomain to bind the extension method to</param>
        /// <param name="jobName">name of the job to disable</param>
        /// <param name="cancellationToken">CancellationToken</param>
        public static async Task DisableAsync(this IJobDomain jenkinsClient, string jobName, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (jobName == null)
            {
                throw new ArgumentNullException(nameof(jobName));
            }
            Log.Debug().WriteLine("Disabling job {0}", jobName);

            var disableJobUri = jenkinsClient.JenkinsBaseUri.AppendSegments("job", jobName, "disable");
            jenkinsClient.Behaviour.MakeCurrent();

            var response = await disableJobUri.PostAsync<HttpResponse>(null, cancellationToken).ConfigureAwait(false);
            response.HandleStatusCode();
        }

        /// <summary>
        /// Create job
        /// </summary>
        /// <param name="jenkinsClient">IJobDomain to bind the extension method to</param>
        /// <param name="jobName">name of the job to create</param>
        /// <param name="jobXmlDocument">XDocument with config.xml for the job</param>
        /// <param name="cancellationToken">CancellationToken</param>
        public static async Task CreateAsync(this IJobDomain jenkinsClient, string jobName, XDocument jobXmlDocument, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (jobName == null)
            {
                throw new ArgumentNullException(nameof(jobName));
            }
            Log.Debug().WriteLine("Creating job {0}", jobName);

            var createJobUri = jenkinsClient.JenkinsBaseUri.AppendSegments("createItem").ExtendQuery("name", jobName);
            jenkinsClient.Behaviour.MakeCurrent();

            var response = await createJobUri.PostAsync<HttpResponse>(jobXmlDocument, cancellationToken).ConfigureAwait(false);
            response.HandleStatusCode();
        }
    }
}
