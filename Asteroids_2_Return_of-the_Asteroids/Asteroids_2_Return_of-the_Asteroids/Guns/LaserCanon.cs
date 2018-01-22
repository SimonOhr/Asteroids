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
    class LaserCanon:WeaponBase
    {
        private bool inRange;
        private Vector2 targetPos;
        public LaserCanon(Vector2 pos):base(pos)
        {            
            gunRateOfFire = 200f;
            gunCooldownTimer = 0;
            gunCooldownTimerReset = 0;

            chargeRate = 700f;
            gunChargeTimerReset = 0;
            maxGunCharge = 5;
        }

        public override void Update(GameTime gt)
        {
            UpdateGunData(gt);
            base.Update(gt);
        }

        virtual protected void UpdateGunData(GameTime gt)
        {
            GunCharging(gt);

            if (Mouse.GetState().LeftButton == ButtonState.Pressed && currentGunCharge > 0)
            {
                projectileTargetPos = Mouse.GetState().Position.ToVector2();
                isShooting(gt);
            }
            else if (Mouse.GetState().LeftButton == ButtonState.Released)
            {
                gunCooldownTimer = 500f;
            }

            if(inRange && currentGunCharge > 0)
            {
                projectileTargetPos = targetPos;
                isShooting(gt);
            }
            UpdateProjectiles(gt);
        }

        virtual protected void GunCharging(GameTime gt)
        {
            if (currentGunCharge < 3)
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

        virtual protected void isShooting(GameTime gt)
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
            }
        }

        public void TargetPos(Vector2 target)
        {
            targetPos = target;
        }

        public void IsInRange(bool temp)
        {
            inRange = temp;
        }

        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
        }
    }
}
