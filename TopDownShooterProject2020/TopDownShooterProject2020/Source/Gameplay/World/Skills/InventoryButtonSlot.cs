#region Includes
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace TopDownShooterProject2020
{
    public class InventoryButtonSlot
    {
        public Animated2d slotIcon, cooldownShade;
        public InventoryButton InventoryButton;
        public InventoryButtonSlot(Vector2 position)
        {
            slotIcon = new Animated2d("2d\\Misc\\skillbarsquare", position, new Vector2(45, 45), Globals.oneFrameOnly, Color.White);
            cooldownShade = new Animated2d("2d\\Misc\\shade", position, new Vector2(41, 41), Globals.oneFrameOnly, Color.Black);

            InventoryButton = null;
        }

        public virtual void Update(Vector2 offset, float cooldownMsec)
        {
            slotIcon.Update(offset);

            if(InventoryButton != null)
            {
                InventoryButton.Update(offset + slotIcon.position);

                float cooldownShadeXY = 1 - cooldownMsec; // gives the % that should be clean
                if (cooldownShadeXY < 0) cooldownShadeXY = 0;
                cooldownShade.dimensions.Y = 41 * cooldownShadeXY;
                cooldownShade.dimensions.X = 41 * cooldownShadeXY;
            }
        }

        public virtual void Draw(Vector2 offset)
        {

            if(slotIcon != null)
            {
                Globals.CleanShader();
                slotIcon.Draw(offset);
            }

            if (InventoryButton != null)
            {

                InventoryButton.Draw(offset + slotIcon.position);
                cooldownShade.Draw(offset + slotIcon.position);

            }

        }
    }
}
