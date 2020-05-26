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


namespace TopDownShooterProject2020.Source.Engine.Output
{
    public class SoundControl
    {
        public SoundItem backgroundMusic;

        private List<SoundItem> sounds = new List<SoundItem>();
        public SoundControl(string musicPath)
        {

            if (musicPath != "")
            {
                changeMusic(musicPath);
            }
        }

        public virtual void adjustVolume(float precent)
        {
            if(backgroundMusic.instance != null)
            {
                backgroundMusic.instance.Volume = precent * backgroundMusic.volume;
            }
        }        

        public virtual void AddSound(string name, string soundPath, float volume)
        {
            sounds.Add(new SoundItem(name, soundPath, volume));
        }
        public virtual void changeMusic(string musicPath)
        {
            if(backgroundMusic != null)
            backgroundMusic.instance.Stop();

                backgroundMusic = new SoundItem("bkg music", musicPath, 1);
                backgroundMusic.CreateInstance();


                FormOption musicVolume = Globals.optionsMenu.GetOptionValue("Music Volume");
                float musicVolumePrecent = 1.0f;
                if (musicVolume != null)
                {
                    musicVolumePrecent = (float)Convert.ToDecimal(musicVolume.value, Globals.culture) / 30f; // 30 max volume
                }


                adjustVolume(musicVolumePrecent);
                backgroundMusic.instance.IsLooped = true;
                backgroundMusic.instance.Play();                    
        }

        public virtual void PlaySound(string name, bool resetEveryTime)
        {
            for(int i = 0; i < sounds.Count; i++)
            {
                if(sounds[i].name == name)
                {
                    if(resetEveryTime)
                    {
                        sounds[i].CreateInstance();
                    }
                    RunSound(sounds[i].sound, sounds[i].instance, sounds[i].volume);
                }
            }
        }

        public virtual void RunSound(SoundEffect sound, SoundEffectInstance instance, float volume)
        {
            FormOption soundVolume = Globals.optionsMenu.GetOptionValue("Sound Volume");
            float soundVolumePrecent = 1.0f;
            if (soundVolume != null)
            {
                soundVolumePrecent = (float)Convert.ToDecimal(soundVolume.value, Globals.culture) / 30f; // 30 max volume
            }

            instance.Volume = soundVolumePrecent * volume;
            instance.Play();
        }
        
    }
}
