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

using System.Collections.Generic;
using System.Runtime.Serialization;

#endregion

namespace Dapplo.Jenkins.Entities
{
    /// <summary>
    /// The overview of the jenkins server
    /// </summary>
    [DataContract]
    public class Overview
    {
        /// <summary>
        /// TODO: Needs comment...
        /// </summary>
        [DataMember(Name = "mode", EmitDefaultValue = false)]
        public string Mode { get; set; }

        /// <summary>
        /// TODO: Needs comment...
        /// </summary>
        [DataMember(Name = "nodeDescription", EmitDefaultValue = false)]
        public string NodeDescription { get; set; }

        /// <summary>
        /// TODO: Needs comment...
        /// </summary>
        [DataMember(Name = "nodeName", EmitDefaultValue = false)]
        public string NodeName { get; set; }

        /// <summary>
        /// TODO: Needs comment...
        /// </summary>
        [DataMember(Name = "numExecutors", EmitDefaultValue = false)]
        public int NumExecutors { get; set; }

        /// <summary>
        /// TODO: Needs comment...
        /// </summary>
        [DataMember(Name = "description", EmitDefaultValue = false)]
        public string Description { get; set; }

        /// <summary>
        /// List of jobs
        /// </summary>
        [DataMember(Name = "jobs", EmitDefaultValue = false)]
        public IList<Job> Jobs { get; set; }

        /// <summary>
        /// The primairy view
        /// </summary>
        [DataMember(Name = "primaryView", EmitDefaultValue = false)]
        public View PrimaryView { get; set; }

        /// <summary>
        /// TODO: Needs comment...
        /// </summary>
        [DataMember(Name = "quietingDown", EmitDefaultValue = false)]
        public bool QuietingDown { get; set; }

        /// <summary>
        /// TODO: Needs comment...
        /// </summary>
        [DataMember(Name = "slaveAgentPort", EmitDefaultValue = false)]
        public int SlaveAgentPort { get; set; }

        /// <summary>
        /// TODO: Needs comment...
        /// </summary>
        [DataMember(Name = "useCrumbs", EmitDefaultValue = false)]
        public bool UseCrumbs { get; set; }

        /// <summary>
        /// TODO: Needs comment...
        /// </summary>
        [DataMember(Name = "useSecurity", EmitDefaultValue = false)]
        public bool UseSecurity { get; set; }

        /// <summary>
        /// The views
        /// </summary>
        [DataMember(Name = "views", EmitDefaultValue = false)]
        public IList<View> Views { get; set; }
    }
}