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

        public void CheckIfAsteroidIsHit()
        {
            //if (gm.GetProjectileList() != null)
            projectiles = gm.GetProjectileList();           
            
            asteroids = gm.GetAsteroidList();
            for (int j = 0; j < projectiles.Count; j++)
            {
                for (int i = 0; i < asteroids.Count; i++)
                {
                    if (projectiles.Count > 0)
                    {
                        if (Vector2.Distance(asteroids[i].pos, projectiles[j].pos) < 50)
                        {
                            EffectsManager.CreateASteroidIsHitEffect(asteroids[i].pos);
                            projectiles.RemoveAt(j);
                            if (CheckIfAsteroidisDead(asteroids[i]))
                            {
                                EffectsManager.CreateAsteroidExplosionEffect(asteroids[i].pos);
                                SoundManager.PlayExplosion();
                                asteroids.RemoveAt(i);
                            }
                            Game1.score += 10;
                            break;
                        }
                    }
                }
            }            
        }

        private bool CheckIfAsteroidisDead(Asteroid a)
        {
            a.hitPoints -= 1;
            if (a.hitPoints <= 0)
            {
                return true;
            }
            return false;
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
                    if (Vector2.Distance(asteroids[i].pos, gm.GetShipPos()) < asteroids[i].radius + (gm.GetShipTex().Width / 2))
                    {
                        TempInvulnarbility = true;
                        gm.SetShipStatus(true);
                        PlayerShipBase.hitPoints -= 1;
                    }
                }
            }
        }
    }
}
