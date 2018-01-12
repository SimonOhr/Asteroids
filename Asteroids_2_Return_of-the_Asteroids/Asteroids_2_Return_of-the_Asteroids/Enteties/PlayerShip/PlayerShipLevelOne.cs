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
    class PlayerShipLevelOne:PlayerShipBase
    {
        //ProjectileBase projectile;        
        //Vector2 projectileTargetPos;

        //double gunCooldownTimer, gunCooldownTimerReset;
        //float gunRateOfFire;

        //double gunChargeTimer, gunChargeTimerReset;
        //float chargeRate;
        //int maxGunCharge;
        //int currentGunCharge;
        public PlayerShipLevelOne(Vector2 pos, Vector2 mousePos):base(pos, mousePos)
        {
            hitbox = new Rectangle((int)pos.X, (int)pos.Y, 50, 50);

            tex = AssetsManager.shipTex;

            EffectsManager.CreateAfterBurnerEffect(Pos);

            weapon = new LaserCanon(Pos);

            hitPoints = 2;
            speed = 3;
            originalSpeed = speed;

            //gunRateOfFire = 200f;
            //gunCooldownTimer = 0;
            //gunCooldownTimerReset = 0;

            //chargeRate = 800f;
            //gunChargeTimerReset = 0;
            //maxGunCharge = 3;
        }

        public override void Update(GameTime gt)
        {
            // UpdateGunData(gt); 
            weapon.SetPos(Pos);
            weapon.Update(gt);
            MovementInput();
            base.Update(gt);
        }

        protected override void MovementInput()
        {

            if (Vector2.Distance(mousePos, Pos) < 4)
            {
                speed = 0;
            }
            else if (Vector2.Distance(mousePos, Pos) >= 4)
            {
                speed = originalSpeed;
            }           
        }
        //virtual protected void UpdateGunData(GameTime gt)
        //{
        //    GunCharging(gt);

        //    if (Mouse.GetState().LeftButton == ButtonState.Pressed && currentGunCharge > 0)
        //    {
        //        projectileTargetPos = Mouse.GetState().Position.ToVector2();
        //        PlayerIsShooting(gt);
        //    }
        //    if (Mouse.GetState().LeftButton == ButtonState.Released)
        //    {
        //        gunCooldownTimer = 500f;
        //    }
        //    UpdateProjectiles(gt);
        //}

        //virtual protected void GunCharging(GameTime gt)
        //{
        //    if (currentGunCharge < 3)
        //    {
        //        gunChargeTimer += gt.ElapsedGameTime.TotalMilliseconds;

        //        if (gunChargeTimer > chargeRate)
        //        {
        //            currentGunCharge++;
        //            gunChargeTimer = gunChargeTimerReset;
        //            // Console.WriteLine("number of shots " + currentGunCharge);
        //        }
        //    }
        //}

        //virtual protected void PlayerIsShooting(GameTime gt)
        //{
        //    gunCooldownTimer += gt.ElapsedGameTime.TotalMilliseconds;

        //    if (gunCooldownTimer > gunRateOfFire)
        //    {
        //        projectile = new LaserProjectile(Pos, projectileTargetPos); // create weapon
        //        projectiles.Add(projectile);
        //        currentGunCharge--;
        //        gunCooldownTimer = gunCooldownTimerReset;
        //        // Console.WriteLine("number of shots " + projectiles.Count);                
        //        SoundManager.PlayShot();
        //    }
        //}

        public override void Draw(SpriteBatch sb)
        {
            weapon.Draw(sb);
            base.Draw(sb);
        }
    }
}
