using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Dapplo.Jenkins.Entities
{
	[DataContract]
	public class Action
	{
		public IList<ParameterDefinition> parameterDefinitions { get; set; }
	}
}
