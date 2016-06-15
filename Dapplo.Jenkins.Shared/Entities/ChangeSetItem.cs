using System.Collections.Generic;
using System.Runtime.Serialization;

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
