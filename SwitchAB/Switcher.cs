using System;
using System.Collections.Generic;
using System.Diagnostics;
using Cirrious.MvvmCross.Plugins.Sqlite;
using SwitchAB.Models;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using SwitchAB.Analytics;


namespace SwitchAB
{
	public partial class Switcher
	{
        private const string DatabaseName = "Switch_AB_DB";

        private ISQLiteConnection databaseConnection;

		private IAnalyticsProvider analyticsProvider;

        private Random RandomGenerator;
       
        private static Switcher switcher;

        public static Switcher Current
        {
            get
            {
                if (switcher == null)
                    switcher = new Switcher();
                return switcher; 
            }
        }

		public void Setup(ISQLiteConnectionFactory sqliteConnectionFactory, IAnalyticsProvider analyticsProvider)
		{
            //TODO: Wrap this in a Task.Factory.StartNew

			var seed = (int)DateTime.Now.Ticks & 0x0000FFFF;

			Debug.WriteLine (string.Format("Seed {0}", seed));

			RandomGenerator = new Random();

			this.databaseConnection = sqliteConnectionFactory.Create(DatabaseName);
			this.databaseConnection.CreateTable<TrialData>();
        
			this.analyticsProvider = analyticsProvider;
		}
		  
		public void TrialSucceeded(string trialName)
		{          
			var trialDataTable = this.databaseConnection.Table<TrialData>();
            
			this.analyticsProvider.RecordTrialSucceeded (trialName, trialDataTable.Single(t => t.TrialName == trialName).TargetName);
		}

		public void BeginTrial(Trial trial)
		{
			var trialDataTable = this.databaseConnection.Table<TrialData>();

			Target target;

			if (!trialDataTable.Any(t => t.TrialName == trial.TrialName))
            {
				var randomIndex = RandomGenerator.Next(0, trial.Targets.Count());

				target = trial.Targets [randomIndex];

				this.databaseConnection.Insert(new TrialData { TrialName = trial.TrialName, TargetName = target.TargetName});
            } 
			else
			{
				target = trial.Targets.Single (trialTarget => trialTarget.TargetName == trialDataTable.Single (t => t.TrialName == trial.TrialName).TargetName);
            }

			target.TargetAction ();

			this.analyticsProvider.RecordTrialStarted (trial.TrialName, target.TargetName);
		}
	}
}

