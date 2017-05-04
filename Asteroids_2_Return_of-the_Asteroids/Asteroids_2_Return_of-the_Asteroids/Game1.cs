using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Asteroids_2_Return_of_the_Asteroids
{
    enum GameState { MenuPhase, PlayPhase, EndPhase}
    enum Layout { Horizontal, Vertical }

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;       

        GameplayManager gm;        

        Random rnd = new Random();

        public static int score;

        GameState currentstate;

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

            currentstate = GameState.PlayPhase;
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            switch (currentstate)
            {
                case GameState.MenuPhase:
                    //if ()
                    //{
                    //    currentstate = GameState.PlayPhase;
                    //}
                    break;
                case GameState.PlayPhase:
                    gm.Update(gameTime);

                    if (Ship.hitPoints <= 0)
                    {
                        currentstate = GameState.EndPhase;
                    }
                    break;
                case GameState.EndPhase:
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {
                        currentstate = GameState.MenuPhase;
                    }
                    break;                
            }
           
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            switch (currentstate)
            {
                case GameState.MenuPhase:
                    //if ()
                    //{
                    //    currentstate = GameState.PlayPhase;
                    //}
                    break;
                case GameState.PlayPhase:
                    gm.Draw(spriteBatch);
                    spriteBatch.DrawString(AssetsManager.text, "Score: " + score, new Vector2(10, 10), Color.White);
                    spriteBatch.DrawString(AssetsManager.text, "Hull Hitpoints: " + Ship.hitPoints, new Vector2(10, 30), Color.White);
                    if (Ship.hitPoints <= 0)
                    {
                        currentstate = GameState.EndPhase;
                    }
                    break;
                case GameState.EndPhase:
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
