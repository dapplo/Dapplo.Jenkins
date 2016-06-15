using System.Runtime.Serialization;

namespace Dapplo.Jenkins.Entities
{
	[DataContract]
	public class Author
	{
		[DataMember(Name = "absoluteUrl", EmitDefaultValue = false)]
		public string AbsoluteUrl { get; set; }

		[DataMember(Name = "fullName", EmitDefaultValue = false)]
		public string FullName { get; set; }
	}
}
