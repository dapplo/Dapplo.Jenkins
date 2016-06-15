using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Dapplo.Jenkins.Entities
{
	[DataContract]
	public class ChangeSet
	{
		[DataMember(Name = "items", EmitDefaultValue = false)]
		public IList<ChangeSetItem> Items { get; set; }
		[DataMember(Name = "kind", EmitDefaultValue = false)]
		public string Kind { get; set; }
	}
}
