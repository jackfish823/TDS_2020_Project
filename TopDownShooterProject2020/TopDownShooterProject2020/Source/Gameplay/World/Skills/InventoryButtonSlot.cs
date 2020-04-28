#region Includes
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace TopDownShooterProject2020
{
    public class InventoryButtonSlot
    {
        public Animated2d slotIcon;
        public InventoryButton InventoryButton;
        public InventoryButtonSlot(Vector2 position)
        {
            slotIcon = new Animated2d("2d\\Misc\\skillbarsquare", position, new Vector2(45, 45), Globals.oneFrameOnly, Color.White);
            InventoryButton = null;
        }

        public virtual void Update(Vector2 offset)
        {
            slotIcon.Update(offset);

            if(InventoryButton != null)
            {
                InventoryButton.Update(offset + slotIcon.position);           
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
            }

        }
    }
}
