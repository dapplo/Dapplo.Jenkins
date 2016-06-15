using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Dapplo.Jenkins.Entities
{
	[DataContract]
	public class JobDetails
	{
		[DataMember(Name = "name", EmitDefaultValue = false)]
		public string Name { get; set; }
		[DataMember(Name = "color", EmitDefaultValue = false)]
		public string Color { get; set; }
		[DataMember(Name = "actions", EmitDefaultValue = false)]
		public IList<Action> Actions { get; set; }
		[DataMember(Name = "buildable", EmitDefaultValue = false)]
		public bool Buildable { get; set; }
		[DataMember(Name = "description", EmitDefaultValue = false)]
		public string Description { get; set; }
		[DataMember(Name = "duration", EmitDefaultValue = false)]
		public int Duration { get; set; }
		[DataMember(Name = "estimatedDuration", EmitDefaultValue = false)]
		public int EstimatedDuration { get; set; }
		[DataMember(Name = "fullDisplayName", EmitDefaultValue = false)]
		public string FullDisplayName { get; set; }
		[DataMember(Name = "id", EmitDefaultValue = false)]
		public string Id { get; set; }
		[DataMember(Name = "keepLog", EmitDefaultValue = false)]
		public bool KeepLog { get; set; }
		[DataMember(Name = "number", EmitDefaultValue = false)]
		public int Number { get; set; }
		[DataMember(Name = "result", EmitDefaultValue = false)]
		public string Result { get; set; }
		[DataMember(Name = "timestamp", EmitDefaultValue = false)]
		public long Timestamp { get; set; }
		[DataMember(Name = "url", EmitDefaultValue = false)]
		public string Url { get; set; }
		[DataMember(Name = "builtOn", EmitDefaultValue = false)]
		public string BuiltOn { get; set; }
		[DataMember(Name = "changeSet", EmitDefaultValue = false)]
		public ChangeSet ChangeSet { get; set; }
		[DataMember(Name = "culprits", EmitDefaultValue = false)]
		public IList<Culprit> Culprits { get; set; }
	}
}
