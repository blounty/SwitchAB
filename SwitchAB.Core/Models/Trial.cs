using Cirrious.MvvmCross.Plugins.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwitchAB.Core.Models
{
    public class Trial
    {
        [PrimaryKey]
        public string Id { get; set; }

        public string TargetId { get; set; }

        public DateTime Start { get; set; }

        public Nullable<DateTime> End { get; set; }


        public static Trial Create(string targetId, DateTime start)
        {
            return new Trial { Id = Guid.NewGuid().ToString(), TargetId = targetId, Start = start };
        }
    }
}
