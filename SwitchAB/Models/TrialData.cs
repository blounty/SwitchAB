using Cirrious.MvvmCross.Plugins.Sqlite;
using System;

namespace SwitchAB.Models
{
	class TrialData
	{
		[PrimaryKey]
		public string Id { get; set; }

		public string TrialName {
			get;
			set;
		}

		public string TargetName {
			get;
			set;
		}

		public TrialData ()
		{
			this.Id = Guid.NewGuid ().ToString ();
		}
	}
}

