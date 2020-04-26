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
    public class SkillButtonSlot
    {
        public Animated2d slotIcon;
        public SkillButton skillButton;
        public SkillButtonSlot(Vector2 position)
        {
            slotIcon = new Animated2d("2d\\Misc\\skillbarsquare", position, new Vector2(45, 45), Globals.oneFrameOnly, Color.White);
            skillButton = null;
        }

        public virtual void Update(Vector2 offset)
        {
            slotIcon.Update(offset);

            if(skillButton != null)
            {
                skillButton.Update(offset + slotIcon.position);           
            }
        }

        public virtual void Draw(Vector2 offset)
        {

            if(slotIcon != null)
            {
                Globals.CleanShader();
                slotIcon.Draw(offset);
            }

            if (skillButton != null)
            {
                skillButton.Draw(offset + slotIcon.position);
            }

        }
    }
}
