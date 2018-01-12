using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids_2_Return_of_the_Asteroids
{
    class Weapon:GameObject
    {
        protected Vector2 projectileTargetPos;
        protected ProjectileBase projectile;
        public List<ProjectileBase> projectiles = new List<ProjectileBase>();

        protected double gunCooldownTimer, gunCooldownTimerReset;
        protected float gunRateOfFire;

        protected double gunChargeTimer, gunChargeTimerReset;
        protected float chargeRate;
        protected int maxGunCharge;
        protected int currentGunCharge;        

        public Weapon(Vector2 pos):base(pos)
        {
            
        }

        virtual protected void UpdateProjectiles(GameTime gt)
        {
            for (int i = 0; i < projectiles.Count; i++)
            {
                projectiles[i].Update(gt);
                if (projectiles[i].doRemove)
                {
                    projectiles.RemoveAt(i);
                }
            }
        }

        virtual protected List<ProjectileBase> GetProjectileList()
        {
            return projectiles;
        }

        public virtual void ClearProjectileList()
        {
            projectiles.Clear();
        }

        public virtual void SetPos(Vector2 newPos)
        {
            pos = newPos;
        }
    }
}
