using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Dapplo.Jenkins.Entities
{
	[DataContract]
	public class ParameterDefinition
	{
		public DefaultParameterValue defaultParameterValue { get; set; }
		public string description { get; set; }
		public string name { get; set; }
		public string type { get; set; }
		public List<string> choices { get; set; }
	}
}
