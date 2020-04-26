#region Includes
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
#endregion


namespace TopDownShooterProject2020.Source.Gameplay.World.Players
{
    public class PlayerValuePacket
    {
        private int playerID;
        private object value;
        public PlayerValuePacket(int playerID, object value)
        {
            this.PlayerID = playerID;
            this.Value = value;
        }

        #region Properties
        public int PlayerID { get => playerID; set => playerID = value; }
        public object Value { get => value; set => this.value = value; }
        #endregion


    }
}
