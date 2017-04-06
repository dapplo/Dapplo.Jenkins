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
    /// Details for a job
    /// </summary>
    [DataContract]
    public class JobDetails : Job
    {
        /// <summary>
        /// Possible actions for the job
        /// </summary>
        [DataMember(Name = "actions", EmitDefaultValue = false)]
        public IList<Action> Actions { get; set; }

        /// <summary>
        /// Is the job buildable?
        /// </summary>
        [DataMember(Name = "buildable", EmitDefaultValue = false)]
        public bool Buildable { get; set; }

        /// <summary>
        /// Description of a job
        /// </summary>
        [DataMember(Name = "description", EmitDefaultValue = false)]
        public string Description { get; set; }

        /// <summary>
        /// How long did the job take
        /// </summary>
        [DataMember(Name = "duration", EmitDefaultValue = false)]
        public int Duration { get; set; }

        /// <summary>
        /// Estimation of how long the job will take
        /// </summary>
        [DataMember(Name = "estimatedDuration", EmitDefaultValue = false)]
        public int EstimatedDuration { get; set; }

        /// <summary>
        /// Full name of the job
        /// </summary>
        [DataMember(Name = "fullDisplayName", EmitDefaultValue = false)]
        public string FullDisplayName { get; set; }

        /// <summary>
        /// Id of the job
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }

        /// <summary>
        /// TODO: Needs comment...
        /// </summary>
        [DataMember(Name = "keepLog", EmitDefaultValue = false)]
        public bool KeepLog { get; set; }

        /// <summary>
        /// TODO: Needs comment...
        /// </summary>
        [DataMember(Name = "number", EmitDefaultValue = false)]
        public int Number { get; set; }

        /// <summary>
        /// TODO: Needs comment...
        /// </summary>
        [DataMember(Name = "result", EmitDefaultValue = false)]
        public string Result { get; set; }

        /// <summary>
        /// TODO: Needs comment...
        /// </summary>
        [DataMember(Name = "timestamp", EmitDefaultValue = false)]
        public long Timestamp { get; set; }

        /// <summary>
        /// TODO: Needs comment...
        /// </summary>
        [DataMember(Name = "builtOn", EmitDefaultValue = false)]
        public string BuiltOn { get; set; }

        /// <summary>
        /// Changes of this job
        /// </summary>
        [DataMember(Name = "changeSet", EmitDefaultValue = false)]
        public ChangeSet ChangeSet { get; set; }

        /// <summary>
        /// The culprits why the job failed
        /// </summary>
        [DataMember(Name = "culprits", EmitDefaultValue = false)]
        public IList<User> Culprits { get; set; }
    }
}