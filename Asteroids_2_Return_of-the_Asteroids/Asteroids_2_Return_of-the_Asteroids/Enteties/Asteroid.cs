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

        Texture2D tex;

        List<Texture2D> textures = new List<Texture2D>();

        List<Vector2> asteroidSpawnPoints;

        Rectangle spawnZoneLeft, spawnZoneRight, spawnZoneUp, hitbox, targetDirection;

        Vector2 posZoneLeft, posZoneRight, posZoneUp, speed;
        
        public Vector2 pos, direction;

        Random rnd;

        public bool isOutOfPlay;

        public Vector2 velocity;

        public int hitPoints/*, mass*/;

        public int radius { get; private set; }

        // float rotation;

        public Asteroid(GameWindow window, Random rnd)
        {
            this.window = window;
            this.rnd = rnd;

            textures.Add(AssetsManager.asteroid1Tex);
            textures.Add(AssetsManager.asteroid2Tex);

            tex = textures[rnd.Next(textures.Count)];

            textures.Clear();

            spawnZoneLeft = new Rectangle(0, 0, (tex.Width + 100), window.ClientBounds.Height);
            posZoneLeft = new Vector2(rnd.Next((spawnZoneLeft.Width) + tex.Width) * -1, rnd.Next(spawnZoneLeft.Height));

            spawnZoneRight = new Rectangle(window.ClientBounds.Width, 0, tex.Width, window.ClientBounds.Height);
            posZoneRight = new Vector2(window.ClientBounds.Width + tex.Width, rnd.Next(spawnZoneRight.Height));

            spawnZoneUp = new Rectangle(0, 0, window.ClientBounds.Width, tex.Height);
            posZoneUp = new Vector2(rnd.Next(spawnZoneUp.Width), rnd.Next(spawnZoneUp.Height) * -1);

            asteroidSpawnPoints = new List<Vector2>();
            asteroidSpawnPoints.Add(posZoneLeft);
            asteroidSpawnPoints.Add(posZoneRight);
            asteroidSpawnPoints.Add(posZoneUp);

            pos = asteroidSpawnPoints[rnd.Next(asteroidSpawnPoints.Count)];

            asteroidSpawnPoints.Clear();

            targetDirection = new Rectangle(window.ClientBounds.Width / 2, window.ClientBounds.Height / 2, 1, window.ClientBounds.Height / 3);

            hitbox = new Rectangle((int)pos.X, (int)pos.Y, tex.Width, tex.Height);

            speed = new Vector2(rnd.Next(1, 10), rnd.Next(1, 10));

            direction = GetDirection();

            radius = 50;

            velocity = speed * direction;

            hitPoints = 2;

           // GetAsteroidMass();
        }

        public void Update(GameTime gt)
        {
            pos += velocity;
            hitbox.X = (int)pos.X;
            hitbox.Y = (int)pos.Y;

            if (pos.X < -spawnZoneLeft.Width || pos.X > window.ClientBounds.Width + spawnZoneRight.Width
                || pos.Y < -spawnZoneUp.Height || pos.Y > window.ClientBounds.Height)
            {
                isOutOfPlay = true;
            }
        }
        /// <summary>
        /// Simple mass for physics, not implemented
        /// </summary>
        /// <returns></returns>
        //private void GetAsteroidMass()
        //{
        //    if (tex == textures[1])
        //    {
        //        mass = 1;
        //    }
        //    else if (tex == textures[0])
        //    {
        //        mass = 1;
        //    }
        //}
        private Vector2 GetDirection()
        {
            Vector2 Direction = new Vector2(targetDirection.X - pos.X, targetDirection.Y - pos.Y);
            return Vector2.Normalize(Direction);
        }
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(tex, new Vector2(hitbox.X, hitbox.Y), null, Color.White, 0, new Vector2(tex.Width / 2, tex.Height / 2), 1, SpriteEffects.None, 1);           
        }
    }
}
