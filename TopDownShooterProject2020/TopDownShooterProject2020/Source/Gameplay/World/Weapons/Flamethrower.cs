#region Includes
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion


namespace TopDownShooterProject2020
{
    public class Flamethrower : BasicWeapon
    {
        SoundEffect soundEffectshoot, soundEffectreload; // test

        public Flamethrower(Unit owner)
            : base("2d\\Weapons\\flamethrower_inventory", owner, new Vector2(90, 225), new Vector2(27, -105))
        {
            this.magazineSize = 100;
            this.currentBullets = this.magazineSize;
            this.sprayable = true;

            SetFireDelay(10.0f); 
            SetReloadTime(1.0f);




            soundEffectshoot = Globals.content.Load<SoundEffect>("Audio\\flamethrower_fire_sound");
            soundEffectreload = Globals.content.Load<SoundEffect>("Audio\\rifle_reload_sound");

        }


        public override void Update(Vector2 offset)
        {
            if (fireDelay.Test() && reloadTime.Test() && currentBullets > 0)
            {
                    soundEffectshoot.Play();
                    GameGlobals.PassDamaginObject(new Flame(new Vector2(owner.position.X, owner.position.Y) + RotatedVectorTowardsMouse(), owner));
                    currentBullets -= 3;
                    fireDelay.ResetToZero();
            }
            if (reloadTime.Test() && currentBullets <= 0)
            {
                Reload();
            }
            
            base.Update(offset);
        }

        public override void Reload() // override because i have speical sound, later make it pass sound class to the base and make general
        {
            soundEffectreload.Play();
            this.reloadTime.ResetToZero();
            this.currentBullets = this.magazineSize;
        }

        public override void Draw(Vector2 offset, Vector2 origin)
        {
            base.Draw(offset, origin);
        }
    }
}
