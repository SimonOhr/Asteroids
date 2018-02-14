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
    class LaserCanon : WeaponBase
    {
        bool shoot;
        public LaserCanon(Vector2 pos) : base(pos)
        {
            gunRateOfFire = 300f;
            gunCooldownTimer = 300;
            gunCooldownTimerReset = 0;

            chargeRate = 700f;
            gunChargeTimerReset = 0;
            maxGunCharge = 3;
        }

        public override void Update(GameTime gt)
        {
            UpdateGunData(gt);
            base.Update(gt);
        }

        private void UpdateGunData(GameTime gt)
        {
            GunCharging(gt);

            if (currentGunCharge > 0 && shoot)
            {               
                isShooting(gt);
            }
            
            UpdateProjectiles(gt);
        }

        private void GunCharging(GameTime gt)
        {
            if (currentGunCharge < maxGunCharge)
            {
                gunChargeTimer += gt.ElapsedGameTime.TotalMilliseconds;

                if (gunChargeTimer > chargeRate)
                {
                    currentGunCharge++;
                    gunChargeTimer = gunChargeTimerReset;
                    // Console.WriteLine("number of shots " + currentGunCharge);
                }
            }
        }

        public override void isShooting(GameTime gt)
        {
            gunCooldownTimer += gt.ElapsedGameTime.TotalMilliseconds;

            if (gunCooldownTimer > gunRateOfFire)
            {
                projectile = new LaserProjectile(pos, projectileTargetPos); // create weapon
                projectiles.Add(projectile);
                currentGunCharge--;
                gunCooldownTimer = gunCooldownTimerReset;
                // Console.WriteLine("number of shots " + projectiles.Count);                
                SoundManager.PlayShot();
                shoot = false;
            }
        }

        public override void SetTargetPos(Vector2 t)
        {
            projectileTargetPos = t;
        }

        public override void Shoot(bool temp)
        {
            shoot = temp;
        }

        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
        }
    }
}
