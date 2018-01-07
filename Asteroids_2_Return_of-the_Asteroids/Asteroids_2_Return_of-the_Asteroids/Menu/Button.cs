using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids_2_Return_of_the_Asteroids
{
    class Button
    {
        Vector2 pos;
        Texture2D tex;
        Texture2D borderTex;
        public string text;
        SpriteFont spriteFont;

        public bool selectable = true;
        bool selected;
        protected bool border;

        readonly int offset;

        public Rectangle destinationRec;
        Color recColor;

        Color textColor;

        public Button(Vector2 pos, Texture2D tex, Texture2D borderTex, string text, SpriteFont spriteFont, Color textColor, bool border)
        {
            this.pos = pos;
            this.tex = tex;
            this.borderTex = borderTex;
            this.text = text;
            this.spriteFont = spriteFont;
            this.textColor = textColor;
            this.border = border;

            offset = 20;

            destinationRec = new Rectangle((int)pos.X - (int)spriteFont.MeasureString(text).X / 2, (int)pos.Y - (int)spriteFont.MeasureString(text).Y / 2, (int)spriteFont.MeasureString(text).X + offset, (int)spriteFont.MeasureString(text).Y + offset);
        }

        public void Select()
        {
            selected = true;
            if (selectable)
            {
                recColor = Color.RosyBrown;
            }
            if (!selectable)
            {
                recColor = Color.DarkGray;
            }
        }

        public void UnSelect()
        {
            selected = false;
            if (selectable)
            {
                recColor = Color.LightSlateGray;
            }
            if (!selectable)
            {
                recColor = Color.DarkGray;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(tex, destinationRec, recColor);
            if (border)
            {
                spriteBatch.Draw(AssetsManager.buttonTex, new Rectangle(destinationRec.X - 5, destinationRec.Y - 5, destinationRec.Height * 2, destinationRec.Height), Color.White);
            }
            spriteBatch.DrawString(spriteFont, text, new Vector2(destinationRec.X + destinationRec.Width / 2 - spriteFont.MeasureString(text).X / 2, destinationRec.Y + destinationRec.Height / 2 - spriteFont.MeasureString(text).Y / 2), textColor);
            //if (selected)
            //{
            //    spriteBatch.Draw(AssetsManager.buttonTex, new Rectangle((int)destinationRec.X - (int)(destinationRec.Height * 0.8), (int)destinationRec.Y, (int)(destinationRec.Height * 0.8), destinationRec.Height), Color.White);
            //}
        }
    }
}


