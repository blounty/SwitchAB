using Cirrious.MvvmCross.Plugins.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwitchAB.Core.Models
{
    public class Sync
    {
        [PrimaryKey]
        public string Id { get; set; }

        public Nullable<DateTime> SyncDate { get; set; }
    }
}
