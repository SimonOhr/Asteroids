using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids_2_Return_of_the_Asteroids
{
    class Asteroid : MovingObject
    {
        GameWindow window;

        Texture2D tex;

        List<Texture2D> textures = new List<Texture2D>();

        List<Vector2> asteroidSpawnPoints;

        Rectangle spawnZoneLeft, spawnZoneRight, spawnZoneUp, targetDirection;

        Vector2 posZoneLeft, posZoneRight, posZoneUp, vSpeed;

        //public Vector2 Pos { get; set; }

        //public Vector2 Direction { get; private set; }

        Random rnd;

        public bool IsOutOfPlay { get; private set; }

        public Vector2 Velocity { get; private set; }

        public int Health { get; set; }

        public int Radius { get; private set; }

        Color color = new Color(); // test
        // float rotation;

        public Asteroid(GameWindow window, Random rnd, Vector2 pos) : base(pos)
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

            Pos = asteroidSpawnPoints[rnd.Next(asteroidSpawnPoints.Count)];

            asteroidSpawnPoints.Clear();

            targetDirection = new Rectangle(window.ClientBounds.Width / 2, window.ClientBounds.Height / 2, 1, window.ClientBounds.Height / 3);

            hitbox = new Rectangle((int)pos.X, (int)pos.Y, tex.Width, tex.Height);

             vSpeed = new Vector2(rnd.Next(1, 10), rnd.Next(1, 10));
            //vSpeed = Vector2.Zero;

            Direction = GetDirection();

            Radius = 50;

            Velocity = vSpeed * Direction;

            Health = 2;

            color = Color.White;
            // GetAsteroidMass();
        }

        public override void Update(GameTime gt)
        {
            Pos += Velocity;
            hitbox.X = (int)Pos.X;
            hitbox.Y = (int)Pos.Y;

            if (Pos.X < -spawnZoneLeft.Width || Pos.X > window.ClientBounds.Width + spawnZoneRight.Width
                || Pos.Y < -spawnZoneUp.Height || Pos.Y > window.ClientBounds.Height)
            {
                color = Color.Red;
                Velocity = new Vector2(0, 0);
                IsOutOfPlay = true;
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
            Vector2 Direction = new Vector2(targetDirection.X - Pos.X, targetDirection.Y - Pos.Y);
            return Vector2.Normalize(Direction);
        }
        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(tex, Pos, null, color, 0, new Vector2(tex.Width / 2, tex.Height / 2), 1, SpriteEffects.None, 1);
        }
    }
}
