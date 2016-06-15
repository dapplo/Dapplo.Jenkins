using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Dapplo.Jenkins.Entities
{
	[DataContract]
	public class ViewDetails
	{
		[DataMember(Name = "description", EmitDefaultValue = false)]
		public string Description { get; set; }
		[DataMember(Name = "jobs", EmitDefaultValue = false)]
		public IList<Job> Jobs { get; set; }
		[DataMember(Name = "name", EmitDefaultValue = false)]
		public string Name { get; set; }
		[DataMember(Name = "property", EmitDefaultValue = false)]
		public List<string> Properties { get; set; }
		[DataMember(Name = "url", EmitDefaultValue = false)]
		public string Url { get; set; }
	}
}
