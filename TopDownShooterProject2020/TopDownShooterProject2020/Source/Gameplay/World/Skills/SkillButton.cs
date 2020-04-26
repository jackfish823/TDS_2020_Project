#region Includes
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace TopDownShooterProject2020
{
    public class SkillButton : BasicButton
    {
        public Vector2 lastOffset;
        public Skill skill;
        public SkillButtonSlot slot;

        public SkillButton(string path, Vector2 position, Vector2 dimensions, PassObject ButtonClicked, object info) 
            : base (path, position, dimensions,"", "", ButtonClicked, info)
        {
            skill = (Skill)info;
            slot = null;
        }

        public override void Update(Vector2 offset)
        {
            lastOffset = offset;

            if(skill != null)
            {
                base.Update(offset);
            }
        }

        public override void RunButtonClick()
        {
            if (ButtonClicked != null)
            {
                SkillCastTypePacket tempPacket = new SkillCastTypePacket(1, (Skill)info);
                if(Hover(lastOffset))
                {
                    tempPacket.seletionType = 0;
                }
                ButtonClicked(tempPacket);
            }

            Reset();
        }

        public override void Draw(Vector2 offeset)
        {           
            base.Draw(offeset);

            if(skill != null)
            {
                skill.icon.Draw(offeset);
            }

        }
    }
}
