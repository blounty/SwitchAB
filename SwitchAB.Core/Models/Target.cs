using Cirrious.MvvmCross.Plugins.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwitchAB.Core.Models
{
    public class Target
    {
        [PrimaryKey]
        public string Id { get; set; }

        public string TargetName { get; set; }

        public int TargetIndex { get; set; }

        public static Target Create(string targetName, int targetIndex)
        {
            return new Target { Id = Guid.NewGuid().ToString(), TargetIndex = targetIndex, TargetName = targetName };
        }
    }
}
