using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids_2_Return_of_the_Asteroids
{
    static class AssetsManager
    {
        public static Texture2D backgroundTex, shipTex, asteroid1Tex, asteroid2Tex, crosshairTex, projectileTex, buttonTex, transBackgroundTex, particleStarTex, particleCircleTex, particleDiamondTex;

        public static SpriteFont text;

        public static Song backgroundMusic;

        public static SoundEffect laserShot, asteroidExplosion;

        public static List<Texture2D> textures = new List<Texture2D>();

          

        public static void LoadContent(ContentManager Content)
        {
            backgroundTex = Content.Load<Texture2D>("bakgrund");
            shipTex = Content.Load<Texture2D>("skepp");
            asteroid1Tex = Content.Load<Texture2D>("asteroid");
            asteroid2Tex = Content.Load<Texture2D>("asteroid2");
            crosshairTex = Content.Load<Texture2D>("sikte");
            projectileTex = Content.Load<Texture2D>("laserskott");
            buttonTex = Content.Load<Texture2D>("vitRektangel");
            transBackgroundTex = Content.Load<Texture2D>("TransBackground");
            particleStarTex = Content.Load<Texture2D>("star");
            particleCircleTex = Content.Load<Texture2D>("circle");
            particleDiamondTex = Content.Load<Texture2D>("diamond");

            text = Content.Load<SpriteFont>(@"text");

            backgroundMusic = Content.Load<Song>(@"Steamtech-Mayhem");

            laserShot = Content.Load<SoundEffect>(@"laser9");
            asteroidExplosion = Content.Load<SoundEffect>(@"Depth_Charge");

            textures.Add(particleCircleTex);
            textures.Add(particleStarTex);
            textures.Add(particleDiamondTex);
        }
    }
}
