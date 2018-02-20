
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
    class PlayerShip : ShipBase
    {        
       // int healthmultiplier = (AssetsManager.healthBarTex.Width / 10);
        Vector2 mousePos/*, oldMousePos*/;
        
      //  public Vector2 Pos { get; private set; }
        public PlayerShip(Vector2 pos) : base(pos)
        {            
            Tex = AssetsManager.shipTex;

            hitbox = new Rectangle((int)pos.X, (int)pos.Y, Tex.Width, Tex.Height);

            maxHealth = 3;
            currentHealth = 3;
            
            //srcHealthbarTex = new Rectangle(0, 0, hitPoints * healthmultiplier, AssetsManager.healthBarTex.Height);

            EffectsManager.CreateAfterBurnerEffect(Pos);

            weapons.Add(new LaserCanon(Pos));
            weapons.Add(new MiniMissileLauncher(pos));

           
            speed = 4;
            originalSpeed = speed;
            drone = new Drone(Pos, this); // for test purposes
        }

        public override void Update(GameTime gt)
        {
            EffectsManager.UpdateAfterBurnerEffect(Pos - (GetDirection(mousePos, Pos) * 50));
            drone.Update(gt);            
            mousePos = KeyMouseReader.CursorViewToWorldPosition;
            //oldMousePos = mousePos;
            //mousePos.X = Mouse.GetState().X;
            //mousePos.Y = Mouse.GetState().Y;
           // var deltaMouse = mousePos - oldMousePos;

            Pos += speed * GetDirection(mousePos, Pos);
            SoftInSoftOut();
            ShipRotation(mousePos, Pos);
            //GetDirection(mousePos);
            ShipIsHitColorSwitch();                                 

            foreach (WeaponBase w in weapons)
            {
                w.SetPos(Pos);
            }

            AfterburnerIntensifier();           

            if (Mouse.GetState().LeftButton == ButtonState.Pressed && weapons[0].GetGuncharge() > 0)
            {
                weapons[0].SetTargetPos(mousePos);
                weapons[0].isShooting(gt);
            }
            else if (Mouse.GetState().LeftButton == ButtonState.Released)
            {
                weapons[0].gunCooldownTimer = 200f;
            }
            base.Update(gt);
        }      

        private void SoftInSoftOut()
        {
            speed = (Vector2.Distance(mousePos, Pos) * 0.015f);
        }

        private void ShipIsHitColorSwitch()
        {
            if (isHit)
            {
                color = Color.Red;
            }
            else
            {
                color = Color.White;
            }
        }

        public override void Draw(SpriteBatch sb)
        {
            foreach (WeaponBase w in weapons)
            {
                w.Draw(sb);
            }
            drone.Draw(sb);
           
            sb.Draw(Tex, Pos, null, color, currentRotation + MathHelper.ToRadians(90), new Vector2(Tex.Width / 2, Tex.Height / 2), 1, SpriteEffects.FlipVertically, 1);
            // base.Draw(sb);
        }

        private void AfterburnerIntensifier()
        {
            EffectsManager.SetAfterBurnerIntensity(speed);
        }

        public override int GetHealth()
        {
            return currentHealth;
        }

        public void SetHealth(int deltaHealth)
        {
            currentHealth += deltaHealth;
        }
    }
}
