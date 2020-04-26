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
    public class SkillMenu
    {
        public bool active;
        Animated2d background;
        Skill selectedSkill; // Drag and drop woud not need this
        MainCharacter mainCharacter;

        public SkillMenu(MainCharacter mainCharacter)
        {
            background = new Animated2d("2d\\Misc\\inventory", new Vector2(Globals.screenWidth/2, Globals.screenHeight/2), new Vector2(1230, 734), Globals.oneFrameOnly, Color.White);

            this.mainCharacter = mainCharacter;
        }

        public virtual void  Update()
        {
            if(active)
            {
                for(int i = 0; i< mainCharacter.Skills.Count; i++)
                {
                    if(mainCharacter.Skills[i].icon.Hover(background.position - background.dimensions / 2 + new Vector2(586, 134) + new Vector2((i % 4) * 123, (i / 4) * 50)))
                    {
                        if(Globals.mouse.LeftClick())
                        {
                            selectedSkill = mainCharacter.Skills[i];
                            selectedSkill.icon.color = Color.Blue;
                        }
                    }
                    else
                    {
                        mainCharacter.Skills[i].icon.color = Color.White;
                    }
                }

                if (Globals.keyboard.GetSinglePress("Q"))
                {
                    mainCharacter.skillBar.slots[0].skillButton = new SkillButton("2d\\Misc\\solid", new Vector2(0, 0), new Vector2(40, 40), mainCharacter.setSkill, selectedSkill);
                  
                }

                if (Globals.keyboard.GetSinglePress("E"))
                {
                    mainCharacter.skillBar.slots[1].skillButton = new SkillButton("2d\\Misc\\solid", new Vector2(0, 0), new Vector2(40, 40), mainCharacter.setSkill, selectedSkill);

                }

                if (Globals.keyboard.GetSinglePress("R"))
                {
                    mainCharacter.skillBar.slots[2].skillButton = new SkillButton("2d\\Misc\\solid", new Vector2(0, 0), new Vector2(40, 40), mainCharacter.setSkill, selectedSkill);

                }
            }
        }

        public virtual void ToggleActive()
        {
            active = !active;

            selectedSkill = null;
        }

        public virtual void Draw()
        {
            if (active)
            {
                background.Draw(Vector2.Zero);

                for (int i = 0; i < mainCharacter.Skills.Count; i++)
                {
                    Animated2d tempIcon = new Animated2d("2d\\Misc\\solid", mainCharacter.Skills[i].icon.position, new Vector2(113, 113), Globals.oneFrameOnly, mainCharacter.Skills[i].icon.color);
                    tempIcon.texture = mainCharacter.Skills[i].icon.texture;
                    tempIcon.Draw(background.position - background.dimensions / 2 + new Vector2(586, 134) + new Vector2((i%4) * 123, (i/4) * 50)); // 4 each row mod the rows and devide the columns classic western type
                }
            }
        }
    }
}
