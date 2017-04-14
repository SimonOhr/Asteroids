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
    class GameplayManager
    {
        GameWindow window;

        Rectangle backgroundRec;

        Asteroid asteroid;
        List<Asteroid> asteroids = new List<Asteroid>();

        Ship ship;

        Projectile projectile;
        List<Projectile> projectiles = new List<Projectile>();

        Random rnd;

        double spawnAsteroidsTimer, spawnAsteroidsTimerReset;
        float spawnAsteroidsInterval;
        int numberOfAsteroidsPerTimerReset;

        Vector2 projectileTargetPos;
        double gunCooldownTimer, gunCooldownTimerReset;
        float gunRateOfFire;

        Vector2 mousePos;

        public int asteroidRadius;        

        public GameplayManager(Random rnd, GameWindow window)
        {
            this.window = window;
            this.rnd = rnd;

            backgroundRec = new Rectangle(0, 0, window.ClientBounds.Width, window.ClientBounds.Height);

            CreatePlayerShip();

            spawnAsteroidsTimerReset = 0;
            spawnAsteroidsInterval = 2;
            numberOfAsteroidsPerTimerReset = 3;

            gunRateOfFire = 200f;
            gunCooldownTimer = 500;
            gunCooldownTimerReset = 0;

            asteroidRadius = 50;
        }

        private void CreatePlayerShip()
        {
            ship = new Ship(new Vector2(50, 50), mousePos);
        }

        private void CreateAsteroids(GameTime gt)
        {
            spawnAsteroidsTimer += gt.ElapsedGameTime.TotalSeconds;

            if (spawnAsteroidsTimer > spawnAsteroidsInterval)
            {
                for (int i = 0; i < numberOfAsteroidsPerTimerReset; i++)
                {
                    asteroid = new Asteroid(window, rnd);
                    asteroids.Add(asteroid);
                }

                spawnAsteroidsTimer = spawnAsteroidsTimerReset;
            }

           // Console.WriteLine(+asteroids.Count);
        }

        public void Update(GameTime gt)
        {
            mousePos.X = Mouse.GetState().Position.X;
            mousePos.Y = Mouse.GetState().Position.Y;

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                projectileTargetPos = Mouse.GetState().Position.ToVector2();
                PlayerIsShooting(gt);                                         
            }
            if (Mouse.GetState().LeftButton == ButtonState.Released)
            {
                gunCooldownTimer = 500f;
            }
            CreateAsteroids(gt);
            CheckIfAsteroidIsInPlay();
            ship.Update(gt);

            for (int i = 0; i < projectiles.Count; i++)
            {
                projectiles[i].Update(gt);
                if (projectiles[i].removeProjectile)
                {
                    projectiles.RemoveAt(i);
                }
            }
            foreach (Asteroid tempAsteroid in asteroids)
            {
                tempAsteroid.Update(gt);
            }

            CheckAsteroidCollision();
            
            CheckIfAsteroidIsHit();
        }

        private void PlayerIsShooting(GameTime gt)
        {
            gunCooldownTimer += gt.ElapsedGameTime.TotalMilliseconds;

            if ( gunCooldownTimer > gunRateOfFire)
            {
                projectile = new Projectile(ship.shipPos, projectileTargetPos);
                projectiles.Add(projectile);
                gunCooldownTimer = gunCooldownTimerReset;
                Console.WriteLine("number of shots " + projectiles.Count);
            }
                        
        }

        private void CheckIfAsteroidIsHit()
        {
            for (int i = 0; i < asteroids.Count-1; i++)
            {
                for (int j = 0; j < projectiles.Count; j++)
                {
                    if (asteroids[i] == null )
                    {
                        break;
                    }
                    if (Vector2.Distance(asteroids[i].asteroidPos, projectiles[j].projectilePos) < 50)
                    {
                        asteroids.RemoveAt(i);
                        projectiles.RemoveAt(j);                       
                    }
                }
            }
        }

        private void CheckAsteroidCollision()
        {
            for (int i = 0; i < asteroids.Count - 1; i++)
            {
                Asteroid enemy1 = asteroids[i];
                for (int j = i + 1; j < asteroids.Count; j++)
                {
                    Asteroid enemy2 = asteroids[j];
                    if (Vector2.Distance(asteroids[i].asteroidPos, asteroids[j].asteroidPos) < asteroidRadius * 2)
                    {
                        Vector2 collisionNormal = asteroids[i].asteroidPos - asteroids[j].asteroidPos;
                        collisionNormal.Normalize();
                        Vector2 collisionDirection = new Vector2(-collisionNormal.Y, collisionNormal.X);

                        Vector2 v1Parallel = Vector2.Dot(collisionNormal, asteroids[i].velocity) * collisionNormal;
                        Vector2 v1Ortho = Vector2.Dot(collisionDirection, asteroids[i].velocity) * collisionDirection;
                        Vector2 v2Parallel = Vector2.Dot(collisionNormal, asteroids[j].velocity) * collisionNormal;
                        Vector2 v2Ortho = Vector2.Dot(collisionDirection, asteroids[j].velocity) * collisionDirection;

                        var v1Length = v1Parallel.Length(); // ()
                        var v2Length = v2Parallel.Length();
                        var commonVelocity = (((asteroids[i].mass) * v1Length) + (asteroids[j].mass * v2Length)) / ((asteroids[i].mass) + (asteroids[j].mass));
                        var v1LengthAfterCollision = commonVelocity - v1Length;
                        var v2LengthAfterCollision = commonVelocity - v2Length;
                        v1Parallel = v1Parallel * (v1LengthAfterCollision / v1Length);
                        v2Parallel = v2Parallel * (v2LengthAfterCollision / v2Length);

                        asteroids[i].velocity = v1Parallel + v1Ortho;
                        asteroids[j].velocity = v2Parallel + v2Ortho;                      
                    }
                }
            }
        }
        private void CheckIfAsteroidIsInPlay()
        {
            for (int i = 0; i < asteroids.Count; i++)
            {
                if (asteroids[i].isOutOfPlay)
                {
                    asteroids.RemoveAt(i);
                    // Console.WriteLine("size of list " + asteroids.Count);
                }
            }
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(AssetsManager.backgroundTex, backgroundRec, Color.White);

            sb.Draw(AssetsManager.crosshairTex, mousePos, Color.White);

            foreach (Projectile tempProjectile in projectiles)
            {
                tempProjectile.Draw(sb);
            }

            ship.Draw(sb);

            foreach (Asteroid tempAsteroid in asteroids)
            {
                tempAsteroid.Draw(sb);
            }                      
        }
    }
}
