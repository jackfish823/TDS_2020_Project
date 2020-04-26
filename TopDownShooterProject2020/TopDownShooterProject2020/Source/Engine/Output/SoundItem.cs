#region Includes
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
#endregion


namespace TopDownShooterProject2020
{
    public class SoundItem
    {
        public float volume;
        public string name;
        public SoundEffect sound;
        public SoundEffectInstance instance;



        public SoundItem(string name, string soundPath, float volume)
        {
            this.name = name;
            this.volume = volume;

            sound = Globals.content.Load<SoundEffect>(soundPath);
            CreateInstance();
        }
        public void CreateInstance()
        {
            instance = sound.CreateInstance();
        }
    }
}
