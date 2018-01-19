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
    class MiniMissileLauncher:WeaponBase
    {
        public MiniMissileLauncher(Vector2 pos) : base(pos)
        {
            gunRateOfFire = 200f;
            gunCooldownTimer = 0;
            gunCooldownTimerReset = 0;

            chargeRate = 100f;
            gunChargeTimerReset = 0;
            maxGunCharge = 10;
        }

        public override void Update(GameTime gt)
        {
            UpdateGunData(gt);
            base.Update(gt);
        }

        virtual protected void UpdateGunData(GameTime gt)
        {
            GunCharging(gt);

            if (Mouse.GetState().RightButton == ButtonState.Pressed && currentGunCharge > 0)
            {
                projectileTargetPos = Mouse.GetState().Position.ToVector2();
                PlayerIsShooting(gt);
            }
            if (Mouse.GetState().RightButton == ButtonState.Released)
            {
                gunCooldownTimer = 500f;
            }
            UpdateProjectiles(gt);
        }

        virtual protected void GunCharging(GameTime gt)
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

        virtual protected void PlayerIsShooting(GameTime gt)
        {
            gunCooldownTimer += gt.ElapsedGameTime.TotalMilliseconds;

            if (gunCooldownTimer > gunRateOfFire)
            {
                projectile = new MiniMissileProjectile(pos, projectileTargetPos); // create weapon
                projectiles.Add(projectile);
                currentGunCharge--;
                gunCooldownTimer = gunCooldownTimerReset;
                // Console.WriteLine("number of shots " + projectiles.Count);                
                SoundManager.PlayShot();
            }
        }

        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
        }
    }
}

