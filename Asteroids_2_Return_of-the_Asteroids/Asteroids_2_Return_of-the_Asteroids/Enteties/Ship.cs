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
    class Ship
    {        
        Vector2 mousePos;
        Color color;
        public Texture2D Tex { get; private set; }       
        public Vector2 Pos { get; private set; }
        Rectangle hitbox;

        public static int hitPoints;
        public bool isHit;

       // public int DamageOutput { get; private set; }

        float speed;
      
        ParticleEngine afterburnerEffect;
        List<Texture2D> afterBurnerTextures;
                
        float currentRotation;

        Projectile projectile;
        public List<Projectile> projectiles = new List<Projectile>();
        Vector2 projectileTargetPos;
        double gunCooldownTimer, gunCooldownTimerReset;
        float gunRateOfFire;

        double gunChargeTimer, gunChargeTimerReset;
        float chargeRate;
        int maxGunCharge;
        int currentGunCharge;

        public Ship(Vector2 pos, Vector2 mousePos)
        {
            this.Pos = pos;
            this.mousePos = mousePos;           

            color = Color.White;
            Tex = AssetsManager.shipTex;           
            hitbox = new Rectangle((int)pos.X, (int)pos.Y, 50, 50);
            hitPoints = 2;
            speed = 3;

            gunRateOfFire = 200f;
            gunCooldownTimer = 0;
            gunCooldownTimerReset = 0;

            chargeRate = 800f;
            gunChargeTimerReset = 0;
            maxGunCharge = 3;

            //afterBurnerTextures = new List<Texture2D>();
            //afterBurnerTextures.Add(AssetsManager.particleCircleTex);
            //afterBurnerTextures.Add(AssetsManager.particleDiamondTex);

            //afterburnerEffect = new ParticleEngine(afterBurnerTextures, pos, TypeOfEffect.AfterBurner);

            //afterBurnerTextures.Clear();
            //afterBurnerTextures = null;
            EffectsManager.CreateAfterBurnerEffect(Pos);
        }
        public void Update(GameTime gt)
        {
            hitbox.X = (int)Pos.X;
            hitbox.Y = (int)Pos.Y;

            mousePos.X = Mouse.GetState().X;
            mousePos.Y = Mouse.GetState().Y;

            MovementInput();
            ShipRotation();
            GetDirection();

            Pos += speed * GetDirection();

            if (hitPoints <= 0)
            {
                color = Color.Blue;
            }

            EffectsManager.UpdateAfterBurnerEffect(Pos);
            //afterburnerEffect.EmitterLocation = Pos + speed * GetDirection();
            //afterburnerEffect.Update();
            UpdateGunData(gt);

        }

        private void MovementInput()
        {

            if (Vector2.Distance(mousePos, Pos) < 4)
            {
                speed = 0;
            }
            else if (Vector2.Distance(mousePos, Pos) >= 4)
            {
                speed = 3;
            }

            if (isHit)
            {
                color = Color.Red;
            }
            else if (!isHit)
            {
                color = Color.White;
            }            
        }

        private void ShipRotation()
        {
            Vector2 directionOfShip = mousePos - (Pos);
            currentRotation = (float)Math.Atan2(directionOfShip.Y, directionOfShip.X);
        }

        private Vector2 GetDirection()
        {
            Vector2 Direction = new Vector2(Mouse.GetState().X - Pos.X, Mouse.GetState().Y - Pos.Y);
            return Vector2.Normalize(Direction);
        }

        private void UpdateGunData(GameTime gt)
        {
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
            UpdateProjectiles(gt);
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
                    // Console.WriteLine("number of shots " + currentGunCharge);
                }
            }
        }

        private void PlayerIsShooting(GameTime gt)
        {
            gunCooldownTimer += gt.ElapsedGameTime.TotalMilliseconds;

            if (gunCooldownTimer > gunRateOfFire)
            {
                projectile = new Projectile(Pos, projectileTargetPos);
                projectiles.Add(projectile);
                currentGunCharge--;
                gunCooldownTimer = gunCooldownTimerReset;
                // Console.WriteLine("number of shots " + projectiles.Count);                
                SoundManager.PlayShot();
            }
        }

        private void UpdateProjectiles(GameTime gt)
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

        public void Draw(SpriteBatch sb)
        {
            //afterburnerEffect.Draw(sb);

            sb.Draw(Tex, Pos, null, color, currentRotation + MathHelper.ToRadians(90), new Vector2(Tex.Width / 2, Tex.Height / 2), 1, SpriteEffects.None, 1);

        }

        public List<Projectile> GetProjectileList()
        {
            return projectiles;
        }

        public void ClearProjectileList()
        {
            projectiles.Clear();
        }
    }
}
