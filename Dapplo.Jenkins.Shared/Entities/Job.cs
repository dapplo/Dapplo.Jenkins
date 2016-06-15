using System.Runtime.Serialization;

namespace Dapplo.Jenkins.Entities
{
	[DataContract]
	public class Job
	{
		[DataMember(Name = "name", EmitDefaultValue = false)]
		public string Name { get; set; }
		[DataMember(Name = "url", EmitDefaultValue = false)]
		public string Url { get; set; }
		[DataMember(Name = "color", EmitDefaultValue = false)]
		public string Color { get; set; }
	}
}
