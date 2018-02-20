using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids_2_Return_of_the_Asteroids
{
    class GameplayManager
    {
        // WeaponBase weapon;
        GameWindow window;
        
        CollisonManager cm;
        DefaultSpaceStation st;
        Pirate pirateTEST;
        Asteroid asteroid;
        public static List<Asteroid> asteroids = new List<Asteroid>();

        public PlayerShip Ship { get; private set; }

        List<ProjectileBase> projectiles = new List<ProjectileBase>();

        Random rnd;

        double spawnAsteroidsTimer, spawnAsteroidsTimerReset;
        float spawnAsteroidsInterval;
        int numberOfAsteroidsPerTick;

        Vector2 cursorPosition;       

        //ParticleEngine particle;

        Vector2 spaceStationPos;

        public GameplayManager(Random rnd, GameWindow window)
        {
            this.window = window;
            this.rnd = rnd;
            

            spaceStationPos = new Vector2(300, 300);
                              
            CreatePlayerShip();
            CreateSpaceStation(spaceStationPos);

            spawnAsteroidsTimerReset = 0;
            spawnAsteroidsInterval = 2;
            numberOfAsteroidsPerTick = 3;

            cm = new CollisonManager(this);

            SoundManager.PlayBgMusic();

            pirateTEST = new Pirate(new Vector2(window.ClientBounds.Width / 2, window.ClientBounds.Height / 2), Ship);
        }        

        private void CreatePlayerShip()
        {
            Ship = new PlayerShip(new Vector2(50, 50));
        }

        private void CreateSpaceStation(Vector2 pos)
        {
            st = new DefaultSpaceStation(pos);
        }

        public void Update(GameTime gt)
        {                        
            cursorPosition = KeyMouseReader.CursorViewToWorldPosition;
            GUI.UpdateGUIMatrix();
            CreateAsteroids(gt);
            //CheckIfAsteroidIsInPlay();
            Ship.Update(gt);
            st.Update(gt);           
            foreach (Asteroid tempAsteroid in asteroids)
            {
                tempAsteroid.Update(gt);
            }

            EffectsManager.UpdateAsteroidIsHitEffect();
            EffectsManager.UpdateAsteroidExplosionEffect();
            cm.CheckIfAsteroidIsHit();
            cm.CheckIfAsteroidInPlay();
            cm.CheckIfShipIsHit(gt);
            pirateTEST.Update(gt);
        }       
        
        private void CreateAsteroids(GameTime gt)
        {
            spawnAsteroidsTimer += gt.ElapsedGameTime.TotalSeconds;

            if (spawnAsteroidsTimer > spawnAsteroidsInterval && asteroids.Count < 30)
            {
                for (int i = 0; i < numberOfAsteroidsPerTick; i++)
                {
                    asteroid = new Asteroid(window, rnd, Vector2.Zero);
                    asteroids.Add(asteroid);
                }
                spawnAsteroidsTimer = spawnAsteroidsTimerReset;
            }
            //  Console.WriteLine(+asteroids.Count);
        }

        //private void ExplosionTime(GameTime gt)
        //{
        //    explosionTimer += gt.ElapsedGameTime.TotalMilliseconds;

        //    if (explosionTimer > explosionTargetLength)
        //    {               
        //        //    explosionList.RemoveAt(0);
        //        //    explosionTimer = explosionReset;               
        //    }
        //  }
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

        //private void CheckIfAsteroidIsInPlay()
        //{
        //    for (int i = 0; i < asteroids.Count; i++)
        //    {
        //        if (asteroids[i].isOutOfPlay)
        //        {
        //            asteroids.RemoveAt(i);
        //        }
        //    }
        //}

        public void Draw(SpriteBatch sb)
        {
            st.Draw(sb);

            sb.Draw(AssetsManager.crosshairTex, new Vector2(cursorPosition.X - (AssetsManager.crosshairTex.Width / 2), cursorPosition.Y - (AssetsManager.crosshairTex.Height / 2)), Color.White);

            if (GetProjectileList() != null)
                foreach (ProjectileBase tempProjectile in GetProjectileList())
                {
                    tempProjectile.Draw(sb);
                }

            EffectsManager.Draw(sb);

            pirateTEST.Draw(sb);
            Ship.Draw(sb);
            GUI.DrawHealthBar(sb, GetPlayerShipHealth());
            foreach (Asteroid tempAsteroid in asteroids)
            {
                tempAsteroid.Draw(sb);
            }
        }

        public ref List<WeaponBase> GetWeaponeList()
        {
            return ref Ship.GetWeaponList();
        }

        public ref List<ProjectileBase> GetProjectileList()
        {
            return ref WeaponBase.GetProjectileList();
        }

        public void ClearProjectileList()
        {
            WeaponBase.ClearProjectileList();
        }

        public ref List<Asteroid> GetAsteroidList()
        {
            return ref asteroids;
        }

        public void SetShipStatus(bool isShipHit)
        {
            Ship.isHit = isShipHit;
        }

        public Vector2 GetShipPos()
        {
            return Ship.Pos;
        }

        public Texture2D GetShipTex()
        {
            return Ship.Tex;
        } 
        
        public int GetPlayerShipHealth()
        {
            return Ship.GetHealth();
        }

        public int GetPlayerShipMaxHealth()
        {
            return Ship.GetMaxHealth();
        }

        public int GetPirateHealth()
        {
            return pirateTEST.GetHealth();
        }

        public void UpdatePlayerHealth(int deltaHealth)
        {
            Ship.SetHealth(deltaHealth);
        }

        public void UpdatePirateHealth(int deltaHealth)
        {
            pirateTEST.SetHealth(deltaHealth);
        }
    }
}
