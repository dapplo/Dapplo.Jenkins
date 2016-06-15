using System.Runtime.Serialization;

namespace Dapplo.Jenkins.Entities
{
	[DataContract]
	public class View
	{
		[DataMember(Name = "name", EmitDefaultValue = false)]
		public string Name { get; set; }
		[DataMember(Name = "url", EmitDefaultValue = false)]
		public string Url { get; set; }
	}
}
