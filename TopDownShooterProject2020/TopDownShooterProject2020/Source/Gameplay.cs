#region Includes
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopDownShooterProject2020.Source.Engine.Output;
#endregion

namespace TopDownShooterProject2020
{
    public enum PlayState
    {
        Play,
        LevelsMap
    }
    public class Gameplay
    {
        

        World world; // Main world

        LevelsMap levelsMap;

        PlayState playState = PlayState.Play;

        PassObject ChangeGameState;

        public Gameplay(PassObject ChangeGameState)
        {
            this.playState = PlayState.LevelsMap;

            this.ChangeGameState = ChangeGameState;

            ResetWorld(null);

            levelsMap = new LevelsMap(LoadLevel);
        }
        public virtual void Update()
        {
            Globals.optionsMenu.prevGameState = GameState.Game;

            if (playState == PlayState.Play)
            {
                this.world.Update();
            }

            else if(playState == PlayState.LevelsMap)
            {
                levelsMap.Update();
            }


        }

        public virtual void ChangePlayState(object info)
        {
            if((PlayState)info == PlayState.LevelsMap)
            {
                levelsMap = new LevelsMap(LoadLevel);
            }
            playState = (PlayState)info;
        }

        public virtual void LoadLevel(object info)
        {
            playState = PlayState.Play;

            int tempLevel = Convert.ToInt32(info, Globals.culture);

            world = new World(ResetWorld, tempLevel, ChangeGameState, ChangePlayState);
        }

        public virtual void ResetWorld(object info)
        {
            int levelID = 1;
            if (world != null)
            {
                levelID = world.levelID;
            }

            world = new World(ResetWorld, levelID, ChangeGameState, ChangePlayState);
        }

        public virtual void Draw()  
        {
            if (playState == PlayState.Play)
            {
                this.world.Draw(Vector2.Zero);
            }
            else if (playState == PlayState.LevelsMap)
            {
                levelsMap.Draw();
            }

        }

    }
}
