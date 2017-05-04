using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids_2_Return_of_the_Asteroids
{
    static class AssetsManager
    {
        public static Texture2D backgroundTex, shipTex, asteroid1Tex, asteroid2Tex, crosshairTex, projectileTex, buttonTex;

        public static SpriteFont text;

        public static void LoadContent(ContentManager Content)
        {
            backgroundTex = Content.Load<Texture2D>("bakgrund");
            shipTex = Content.Load<Texture2D>("skepp");
            asteroid1Tex = Content.Load<Texture2D>("asteroid");
            asteroid2Tex = Content.Load<Texture2D>("asteroid2");
            crosshairTex = Content.Load<Texture2D>("sikte");
            projectileTex = Content.Load<Texture2D>("laserskott");
            buttonTex = Content.Load<Texture2D>("vitRektangel");

            text = Content.Load<SpriteFont>(@"text");
        }
    }
}
