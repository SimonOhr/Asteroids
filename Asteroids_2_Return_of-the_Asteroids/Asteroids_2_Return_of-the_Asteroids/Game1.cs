﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Asteroids_2_Return_of_the_Asteroids
{
    enum GameState { MenuPhase, PlayPhase, EndPhase }
    enum Layout { Horizontal, Vertical }

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        GameplayManager gm;

        Random rnd = new Random();

        public static int score;

        GameState currentstate;

        new Rectangle screenRec;

        ButtonMenu startMenu, endgameMenu, pauseMenu;

        bool hasPaused;

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

            gm = new GameplayManager(rnd, Window);

            screenRec = Window.ClientBounds;

            endgameMenu = new ButtonMenu(true, new string[4] { "Asteroids", "Restart", "MainMenu", "Exit" }, new Rectangle(0, 0, screenRec.Width, screenRec.Height), Layout.Vertical, AssetsManager.backgroundTex, AssetsManager.buttonTex, AssetsManager.buttonTex, AssetsManager.text, Color.White, false);

            startMenu = new ButtonMenu(true, new string[5] { "Asteroids", "Start Game", "HighScore", "Instructions", "Exit" }, new Rectangle(0, 0, screenRec.Width, screenRec.Height), Layout.Vertical, AssetsManager.backgroundTex, AssetsManager.buttonTex, AssetsManager.buttonTex, AssetsManager.text, Color.Wheat, false);

            pauseMenu = new ButtonMenu(false, new string[3] { "Resume Game", "HighScore", "Exit" }, new Rectangle(0, 0, screenRec.Width, screenRec.Height), Layout.Vertical, AssetsManager.transBackgroundTex, AssetsManager.buttonTex, AssetsManager.buttonTex, AssetsManager.text, Color.Wheat, false);

            currentstate = GameState.MenuPhase;
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            KeyMouseReader.Update();
            switch (currentstate)
            {
                case GameState.MenuPhase:
                    IsMouseVisible = true;

                    startMenu.Update();
                    StartMenuButtons();

                    break;
                case GameState.PlayPhase:
                    IsMouseVisible = false;

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
                    if (Ship.hitPoints <= 0)
                    {
                        currentstate = GameState.EndPhase;
                    }

                    break;
                case GameState.EndPhase:
                    IsMouseVisible = true;

                    endgameMenu.Update();
                    EndgameMenuButtons();

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

            if (startMenu.ClickedName() == "Exit")
            {
                Exit();
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
            if (endgameMenu.ClickedName() == "Exit")
            {
                Exit();
            }

            if (endgameMenu.ClickedName() == "Restart")
            {
                CleanSlate();
                currentstate = GameState.PlayPhase;
            }

            if (endgameMenu.ClickedName() == "MainMenu")
            {
                CleanSlate();
                currentstate = GameState.MenuPhase;
            }
        }

        private void CleanSlate()
        {
            Ship.hitPoints = 3;
            score = 0;
            gm.asteroids.Clear();
            gm.projectiles.Clear();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            switch (currentstate)
            {
                case GameState.MenuPhase:
                    startMenu.Draw(spriteBatch);
                    break;
                case GameState.PlayPhase:
                    gm.Draw(spriteBatch);
                    spriteBatch.DrawString(AssetsManager.text, "Score: " + score, new Vector2(10, 10), Color.White);
                    spriteBatch.DrawString(AssetsManager.text, "Hull Hitpoints: " + Ship.hitPoints, new Vector2(10, 30), Color.White);

                    if (hasPaused)
                    {
                        pauseMenu.Draw(spriteBatch);
                    }

                    if (Ship.hitPoints <= 0)
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
    }
}
