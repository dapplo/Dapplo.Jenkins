using System.Runtime.Serialization;

namespace Dapplo.Jenkins.Entities
{
	[DataContract]
	public class CrumbIssuer
	{
		[DataMember(Name = "crumb", EmitDefaultValue = false)]
		public string Crumb
		{
			get;
			set;
		}

		[DataMember(Name = "crumbRequestField", EmitDefaultValue = false)]
		public string CrumbRequestField
		{
			get;
			set;
		}
	}
}
