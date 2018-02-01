using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;

namespace Asteroids_2_Return_of_the_Asteroids
{
    enum GameState { MenuPhase, DisplayHiSc, PlayPhase, EndPhase, SaveHighScorePhase }
    enum Layout { Horizontal, Vertical }

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Camera camera;
        Viewport view;
        Rectangle backgroundRec;
        GameplayManager gm;
        private IntPtr intPtr;

        Random rnd = new Random();

        public static int score;

        GameState currentstate;

        Rectangle screenRec;

        ButtonMenu startMenu, hiScMenu, endgameMenu, pauseMenu;

        HighScoreItem hiSc;
        // string[] hiScNames;
        bool hasPaused;

        List<HighScoreItem> hsArray;
        Form1 form;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            this.Window.Position = new Point(Window.ClientBounds.Width / 5, 0);
            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 1000;
            graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            AssetsManager.LoadContent(Content);
            view = GraphicsDevice.Viewport;
            CreateCamera();
            backgroundRec = new Rectangle(view.X, view.Y, view.Width, view.Height);

            gm = new GameplayManager(rnd, Window);

            screenRec = Window.ClientBounds;

            endgameMenu = new ButtonMenu(true, new string[5] { "Asteroids", "Restart", "Save HighScore", "MainMenu", "Exit" }, new Rectangle(0, 0, screenRec.Width, screenRec.Height), Layout.Vertical, AssetsManager.backgroundTex, AssetsManager.buttonTex, AssetsManager.buttonTex, AssetsManager.text, Color.White, false);

            startMenu = new ButtonMenu(true, new string[5] { "Asteroids", "Start Game", "HighScore", "Instructions", "Exit" }, new Rectangle(0, 0, screenRec.Width, screenRec.Height), Layout.Vertical, AssetsManager.backgroundTex, AssetsManager.buttonTex, AssetsManager.buttonTex, AssetsManager.text, Color.Wheat, false);

            pauseMenu = new ButtonMenu(false, new string[3] { "Resume Game", "HighScore", "Exit" }, new Rectangle(0, 0, screenRec.Width, screenRec.Height), Layout.Vertical, AssetsManager.transBackgroundTex, AssetsManager.buttonTex, AssetsManager.buttonTex, AssetsManager.text, Color.Wheat, false);

            hiScMenu = new ButtonMenu(true, new string[3] { "HighScore", "Search", "Back" }, new Rectangle(0, 0, screenRec.Width, screenRec.Height), Layout.Vertical, AssetsManager.backgroundTex, AssetsManager.buttonTex, AssetsManager.buttonTex, AssetsManager.text, Color.Wheat, false);

            score = 10;

            form = new Form1(this);

            hsArray = new List<HighScoreItem>();

