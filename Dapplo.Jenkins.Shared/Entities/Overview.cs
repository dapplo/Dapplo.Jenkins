using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Dapplo.Jenkins.Entities
{
	[DataContract]
	public class Overview
	{
		[DataMember(Name = "mode", EmitDefaultValue = false)]
		public string Mode { get; set; }
		[DataMember(Name = "nodeDescription", EmitDefaultValue = false)]
		public string NodeDescription { get; set; }
		[DataMember(Name = "nodeName", EmitDefaultValue = false)]
		public string NodeName { get; set; }
		[DataMember(Name = "numExecutors", EmitDefaultValue = false)]
		public int NumExecutors { get; set; }
		[DataMember(Name = "description", EmitDefaultValue = false)]
		public string Description { get; set; }
		[DataMember(Name = "jobs", EmitDefaultValue = false)]
		public IList<Job> Jobs { get; set; }
		[DataMember(Name = "primaryView", EmitDefaultValue = false)]
		public PrimaryView PrimaryView { get; set; }
		[DataMember(Name = "quietingDown", EmitDefaultValue = false)]
		public bool QuietingDown { get; set; }
		[DataMember(Name = "slaveAgentPort", EmitDefaultValue = false)]
		public int SlaveAgentPort { get; set; }
		[DataMember(Name = "useCrumbs", EmitDefaultValue = false)]
		public bool UseCrumbs { get; set; }
		[DataMember(Name = "useSecurity", EmitDefaultValue = false)]
		public bool UseSecurity { get; set; }
		[DataMember(Name = "views", EmitDefaultValue = false)]
		public IList<View> Views { get; set; }
	}
}
