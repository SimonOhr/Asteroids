using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids_2_Return_of_the_Asteroids
{
    class WeaponBase:GameObject
    {
        protected Vector2 projectileTargetPos;
        protected ProjectileBase projectile;
        static protected List<ProjectileBase> projectiles = new List<ProjectileBase>();

        protected double gunCooldownTimerReset;
        protected float gunRateOfFire;
        public double gunCooldownTimer;

        protected double gunChargeTimer;
        protected double gunChargeTimerReset;
        protected float chargeRate;
        protected int maxGunCharge;
        protected int currentGunCharge;
        protected bool shoot;
        public String Name { get; set; }       
        public Vector2 targetPos { get; set; }

        public WeaponBase(Vector2 pos):base(pos)
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

        public virtual void isShooting(GameTime gt)
        { }

        public static ref List<ProjectileBase> GetProjectileList()
        {            
            return ref projectiles;
        }

        public static void ClearProjectileList()
        {
            projectiles.Clear();
        }

        public virtual void SetPos(Vector2 newPos)
        {
            Pos = newPos;
        }

        public virtual void SetTargetPos(Vector2 t)
        {
            projectileTargetPos = t;
        }

        public virtual int GetGuncharge()
        {
            return currentGunCharge;
        }      

        public virtual void Shoot(bool temp)
        {
            shoot = temp;
        }

        public bool canShoot()
        {
            if (currentGunCharge > 0)
                return true;
            return false;
        }
    }
}
