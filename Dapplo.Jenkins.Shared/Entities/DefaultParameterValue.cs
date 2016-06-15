using System.Runtime.Serialization;

namespace Dapplo.Jenkins.Entities
{
	[DataContract]
	public class DefaultParameterValue
	{
		public string value { get; set; }
	}
}