            currentstate = GameState.MenuPhase;


        }

        private void CreateCamera()
        {
            camera = new Camera(view);
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            KeyMouseReader.Update(camera);
            backgroundRec.X = (int)camera.GetPosition().X;
            backgroundRec.Y = (int)camera.GetPosition().Y;

            switch (currentstate)
            {
                case GameState.MenuPhase:
                    IsMouseVisible = true;

                    startMenu.Update();
                    StartMenuButtons();
                    camera.SetPosition(new Vector2(Window.ClientBounds.Width / 2, Window.ClientBounds.Height / 2));
                    break;
                case GameState.DisplayHiSc:
                    hiScMenu.Update();
                    HiScMenuButtons();

                    break;
                case GameState.PlayPhase:
                    IsMouseVisible = false;
                    camera.SetPosition(gm.Ship.Pos);
                    if (!hasPaused)
                    {
                        gm.Update(gameTime);
                    }

                    if (KeyMouseReader.KeyPressed(Keys.Escape) && !hasPaused)
                    {
                        hasPaused = true;
                    }

                    if (hasPaused)
                    {
                        IsMouseVisible = true;
                        pauseMenu.Update();
                        PauseMenuButtons();
                    }

                    if (ShipBase.hitPoints <= 0)
                    {
                        currentstate = GameState.EndPhase;
                    }

                    break;
                case GameState.EndPhase:
                    IsMouseVisible = true;

                    endgameMenu.Update();
                    EndgameMenuButtons();

                    break;
                case GameState.SaveHighScorePhase:

                    form.Show();

                    if (form.formDone)
                    {
                        form.Close();
                        form.formDone = false;
                        SaveHighScore();
                        currentstate = GameState.EndPhase;
                        form = new Form1(this);
                    }
                    break;
            }
            base.Update(gameTime);
        }

        private void StartMenuButtons()
        {
            if (startMenu.ClickedName() == "Start Game")
            {
                currentstate = GameState.PlayPhase;
            }

            if (startMenu.ClickedName() == "HighScore")
            {
                currentstate = GameState.DisplayHiSc;
            }

            if (startMenu.ClickedName() == "Exit")
            {
                Exit();
            }
        }

        private void HiScMenuButtons()
        {
            if (hiScMenu.ClickedName() == "Back")
            {
                currentstate = GameState.MenuPhase;
            }
        }
        private void PauseMenuButtons()
        {
            if (pauseMenu.ClickedName() == "Resume Game")
            {
                hasPaused = false;
            }

            if (pauseMenu.ClickedName() == "Exit")
            {
                Exit();
            }
        }

        private void EndgameMenuButtons()
        {
            if (endgameMenu.ClickedName() == "Restart")
            {
                CleanSlate();
                currentstate = GameState.PlayPhase;
            }

            if (endgameMenu.ClickedName() == "Save HighScore")
            {
                currentstate = GameState.SaveHighScorePhase;
            }

            if (endgameMenu.ClickedName() == "MainMenu")
            {
                CleanSlate();
                currentstate = GameState.MenuPhase;
            }

            if (endgameMenu.ClickedName() == "Exit")
            {
                Exit();
            }
        }

        private void CleanSlate()
        {
            ShipBase.hitPoints = 3;
            score = 0;
            GameplayManager.asteroids.Clear();
            gm.ClearProjectileList();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.GetTransform());
            spriteBatch.Draw(AssetsManager.backgroundTex, backgroundRec, null, Color.White, 0f, new Vector2(AssetsManager.backgroundTex.Width / 2, AssetsManager.backgroundTex.Height / 2), SpriteEffects.None, 0);

            switch (currentstate)
            {
                case GameState.MenuPhase:
                    startMenu.Draw(spriteBatch);
                    break;
                case GameState.DisplayHiSc:
                    hiScMenu.Draw(spriteBatch);
                    break;
                case GameState.PlayPhase:
                    gm.Draw(spriteBatch);
                    spriteBatch.DrawString(AssetsManager.text, "Score: " + score, new Vector2(10, 10), Color.White);
                    spriteBatch.DrawString(AssetsManager.text, "Hull Hitpoints: " + ShipBase.hitPoints, new Vector2(10, 30), Color.White);
                    if (hasPaused)
                    {
                        pauseMenu.Draw(spriteBatch);
                    }

                    if (ShipBase.hitPoints <= 0)
                    {
                        currentstate = GameState.EndPhase;
                    }
                    break;
                case GameState.EndPhase:
                    endgameMenu.Draw(spriteBatch);
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {
                        currentstate = GameState.MenuPhase;
                    }
                    break;
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void ReadFromFile()
        {
            StreamReader file = new StreamReader("HighScore.txt");
            string line;
            hsArray.Clear();
            while (!file.EndOfStream)
            {
                line = file.ReadLine();
                string[] temp = line.Split(',');
                int score = int.Parse(temp[1]);
                hiSc = new HighScoreItem(temp[0], score);
                hsArray.Add(hiSc);
            }
            file.Close();
        }

        private void SaveToFile()
        {
            StreamWriter file = new StreamWriter("HighScore.txt");
            for (int i = 0; i < hsArray.Count; i++)
            {
                file.WriteLine(hsArray[i].ToString());
            }
            file.Close();
        }

        private void SaveHighScore()
        {
            ReadFromFile();
            hsArray.Add(new HighScoreItem(form.PlayerName, score));
            SaveToFile();
            Console.WriteLine("Score is saved");
            // 
        }

        //private void GetScoreList() {
        //    int it = 0;
        //    ReadFromFile();
        //    foreach (HighScoreItem h in hsArray) {
        //        hiScNames[it] = h.name;
        //        it++;
        //    }
        //}
    }
}
