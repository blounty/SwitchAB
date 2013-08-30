using System;
using System.Collections.Generic;
using System.Diagnostics;
using Cirrious.MvvmCross.Plugins.Sqlite;
using XPlatUtils;
using SwitchAB.Core.Models;
using System.Net;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;


namespace SwitchAB
{
	public partial class Switcher
	{
        private const string DatabaseName = "Switch_AB_DB";

        private ISQLiteConnection databaseConnection;

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

		public async Task Setup(string apiKey)
		{
            //TODO: Wrap this in a Task.Factory.StartNew
            RandomGenerator = new Random((int)DateTime.UtcNow.Ticks);

            this.databaseConnection = ServiceContainer.Resolve<ISQLiteConnectionFactory>().Create(DatabaseName);
            this.databaseConnection.CreateTable<Target>();
            this.databaseConnection.CreateTable<Trial>();
            this.databaseConnection.CreateTable<Sync>();

            var syncTable = this.databaseConnection.Table<Sync>();
            var trialTable = this.databaseConnection.Table<Trial>();

            if (!syncTable.Any())
            {
                this.databaseConnection.Insert(new Sync { Id = Guid.NewGuid().ToString() });
            }
            else
            {
                var sync = syncTable.Single();
                List<Trial> trials;
                if (sync.SyncDate.HasValue)
                {
                    trials = trialTable.Where(tr => tr.Start >= sync.SyncDate).ToList();
                }
                else
                {
                    trials = trialTable.ToList();
                }
                
                if(trials.Count() > 0)
                {

                    sync.SyncDate = DateTime.Now.ToUniversalTime();

					var client = new HttpClient ();
					client.BaseAddress = new Uri("http://localhost:56851/");

					client.DefaultRequestHeaders.Accept.Add(
						new MediaTypeWithQualityHeaderValue("application/json"));

					await client.PostAsJsonAsync ("api/Blah", trials);
                    this.databaseConnection.Update(sync);
                }
            }
		}

		public void TargetAchieved(string targetName)
		{          
            var targetsTable = this.databaseConnection.Table<Target>();
            var trialsTable = this.databaseConnection.Table<Trial>();
            var target = targetsTable.Single(t => t.TargetName == targetName);
            var trial = trialsTable
                .Where(tr => tr.TargetId == target.Id)
                .OrderByDescending(tr => tr.Start)
                .First();

            if (!trial.End.HasValue)
            {
                trial.End = DateTime.Now.ToUniversalTime();

                this.databaseConnection.Update(trial);
            }
		}

		public void TestWithTarget(string targetName, List<Action> actions)
		{
            Target target;

            var targetsTable = this.databaseConnection.Table<Target>();
            if (!targetsTable.Any(t => t.TargetName == targetName))
            {
                var randomIndex = RandomGenerator.Next(1, actions.Count + 1) - 1;

                target = Target.Create(targetName, randomIndex);

                this.databaseConnection.Insert(target);
            } else{
                target = targetsTable.Single(t => t.TargetName == targetName);
            }

            var action = actions[target.TargetIndex];

            action();

            var trial = Trial.Create(target.Id, DateTime.Now.ToUniversalTime());

            this.databaseConnection.Insert(trial);
		}
	}
}

