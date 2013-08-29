using System;
using System.Collections.Generic;
using Cirrious.MvvmCross.Plugins.Sqlite.Touch;
using Cirrious.MvvmCross.Plugins.Sqlite;

using System.Linq;
using System.Text;
using XPlatUtils;

namespace SwitchAB
{
    public static class SwitcherInitializer
    {
        public static void Bootstrap()
        {
            ServiceContainer.Register<ISQLiteConnectionFactory>(new MvxTouchSQLiteConnectionFactory());
        }
    }
}
         