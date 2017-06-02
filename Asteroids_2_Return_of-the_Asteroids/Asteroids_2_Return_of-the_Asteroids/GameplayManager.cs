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
        public List<Asteroid> asteroids = new List<Asteroid>();

        public Ship Ship { get; private set; }

        Projectile projectile;
         public List<Projectile> projectiles = new List<Projectile>();

        Random rnd;

        double spawnAsteroidsTimer, spawnAsteroidsTimerReset;
        float spawnAsteroidsInterval;
        int numberOfAsteroidsPerTimerReset;

        Vector2 projectileTargetPos;
        double gunCooldownTimer, gunCooldownTimerReset;
        float gunRateOfFire;

        double gunChargeTimer, gunChargeTimerReset;
        float chargeRate;
        int maxGunCharge;     
        int currentGunCharge;

        Vector2 mousePos;

        public bool TempInvulnarbility { get; private set; } = false;
        double tempTimer, tempReset = 0;
        float tempTarget = 2;

        ParticleEngine asteroidExplosion;
        List<ParticleEngine> explosionList;

        double explosionTimer, explosionReset;
        float explosionTargetLength;

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
            gunCooldownTimer = 0;
            gunCooldownTimerReset = 0;

            chargeRate = 800f;
            gunChargeTimerReset = 0;
            maxGunCharge = 3;

            explosionList = new List<ParticleEngine>();

            explosionReset = 0;
            explosionTargetLength = 200f;
        }

        private void CreatePlayerShip()
        {
            Ship = new Ship(new Vector2(50, 50), mousePos);
        }

        public void Update(GameTime gt)
        {
            mousePos.X = Mouse.GetState().Position.X;
            mousePos.Y = Mouse.GetState().Position.Y;

            GunCharging(gt);

            if (Mouse.GetState().LeftButton == ButtonState.Pressed && currentGunCharge > 0)
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
            Ship.Update(gt);


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

            if (explosionList.Count > 0)
            {
                ExplosionTime(gt);
                foreach (ParticleEngine explosion in explosionList)
                {
                    explosion.Update();
                }
            }

            //CheckAsteroidCollision();

            CheckIfAsteroidIsHit();

            CheckIfShipIsHit(gt);
        }

        private void GunCharging(GameTime gt)
        {
            if (currentGunCharge < 3)
            {
                gunChargeTimer += gt.ElapsedGameTime.TotalMilliseconds;

                if (gunChargeTimer > chargeRate)
                {
                    currentGunCharge++;
                    gunChargeTimer = gunChargeTimerReset;
                    Console.WriteLine("number of shots " + currentGunCharge);
                }
            }
        }
        private void PlayerIsShooting(GameTime gt)
        {
            gunCooldownTimer += gt.ElapsedGameTime.TotalMilliseconds;

            if (gunCooldownTimer > gunRateOfFire)
            {
                projectile = new Projectile(Ship.ShipPos, projectileTargetPos);
                projectiles.Add(projectile);
                currentGunCharge--;
                gunCooldownTimer = gunCooldownTimerReset;
                Console.WriteLine("number of shots " + projectiles.Count);
            }
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

            Console.WriteLine(+asteroids.Count);
        }

        private void CheckIfAsteroidIsHit()
        {
            for (int j = 0; j < projectiles.Count; j++)
            {
                for (int i = 0; i < asteroids.Count; i++)
                {
                    if (projectiles.Count > 0)
                    {
                        if (Vector2.Distance(asteroids[i].asteroidPos, projectiles[j].projectilePos) < 50)
                        {
                            asteroidExplosion = new ParticleEngine(AssetsManager.textures, asteroids[i].asteroidPos, false, true);
                            asteroidExplosion.EmitterLocation = asteroids[i].asteroidPos;
                            explosionList.Add(asteroidExplosion);
                                                     
                            projectiles.RemoveAt(j);
                            asteroids.RemoveAt(i);   
                                                     
                            Game1.score += 10;

                            break;
                        }
                    }
                }
            }
        }

        private void ExplosionTime(GameTime gt)
        {
            explosionTimer += gt.ElapsedGameTime.TotalMilliseconds;

            if (explosionTimer > explosionTargetLength)
            {
                explosionList.RemoveAt(0);
                explosionTimer = explosionReset;               
            }
        }
        //private void CheckAsteroidCollision()
        //{
        //    for (int i = 0; i < asteroids.Count; i++)
        //    {
        //        Asteroid enemy1 = asteroids[i];
        //        for (int j = i + 1; j < asteroids.Count; j++)
        //        {
        //            Asteroid enemy2 = asteroids[j];
        //            if (Vector2.Distance(asteroids[i].asteroidPos, asteroids[j].asteroidPos) < asteroid.AsteroidRadius * 2)
        //            {
        //                Vector2 collisionNormal = asteroids[i].asteroidPos - asteroids[j].asteroidPos;
        //                collisionNormal.Normalize();
        //                Vector2 collisionDirection = new Vector2(-collisionNormal.Y, collisionNormal.X);

        //                Vector2 v1Parallel = Vector2.Dot(collisionNormal, asteroids[i].velocity) * collisionNormal;
        //                Vector2 v1Ortho = Vector2.Dot(collisionDirection, asteroids[i].velocity) * collisionDirection;
        //                Vector2 v2Parallel = Vector2.Dot(collisionNormal, asteroids[j].velocity) * collisionNormal;
        //                Vector2 v2Ortho = Vector2.Dot(collisionDirection, asteroids[j].velocity) * collisionDirection;

        //                var v1Length = v1Parallel.Length();
        //                var v2Length = v2Parallel.Length();
        //                var commonVelocity = (((asteroids[i].mass) * v1Length) + (asteroids[j].mass * v2Length)) / ((asteroids[i].mass) + (asteroids[j].mass));
        //                var v1LengthAfterCollision = commonVelocity - v1Length;
        //                var v2LengthAfterCollision = commonVelocity - v2Length;
        //                v1Parallel = v1Parallel * (v1LengthAfterCollision / v1Length);
        //                v2Parallel = v2Parallel * (v2LengthAfterCollision / v2Length);

        //                asteroids[i].velocity = v1Parallel + v1Ortho;
        //                asteroids[j].velocity = v2Parallel + v2Ortho;
        //            }
        //        }
        //    }
        //}
        private void CheckIfAsteroidIsInPlay()
        {
            for (int i = 0; i < asteroids.Count; i++)
            {
                if (asteroids[i].isOutOfPlay)
                {
                    asteroids.RemoveAt(i);
                }
            }
        }

        private void CheckIfShipIsHit(GameTime gt)
        {
            if (TempInvulnarbility)
            {
                tempTimer += gt.ElapsedGameTime.TotalSeconds;
                if (tempTimer >= tempTarget)
                {
                    TempInvulnarbility = false;
                    Ship.isHit = false;
                    tempTimer = tempReset;
                }
            }
            else if (!TempInvulnarbility)
            {
                for (int i = 0; i < asteroids.Count; i++)
                {
                    if (Vector2.Distance(asteroids[i].asteroidPos, Ship.ShipPos) < asteroids[i].AsteroidRadius + (Ship.ShipTex.Width / 2))
                    {

                        TempInvulnarbility = true;
                        Ship.isHit = true;
                        Ship.hitPoints -= 1;
                    }
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

            Ship.Draw(sb);
            if (explosionList.Count > 0)
            {
                asteroidExplosion.Draw(sb);
            }
          
            foreach (Asteroid tempAsteroid in asteroids)
            {
                tempAsteroid.Draw(sb);
            }
        }
    }
}
