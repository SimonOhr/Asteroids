using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids_2_Return_of_the_Asteroids
{
    class CollisonManager
    {
        GameplayManager gm;
        List<ProjectileBase> projectiles;
        List<Asteroid> asteroids;

        public bool TempInvulnarbility { get; private set; } = false;
        double tempTimer, tempReset = 0;
        float tempTarget = 2;

        public CollisonManager(GameplayManager gm)
        {
            this.gm = gm;
            projectiles = new List<ProjectileBase>();
            asteroids = new List<Asteroid>();
        }

        public void CheckIfAsteroidInPlay()
        {
            asteroids = gm.GetAsteroidList();
            for (int i = 0; i < asteroids.Count - 1; i++)
            {
                if (asteroids[i].IsOutOfPlay) asteroids.RemoveAt(i);
            }
        }
        /// <summary>
        /// also handles projectiles colliding with asteroids
        /// </summary>
        public void CheckIfAsteroidIsHit()
        {
            //if (gm.GetProjectileList() != null)
            projectiles = gm.GetProjectileList();

            asteroids = gm.GetAsteroidList();

            if (projectiles.Count > 0 && projectiles.Count > 0)
            {
                for (int j = 0; j < projectiles.Count; j++)
                {
                    for (int i = 0; i < asteroids.Count; i++)
                    {
                        if (Vector2.Distance(asteroids[i].Pos, projectiles[j].Pos) <= 50)
                        {
                            AsteroidIsHitCollisionhandler(asteroids[i], projectiles[j],i, j);
                            break;
                        }
                    }
                }
            }
        }
        private void AsteroidIsHitCollisionhandler(Asteroid asteroid, ProjectileBase projectile,int collidingAsteroidId, int collidingProjectileId)
        {
            CreateHitEffect(asteroid.Pos);

            RemoveCollidedProjectile(collidingProjectileId);

            if (CheckIfAsteroidisDead(asteroid))
            {
                CreateAsteroidExplosionEffect(asteroid.Pos);

                PlayerAsteroidExplosionSound();

                RemoveDeadAsteroid(collidingAsteroidId);                
            }           
        }
        private void CreateHitEffect(Vector2 targetPos)
        {
            EffectsManager.CreateAsteroidIsHitEffect(targetPos);
        }
        private void RemoveCollidedProjectile(int projectileId)
        {
            projectiles.RemoveAt(projectileId);
        }

        private bool CheckIfAsteroidisDead(Asteroid a)
        {
            a.Health -= 1;
            if (a.Health <= 0)
            {
                return true;
            }
            return false;
        }

        private void CreateAsteroidExplosionEffect(Vector2 targetPos)
        {
            EffectsManager.CreateAsteroidExplosionEffect(targetPos);
        }       

        private void PlayerAsteroidExplosionSound()
        {
            SoundManager.PlayExplosion();
        }

        private void RemoveDeadAsteroid(int asteroidId)
        {
            asteroids.RemoveAt(asteroidId);
        }

        private void AddToScore()
        {
            Game1.score += 10;
        }

        public void CheckIfShipIsHit(GameTime gt)
        {
            if (TempInvulnarbility)
            {
                tempTimer += gt.ElapsedGameTime.TotalSeconds;
                if (tempTimer >= tempTarget)
                {
                    TempInvulnarbility = false;
                    gm.SetShipStatus(false);
                    tempTimer = tempReset;
                }
            }
            else if (!TempInvulnarbility)
            {
                for (int i = 0; i < asteroids.Count; i++)
                {
                    if (Vector2.Distance(asteroids[i].Pos, gm.GetShipPos()) < asteroids[i].Radius + (gm.GetShipTex().Width / 2))
                    {
                        TempInvulnarbility = true;
                        gm.SetShipStatus(true);
                        gm.UpdatePlayerHealth(-1);
                    }
                }
            }
        }
    }
}
