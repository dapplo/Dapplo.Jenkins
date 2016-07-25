#region Dapplo 2016 - GNU Lesser General Public License

// Dapplo - building blocks for .NET applications
// Copyright (C) 2016 Dapplo
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
	[DataContract]
	public class ChangeSetItem
	{
		[DataMember(Name = "affectedPaths", EmitDefaultValue = false)]
		public IList<string> AffectedPaths { get; set; }

		[DataMember(Name = "commitId", EmitDefaultValue = false)]
		public string CommitId { get; set; }

		[DataMember(Name = "timestamp", EmitDefaultValue = false)]
		public int Timestamp { get; set; }

		[DataMember(Name = "author", EmitDefaultValue = false)]
		public Author Author { get; set; }

		[DataMember(Name = "comment", EmitDefaultValue = false)]
		public string Comment { get; set; }

		[DataMember(Name = "date", EmitDefaultValue = false)]
		public string Date { get; set; }

		[DataMember(Name = "id", EmitDefaultValue = false)]
		public string Id { get; set; }

		[DataMember(Name = "msg", EmitDefaultValue = false)]
		public string Msg { get; set; }

		[DataMember(Name = "paths", EmitDefaultValue = false)]
		public IList<Path> Paths { get; set; }
	}
}