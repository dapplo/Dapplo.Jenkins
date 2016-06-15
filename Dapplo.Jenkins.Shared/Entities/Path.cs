using System.Runtime.Serialization;

namespace Dapplo.Jenkins.Entities
{
	[DataContract]
	public class Path
	{
		[DataMember(Name = "editType", EmitDefaultValue = false)]
		public string EditType { get; set; }

		[DataMember(Name = "file", EmitDefaultValue = false)]
		public string File { get; set; }
	}
}
