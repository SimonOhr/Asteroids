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
    class ButtonMenu
    {
        protected bool hasTitle;

        Texture2D buttonTex;
        Texture2D backgroundTex;
        protected Texture2D borderTex;
        public Rectangle menuRec;

        Layout layout;

        public List<Button> menuButtons;

        readonly int offsetFromCenter;

        protected int selectedbutton;

        SpriteFont spriteFont;
        Color buttonTextColor;

        protected bool bordered;

        public ButtonMenu(bool hasTitle, string[] buttonTexts, Rectangle menuRec, Layout layout, Texture2D backgroundTex, Texture2D buttonTex, Texture2D borderTex, SpriteFont spriteFont, Color buttonTextColor, bool bordered)
        {
            this.hasTitle = hasTitle;
            this.menuRec = menuRec;
            this.layout = layout;
            this.backgroundTex = backgroundTex;
            this.buttonTex = buttonTex;
            this.borderTex = borderTex;
            this.spriteFont = spriteFont;
            this.buttonTextColor = buttonTextColor;
            this.bordered = bordered;
            menuButtons = new List<Button>();

            offsetFromCenter = 10;

            if (buttonTexts != null)
            {
                InitializeButtons(buttonTexts);
            }
        }

        protected void InitializeButtons(string[] buttonTexts)
        {
            if (layout == Layout.Horizontal)
            {
                for (int i = 0; i < buttonTexts.Length; i++)
                {
                    Button button = new Button(new Vector2(menuRec.X + menuRec.Width / (buttonTexts.Length + 1) * (i + 1) - offsetFromCenter, menuRec.Y + menuRec.Height / 2 - offsetFromCenter), buttonTex, borderTex, buttonTexts[i], spriteFont, buttonTextColor, bordered);
                    menuButtons.Add(button);
                }
            }
            else if (layout == Layout.Vertical)
            {
                for (int i = 0; i < buttonTexts.Length; i++)
                {
                    Button button = new Button(new Vector2(menuRec.X + menuRec.Width / 2 - offsetFromCenter, menuRec.Y + menuRec.Height / (buttonTexts.Length + 1) * (i + 1) - offsetFromCenter), buttonTex, borderTex, buttonTexts[i], spriteFont, buttonTextColor, bordered);
                    menuButtons.Add(button);
                }
            }

            selectedbutton = hasTitle ? 1 : 0;
        }

        public void Update()
        {
            CheckMouseSelection();
            CheckKeySelection();

            if (hasTitle && selectedbutton == 0)
            {
                selectedbutton = 1;
            }

            for (int i = (hasTitle ? 1 : 0); i < menuButtons.Count; i++)
            {
                if (i == selectedbutton)
                {
                    menuButtons[i].Select();
                }
                else
                {
                    menuButtons[i].UnSelect();
                }
            }
        }

        private void CheckMouseSelection()
        {
            for (int i = (hasTitle ? 1 : 0); i < menuButtons.Count; i++)
            {
                if (menuButtons[i].destinationRec.Contains(KeyMouseReader.CursorViewToWorldPosition))
                {
                    selectedbutton = i;
                }
            }
        }

        private void CheckKeySelection()
        {
            if (selectedbutton < menuButtons.Count - 1)
            {
                if ((layout == Layout.Vertical && KeyMouseReader.KeyPressed(Keys.Down)) || (layout == Layout.Horizontal && KeyMouseReader.KeyPressed(Keys.Right)) || KeyMouseReader.ScrollDown())
                {
                    selectedbutton++;
                }
            }
            if (selectedbutton > (hasTitle ? 1 : 0))
            {
                if ((layout == Layout.Vertical && KeyMouseReader.KeyPressed(Keys.Up)) || (layout == Layout.Horizontal && KeyMouseReader.KeyPressed(Keys.Left)) || KeyMouseReader.ScrollUp())
                {
                    selectedbutton--;
                }
            }
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backgroundTex, menuRec, Color.DarkBlue);

            for (int i = 0; i < menuButtons.Count; i++)
            {
                menuButtons[i].Draw(spriteBatch);
            }
        }



        public int? ClickedNumber()
        {
            if (Clicked())
            {
                return selectedbutton;
            }

            return null;
        }

        public string ClickedName()
        {
            if (Clicked())
            {
                return menuButtons[selectedbutton].text;
            }

            return null;
        }

        protected bool Clicked()
        {
            if (menuButtons.Count > (hasTitle ? 1 : 0))
            {
                if (KeyMouseReader.KeyPressed(Keys.Enter))
                {
                    return true;
                }
                else if (menuButtons[selectedbutton].destinationRec.Contains(KeyMouseReader.CursorViewToWorldPosition) && KeyMouseReader.LeftClick())
                {
                    return true;
                }
            }

            return false;
        }

        public void ChangeTitle(string newTitle)
        {
            if (hasTitle)
            {
                if (layout == Layout.Horizontal)
                {
                    Button button = new Button(new Vector2(menuRec.X + menuRec.Width / (menuButtons.Count + 1) - offsetFromCenter, menuRec.Y + menuRec.Height / 2 - offsetFromCenter), buttonTex, borderTex, newTitle, spriteFont, buttonTextColor, bordered);
                    menuButtons[0] = button;
                }
                else if (layout == Layout.Vertical)
                {
                    Button button = new Button(new Vector2(menuRec.X + menuRec.Width / 2 - offsetFromCenter, menuRec.Y + menuRec.Height / (menuButtons.Count + 1) - offsetFromCenter), buttonTex, borderTex, newTitle, spriteFont, buttonTextColor, bordered);
                    menuButtons[0] = button;
                }
            }
        }

        public void ChangeButtons(string[] newButtonTexts)
        {
            Button tempTitle = null;
            if (hasTitle) { tempTitle = menuButtons[0]; }
            menuButtons.Clear();

            string[] buttonTexts = new string[newButtonTexts.Length + (hasTitle ? 1 : 0)];
            if (hasTitle && tempTitle != null) { buttonTexts[0] = tempTitle.text; }

            for (int i = (hasTitle ? 1 : 0); i < buttonTexts.Length; i++)
            {
                buttonTexts[i] = newButtonTexts[i - (hasTitle ? 1 : 0)];
            }

            if (layout == Layout.Horizontal)
            {
                for (int i = 0; i < buttonTexts.Length; i++)
                {
                    Button button = new Button(new Vector2(menuRec.X + menuRec.Width / (buttonTexts.Length + 1) * (i + 1) - offsetFromCenter, menuRec.Y + menuRec.Height / 2 - offsetFromCenter), buttonTex, borderTex, buttonTexts[i], spriteFont, buttonTextColor, bordered);
                    menuButtons.Add(button);
                }
            }
            else if (layout == Layout.Vertical)
            {
                for (int i = 0; i < buttonTexts.Length; i++)
                {
                    Button button = new Button(new Vector2(menuRec.X + menuRec.Width / 2 - offsetFromCenter, menuRec.Y + menuRec.Height / (buttonTexts.Length + 1) * (i + 1) - offsetFromCenter), buttonTex, borderTex, buttonTexts[i], spriteFont, buttonTextColor, bordered);
                    menuButtons.Add(button);
                }
            }
        }

        public void MakeButtonSelectable(int number, bool selectable)
        {
            menuButtons[number].selectable = selectable;
        }
    }
}


