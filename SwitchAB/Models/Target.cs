using Cirrious.MvvmCross.Plugins.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwitchAB.Models
{
    public class Target
    {
      

        public string TargetName { get; set; }

		public Action TargetAction {
			get;
			set;
		}

		public Target (string targetName, Action targetAction)
		{
			this.TargetAction = targetAction;
			this.TargetName = targetName;
		}
    }
}
