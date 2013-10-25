using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using SwitchAB.Models;

namespace SwitchAB.Analytics
{
	public interface IAnalyticsProvider
	{
		Task RecordTrialStarted(string trialName, string targetName);

		Task RecordTrialSucceeded(string trialName, string targetName);
	}
}

