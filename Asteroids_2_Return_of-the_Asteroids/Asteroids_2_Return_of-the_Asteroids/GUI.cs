using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids_2_Return_of_the_Asteroids
{
    static class GUI
    {
        private static Texture2D healthbarTex = AssetsManager.healthBarTex;
        private static Rectangle srcHealthbarTex;
        static int healthmultiplier = (AssetsManager.healthBarTex.Width / 10);
        public static ButtonMenu startMenu, hiScMenu, endgameMenu, pauseMenu;
        public static Vector2 defaultPositionWorldToView, defaultPositionViewToWorld, healthbarPositionWorldToView, healthBarPositionViewToWorld, scoreFontPositionWorldToView, scoreFontPositionViewToWorld;
        private static Rectangle screenRec;

        public static void Load(Rectangle screenRec)
        {
            GUI.screenRec = screenRec;
            defaultPositionWorldToView = Vector2.Zero;
            srcHealthbarTex = new Rectangle(0, 0, ShipBase.hitPoints * healthmultiplier, AssetsManager.healthBarTex.Height);
            healthbarPositionWorldToView = Vector2.Zero;
            scoreFontPositionWorldToView = new Vector2(0, 50);
        }
        public static void UpdateGUIMatrix()
        {
            healthBarPositionViewToWorld = Vector2.Transform(healthbarPositionWorldToView, Matrix.Invert(KeyMouseReader.PassCameraInformation().GetTransform()));
            scoreFontPositionViewToWorld = Vector2.Transform(scoreFontPositionWorldToView, Matrix.Invert(KeyMouseReader.PassCameraInformation().GetTransform()));
            defaultPositionViewToWorld = Vector2.Transform(defaultPositionWorldToView, Matrix.Invert(KeyMouseReader.PassCameraInformation().GetTransform()));
        }

        public static void DrawHealthBar(SpriteBatch sb)
        {
            TrackPlayerHealth();
            sb.Draw(healthbarTex, healthBarPositionViewToWorld, srcHealthbarTex, Color.White);
        }
        private static void TrackPlayerHealth()
        {
            if (srcHealthbarTex.Width != ShipBase.hitPoints * healthmultiplier)
            {
                srcHealthbarTex.Width = ShipBase.hitPoints * healthmultiplier;
            }
        }

        public static void DrawInGameText(SpriteBatch sb)
        {
            sb.DrawString(AssetsManager.Score, "Score: " + Game1.score, scoreFontPositionViewToWorld, Color.White);
            //sb.DrawString(AssetsManager.text, "Hull Hitpoints: " + ShipBase.hitPoints, new Vector2(10, 30), Color.White);
        }
        /// <summary>
        /// creating menus here, so that their position is correct, the way the classes uses the position means, after they're craeted, the position cannot be updated, which would be a much better solution.
        /// Currently though, this works. I am aware that each class gets created 60 times a second, will fix the menu classes, so that the position can be updated. Example below, should work for all menues.
        /// Could add a do_once method, or condition it to update the position once per state, rather than every iteration.
        /// 
        /// private static void SetMenuPos(ref ButtenMenu menu)
        /// {
        /// menu.screenRec.X = (int)defaultPositionViewToWorld.X;
        /// menu.screenRec.Y = (int)defaultPositionViewToWorld.Y;
        /// }
        /// 
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        private static ButtonMenu CreateStartMenu(ButtonMenu menu)
        {
            return menu = new ButtonMenu(true, new string[5] { "Asteroids", "Start Game", "HighScore", "Instructions", "Exit" }, new Rectangle((int)defaultPositionViewToWorld.X, (int)defaultPositionViewToWorld.Y, screenRec.Width, screenRec.Height), Layout.Vertical, AssetsManager.backgroundTex, AssetsManager.buttonTex, AssetsManager.buttonTex, AssetsManager.text, Color.Wheat, false);
        }

        private static ButtonMenu CreatePauseMenu(ButtonMenu menu)
        {
            return menu = new ButtonMenu(false, new string[3] { "Resume Game", "HighScore", "Exit" }, new Rectangle((int)defaultPositionViewToWorld.X, (int)defaultPositionViewToWorld.Y, screenRec.Width, screenRec.Height), Layout.Vertical, AssetsManager.transBackgroundTex, AssetsManager.buttonTex, AssetsManager.buttonTex, AssetsManager.text, Color.Wheat, false);
        }

        private static ButtonMenu CreateEndMenu(ButtonMenu menu)
        {
            return menu = new ButtonMenu(true, new string[5] { "Asteroids", "Restart", "Save HighScore", "MainMenu", "Exit" }, new Rectangle((int)defaultPositionViewToWorld.X, (int)defaultPositionViewToWorld.Y, screenRec.Width, screenRec.Height), Layout.Vertical, AssetsManager.backgroundTex, AssetsManager.buttonTex, AssetsManager.buttonTex, AssetsManager.text, Color.White, false);
        }

        private static ButtonMenu CreateHiScMenu(ButtonMenu menu)
        {
            return menu = new ButtonMenu(true, new string[3] { "HighScore", "Search", "Back" }, new Rectangle((int)defaultPositionViewToWorld.X, (int)defaultPositionViewToWorld.Y, screenRec.Width, screenRec.Height), Layout.Vertical, AssetsManager.backgroundTex, AssetsManager.buttonTex, AssetsManager.buttonTex, AssetsManager.text, Color.Wheat, false);
        }
        public static GameState UpdateStartMenu(GameState currentState)
        {
            startMenu = CreateStartMenu(startMenu);
            startMenu.Update();
            return StartMenuButtons(currentState);
        }

        private static GameState StartMenuButtons(GameState currentstate)
        {
            if (startMenu.ClickedName() == "Start Game")
            {
                return currentstate = GameState.PlayPhase;
            }

            if (startMenu.ClickedName() == "HighScore")
            {
                return currentstate = GameState.DisplayHiSc;
            }

            if (startMenu.ClickedName() == "Exit")
            {
                return currentstate = GameState.Exit;
            }
            return currentstate;
        }

        public static GameState UpdatePauseMenu(GameState currentState)
        {
            pauseMenu = CreatePauseMenu(pauseMenu);
            pauseMenu.Update();
            return PauseMenuButtons(currentState);
        }

        private static GameState PauseMenuButtons(GameState currentstate)
        {
            if (pauseMenu.ClickedName() == "Resume Game")
            {
                return currentstate = GameState.EndPause;
            }

            if (pauseMenu.ClickedName() == "Exit")
            {
                return currentstate = GameState.Exit;
            }
            return currentstate;
        }

        public static GameState UpdateEndGameMenu(GameState currentState)
        {
            endgameMenu = CreateEndMenu(endgameMenu);
            endgameMenu.Update();
            return EndgameMenuButtons(currentState);
        }

        private static GameState EndgameMenuButtons(GameState currentstate)
        {
            if (endgameMenu.ClickedName() == "Restart")
            {
                return currentstate = GameState.PlayPhase;
            }

            if (endgameMenu.ClickedName() == "Save HighScore")
            {
                return currentstate = GameState.SaveHighScorePhase;
            }

            if (endgameMenu.ClickedName() == "MainMenu")
            {
                return currentstate = GameState.MenuPhase;
            }

            if (endgameMenu.ClickedName() == "Exit")
            {
                return currentstate = GameState.Exit;
            }
            return currentstate;
        }

        public static GameState UpdateHighScoreMenu(GameState currentState)
        {
            hiScMenu = CreateHiScMenu(hiScMenu);
            hiScMenu.Update();
            return HiScMenuButtons(currentState);
        }

        private static GameState HiScMenuButtons(GameState currentstate)
        {
            if (hiScMenu.ClickedName() == "Back")
            {
                return currentstate = GameState.MenuPhase;
            }
            return currentstate;
        }

        public static void DrawStartMenu(SpriteBatch sb)
        {
            if (startMenu != null)
                startMenu.Draw(sb);
        }

        public static void DrawHighScoreMenu(SpriteBatch sb)
        {
            if (hiScMenu != null)
                hiScMenu.Draw(sb);
        }

        public static void DrawPauseMenu(SpriteBatch sb)
        {
            if (pauseMenu != null)
                pauseMenu.Draw(sb);
        }

        public static void DrawEndgameMenu(SpriteBatch sb)
        {
            if (endgameMenu != null)
                endgameMenu.Draw(sb);
        }
    }
}
