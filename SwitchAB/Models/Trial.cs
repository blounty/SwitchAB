using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwitchAB.Models
{
    public class Trial
    {
		public string TrialName { get; set; }

		public Target[] Targets {
			get;
			set;
		}

		public Trial (string trialName, Target[] targets)
		{
			this.TrialName = trialName;
			this.Targets = targets;
		}
    }
}
