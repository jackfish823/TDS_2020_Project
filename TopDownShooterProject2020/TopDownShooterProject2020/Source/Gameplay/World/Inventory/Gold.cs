#region Includes
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace TopDownShooterProject2020
{
    public class Gold : InventoryItem 
    {

        public Gold(int amount)
            : base(amount, "Gold")          
        {
            icon = new Basic2d("2d\\Misc\\money_icon", new Vector2(0,0), new Vector2(20, 20));
        }

    }
}
