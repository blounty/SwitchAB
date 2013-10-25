using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using SwitchAB.Analytics.Parse;
using Parse;

namespace SwitchAB.Samples.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate
    {
        UIWindow window;
        MyViewController viewController;

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
			ParseClient.Initialize("747QpfGXazxXuOBzLnQXrtg3dRxeddr9OwvPVbYB", "CHEglynqFyGtpNtuwDyluxvF48sfujlttbUPu6ai");

			Switcher.Current.Setup(new Cirrious.MvvmCross.Plugins.Sqlite.Touch.MvxTouchSQLiteConnectionFactory(), new ParseAnalyticsProvider());

            window = new UIWindow(UIScreen.MainScreen.Bounds);

            viewController = new MyViewController();
            window.RootViewController = viewController;

            window.MakeKeyAndVisible();

            return true;
        }
    }
}

