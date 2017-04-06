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
using System.Net.Http;

namespace Dapplo.Jenkins
{
    /// <summary>
    /// This exception is thrown to add Jenkins specific information to when an exception is thrown
    /// </summary>
    public class JenkinsException : HttpRequestException
    {
        /// <summary>
        /// Constructor with a HttpStatus code and an error response
        /// </summary>
        /// <param name="httpStatusCode">HttpStatusCode</param>
        /// <param name="response">string with the error response message</param>
        public JenkinsException(HttpStatusCode httpStatusCode, string response) : base($"{httpStatusCode}({(int)httpStatusCode}) : {response}")
        {
        }

        /// <summary>
        /// Constructor with a HttpStatus code and an error response
        /// </summary>
        /// <param name="httpStatusCode">HttpStatusCode</param>
        public JenkinsException(HttpStatusCode httpStatusCode) : base($"{httpStatusCode}({(int)httpStatusCode})")
        {
        }

    }
}