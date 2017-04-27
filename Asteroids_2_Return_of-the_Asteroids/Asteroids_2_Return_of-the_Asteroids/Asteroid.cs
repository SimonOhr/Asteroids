using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids_2_Return_of_the_Asteroids
{
    class Asteroid
    {
        GameWindow window;

        Texture2D asteroid1Tex;
        Texture2D asteroid2Tex;        
        List<Texture2D> asteroidTextures = new List<Texture2D>();
        Texture2D choosenAsteroidTex;

        List<Vector2> asteroidSpawnPoints;

        Rectangle asteroidZoneLeft;
        Vector2 asteroidPosZoneLeft;       

        Rectangle asteroidZoneRight;
        Vector2 asteroidPosZoneRight;        

        Rectangle asteroidZoneUp;
        Vector2 asteroidPosZoneUp;

        public Vector2 asteroidPos;

        Rectangle asteroidHitbox;

        Vector2 speed;

        Rectangle targetDirection;
        public Vector2 direction;

        Random rnd;        

        public bool isOutOfPlay;        
        
        public Vector2 velocity;
        public int mass;

        public int AsteroidRadius { get; private set; }

        float rotation;

        public Asteroid(GameWindow window, Random rnd)
        {
            this.window = window;
            this.rnd = rnd;

            asteroid1Tex = AssetsManager.asteroid1Tex;
            asteroid2Tex = AssetsManager.asteroid2Tex;
            asteroidTextures.Add(asteroid1Tex);
            asteroidTextures.Add(asteroid2Tex);
            choosenAsteroidTex = asteroidTextures[rnd.Next(asteroidTextures.Count)];           

            asteroidZoneLeft = new Rectangle(0, 0, 400, window.ClientBounds.Height);
            asteroidPosZoneLeft = new Vector2(rnd.Next((asteroidZoneLeft.Width) + choosenAsteroidTex.Width) * -1, rnd.Next(asteroidZoneLeft.Height));

            asteroidZoneRight = new Rectangle(window.ClientBounds.Width, 0, 400, window.ClientBounds.Height);
            asteroidPosZoneRight = new Vector2(window.ClientBounds.Width + choosenAsteroidTex.Width, rnd.Next(asteroidZoneRight.Height));

            asteroidZoneUp = new Rectangle(0, 0, window.ClientBounds.Width, 400);
            asteroidPosZoneUp = new Vector2(rnd.Next(asteroidZoneUp.Width), rnd.Next(asteroidZoneUp.Height) *-1);

            asteroidSpawnPoints = new List<Vector2>();
            asteroidSpawnPoints.Add(asteroidPosZoneLeft);
            asteroidSpawnPoints.Add(asteroidPosZoneRight);
            asteroidSpawnPoints.Add(asteroidPosZoneUp);
                     
            asteroidPos = asteroidSpawnPoints[rnd.Next(asteroidSpawnPoints.Count)];

            asteroidHitbox = new Rectangle((int)asteroidPos.X, (int)asteroidPos.Y, choosenAsteroidTex.Width, choosenAsteroidTex.Height);          

            targetDirection = new Rectangle((window.ClientBounds.Width / 2), (window.ClientBounds.Height / 2), 1, 1);

            speed = new Vector2(rnd.Next(1,7),rnd.Next(1,7));   
                    
            direction = GetDirection();

            AsteroidRadius = 50;

            velocity = speed * direction;

            GetAsteroidMass();        
        }

        public void Update(GameTime gt)
        {
            asteroidPos += velocity;            
            asteroidHitbox.X = (int)asteroidPos.X;
            asteroidHitbox.Y = (int)asteroidPos.Y;            

            if (asteroidPos.X < -asteroidZoneLeft.Width || asteroidPos.X > window.ClientBounds.Width + asteroidZoneRight.Width
                || asteroidPos.Y < -asteroidZoneUp.Height || asteroidPos.Y > window.ClientBounds.Height)
            {
                isOutOfPlay = true;
            }                              
        }
        private void GetAsteroidMass()
        {
            if (choosenAsteroidTex == asteroidTextures[1])
            {
                mass = 1;
            }
            else if (choosenAsteroidTex == asteroidTextures[0])
            {
                mass = 1;
            }
        }
        private Vector2 GetDirection()
        {            
            Vector2 Direction = new Vector2(targetDirection.X - asteroidPos.X, targetDirection.Y - asteroidPos.Y);
            return Vector2.Normalize(Direction);
        }
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(choosenAsteroidTex, new Vector2(asteroidHitbox.X, asteroidHitbox.Y), null, Color.White,0, new Vector2(choosenAsteroidTex.Width / 2, choosenAsteroidTex.Height / 2), 1, SpriteEffects.None, 1);
           // sb.Draw(shipTex, shipPos, null, Color.White, currentRotation + MathHelper.ToRadians(90), new Vector2(shipTex.Width / 2, shipTex.Height / 2), 1, SpriteEffects.None, 1);
        }
    }
}
