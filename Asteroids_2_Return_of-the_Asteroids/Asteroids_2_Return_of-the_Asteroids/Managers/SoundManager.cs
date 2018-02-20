using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids_2_Return_of_the_Asteroids
{
    static class SoundManager
    {     
        public static void PlayBgMusic()
        {
            MediaPlayer.Play(AssetsManager.backgroundMusic);//send to explosionclass
            MediaPlayer.IsRepeating = true;//send to explosionclass
            MediaPlayer.Volume = 0.1f;//send to explosionclass
        }

        public static void PlayExplosion()
        {
            var instance = AssetsManager.AsteroidExplosion.CreateInstance();
            instance.Volume = 0.1f;
            instance.Play();
        }

        public static void PlayShot()
        {
            var instance = AssetsManager.LaserShot.CreateInstance(); // Send to soundClass
            instance.Volume = 0.5f;// Send to soundClass
            instance.Play();// Send to soundClass
        }
    }
}
