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

using System.Net;
using Dapplo.HttpExtensions;
using Dapplo.Log;

namespace Dapplo.Jenkins.Internal
{
    /// <summary>
    /// Handle the HttpResponse object
    /// </summary>
    internal static class HttpResponseExtensions
    {
        private static readonly LogSource Log = new LogSource();

        /// <summary>
        /// Helper method to log the error
        /// </summary>
        /// <param name="httpStatusCode">HttpStatusCode</param>
        /// <param name="error">Error</param>
        private static void LogError(HttpStatusCode httpStatusCode, string error = null)
        {
            // Log all error information
            Log.Warn().WriteLine("Http status code: {0} ({1}). Response from server: {2}", httpStatusCode.ToString(), (int)httpStatusCode, error ?? httpStatusCode.ToString());
        }



        /// <summary>
        ///     Helper method for handling errors in the response, if the response doesn't have the expected status code an
        ///     exception is thrown.
        ///     Else the real response is returned.
        /// </summary>
        /// <typeparam name="TResponse">Type for the ok content</typeparam>
        /// <param name="expectedHttpStatusCode">HttpStatusCode to expect</param>
        /// <param name="response">TResponse</param>
        /// <returns>TResponse</returns>
        public static TResponse HandleErrors<TResponse>(this HttpResponse<TResponse> response, HttpStatusCode expectedHttpStatusCode = HttpStatusCode.OK)
            where TResponse : class
        {
            if (response.StatusCode == expectedHttpStatusCode)
            {
                return response.Response;
            }
            LogError(response.StatusCode);
            throw new JenkinsException(response.StatusCode);
        }

        /// <summary>
        ///     Helper method for handling errors in the response, if the response doesn't have the expected status code an
        ///     exception is thrown.
        /// </summary>
        /// <param name="expectedHttpStatusCode">HttpStatusCode to expect</param>
        /// <param name="response">TResponse</param>
        public static void HandleStatusCode(this HttpResponse response, HttpStatusCode expectedHttpStatusCode = HttpStatusCode.OK)
        {
            if (response.StatusCode == expectedHttpStatusCode)
            {
                return;
            }
            LogError(response.StatusCode);
            throw new JenkinsException(response.StatusCode);
        }
    }
}
