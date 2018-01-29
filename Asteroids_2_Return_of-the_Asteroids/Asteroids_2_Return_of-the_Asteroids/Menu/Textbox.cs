using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids_2_Return_of_the_Asteroids
{
    class Textbox
    {
        Texture2D texture;
        Vector2 position;

      //  KeyboardState currentState, oldState;
        String GUIEnterName;
        String GUIInputName;
        Rectangle textBoxArea;
        SpriteFont spriteFont;
        int offset = 20;

        public Textbox(Texture2D tex, Vector2 pos, SpriteFont sf)
        {
            texture = tex;
            position = pos;
            spriteFont = sf;
        }

        private void CreateTextBox()
        {           
            textBoxArea = new Rectangle((int)position.X - (int)spriteFont.MeasureString(GUIInputName).X / 2, (int)position.Y - (int)spriteFont.MeasureString(GUIInputName).Y / 2, (int)spriteFont.MeasureString(GUIInputName).X + offset, (int)spriteFont.MeasureString(GUIInputName).Y + offset);

        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, textBoxArea, Color.White);
        }

    }
}
