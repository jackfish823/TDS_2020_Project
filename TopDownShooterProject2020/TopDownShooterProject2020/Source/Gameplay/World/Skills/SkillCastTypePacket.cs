#region Includes
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace TopDownShooterProject2020
{
    public class SkillCastTypePacket
    {
        public int seletionType;
        public Skill skill;

        public SkillCastTypePacket(int seletionType, Skill skill)
        {
            this.seletionType = seletionType;
            this.skill = skill;
        }
    }
}


