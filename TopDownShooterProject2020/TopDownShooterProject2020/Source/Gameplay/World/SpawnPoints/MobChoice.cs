#region Includes
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
#endregion

namespace TopDownShooterProject2020
{
    // Data packet
    public class MobChoice
    {
        public int rate;
        public string mobString;
        public MobChoice(string mobString, int rate)
        {
            this.rate = rate;
            this.mobString = mobString;
        }
    }
}
