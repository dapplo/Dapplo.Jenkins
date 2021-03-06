﻿#region Dapplo 2017 - GNU Lesser General Public License

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

using System.Runtime.Serialization;

#endregion

namespace Dapplo.Jenkins.Entities
{
    /// <summary>
    /// TODO: Needs comment...
    /// </summary>
    [DataContract]
    public class Path
    {
        /// <summary>
        /// TODO: Needs comment...
        /// </summary>
        [DataMember(Name = "editType", EmitDefaultValue = false)]
        public string EditType { get; set; }

        /// <summary>
        /// TODO: Needs comment...
        /// </summary>
        [DataMember(Name = "file", EmitDefaultValue = false)]
        public string File { get; set; }
    }
}