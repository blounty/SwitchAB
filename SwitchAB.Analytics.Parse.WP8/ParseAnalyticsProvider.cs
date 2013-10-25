using Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchAB.Analytics.Parse
{
    public class ParseAnalyticsProvider : IAnalyticsProvider
    {
        public ParseAnalyticsProvider()
        {
        }

        #region IAnalyticsProvider implementation

        public async Task RecordTrialStarted(string trialName, string targetName)
        {
            var trialData = new Dictionary<string, string> {
				{"targetName", targetName }
			};
            await ParseAnalytics.TrackEventAsync(string.Format("{0}-Started", trialName), trialData);
        }

        public async Task RecordTrialSucceeded(string trialName, string targetName)
        {
            var trialData = new Dictionary<string, string> {
				{"targetName", targetName }
			};
            await ParseAnalytics.TrackEventAsync(string.Format("{0}-Succeeded", trialName), trialData);
        }

        #endregion
    }
}
