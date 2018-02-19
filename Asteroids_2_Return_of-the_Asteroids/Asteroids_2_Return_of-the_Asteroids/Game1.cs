using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;

namespace Asteroids_2_Return_of_the_Asteroids
{
    enum GameState { MenuPhase, DisplayHiSc, PlayPhase, EndPhase, SaveHighScorePhase, Exit, EndPause }
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
            backgroundRec = new Rectangle(view.X-30, view.Y-30, view.Width+30, view.Height+30);

            gm = new GameplayManager(rnd, Window);

            screenRec = Window.ClientBounds;
                        

            score = 10;

            form = new Form1(this);

            hsArray = new List<HighScoreItem>();

            GUI.Load(screenRec);

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

                    currentstate = GUI.UpdateStartMenu(currentstate);                    
                   
                    camera.SetPosition(new Vector2(Window.ClientBounds.Width / 2, Window.ClientBounds.Height / 2));
                    break;
                case GameState.DisplayHiSc:
                    currentstate = GUI.UpdateHighScoreMenu(currentstate);                                 
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
                        currentstate = GUI.UpdatePauseMenu(currentstate);                
                    }

                    if (gm.GetPlayerShipHealth() <= 0)
                    {
                        currentstate = GUI.UpdateEndGameMenu(currentstate);
                    }
                    break;
                case GameState.EndPhase:
                    currentstate = GUI.UpdateEndGameMenu(currentstate);
                    IsMouseVisible = true;
                    CleanSlate();  
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
                case GameState.Exit:
                    Exit();
                    break;
                case GameState.EndPause:
                    hasPaused = false;
                    currentstate = GameState.PlayPhase;
                    break;
            }
            base.Update(gameTime);
        }        

        private void CleanSlate()
        {
            ShipBase.hitPoints = PlayerShip.maxHealth;
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
                    GUI.DrawStartMenu(spriteBatch);
                    break;
                case GameState.DisplayHiSc:                  
                    GUI.DrawHighScoreMenu(spriteBatch);
                    break;
                case GameState.PlayPhase:
                    gm.Draw(spriteBatch);
                    GUI.DrawInGameText(spriteBatch);
                    if (hasPaused)
                    {                     
                        GUI.DrawPauseMenu(spriteBatch);
                    }
                    if (ShipBase.hitPoints <= 0)
                    {
                        currentstate = GameState.EndPhase;
                    }
                    break;
                case GameState.EndPhase:                   
                    GUI.endgameMenu.Draw(spriteBatch);
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
