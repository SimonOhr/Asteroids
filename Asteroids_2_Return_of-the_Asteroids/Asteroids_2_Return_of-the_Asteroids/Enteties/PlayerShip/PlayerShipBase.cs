
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
    class PlayerShipBase : MovingObject
    {
        //protected WeaponBase weapon;
        //protected WeaponBase DEBUGGweapon;
        protected Texture2D healthbarTex = AssetsManager.healthBarTex;
        protected Rectangle srcHealthbarTex;
        protected int currentHealth;

        protected Drone drone;
        protected List<WeaponBase> weapons; //Note* might need a list of lists? to make it possible for multiple ships with different weaponry
        protected Vector2 mousePos;
        protected Color color;
        public Texture2D tex;
        protected Vector2 oldPos;
        public Vector2 Pos { get; private set; }
        protected float originalSpeed;


        public static int hitPoints;
        virtual public bool isHit { get; set; }     
                
        protected float currentRotation;
        /// <summary>
        ///  Baseclass refactoring -> refactor to apply to all ships, including AI controlled
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="mousePos"></param>
        public PlayerShipBase(Vector2 pos, Vector2 mousePos) : base(pos)
        {
            this.Pos = pos;
            this.mousePos = mousePos;

            color = Color.White;
            weapons = new List<WeaponBase>();                  
        }
        virtual public void Update(GameTime gt)
        {
            hitbox.X = (int)Pos.X;
            hitbox.Y = (int)Pos.Y;

            mousePos.X = Mouse.GetState().X;
            mousePos.Y = Mouse.GetState().Y;

            SoftInSoftOut();
            ShipRotation();
            GetDirection();
            ShipIsHitColorSwitch();

            Pos += speed * GetDirection();

            if (hitPoints <= 0)
            {
                color = Color.Blue;
            }

            foreach (WeaponBase w in weapons)
            {
                w.Update(gt);
            }
        }

        virtual protected void SoftInSoftOut()
        {
            speed = (Vector2.Distance(mousePos, Pos) * 0.015f);           
        }

        virtual protected void ShipIsHitColorSwitch()
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

        virtual protected void ShipRotation()
        {
            Vector2 directionOfShip = mousePos - (Pos);
            currentRotation = (float)Math.Atan2(directionOfShip.Y, directionOfShip.X);
        }

        virtual protected Vector2 GetDirection()
        {
            Vector2 Direction = new Vector2(Mouse.GetState().X - Pos.X, Mouse.GetState().Y - Pos.Y);
            return Vector2.Normalize(Direction);
        }     

        virtual public void Draw(SpriteBatch sb)
        {           

            sb.Draw(tex, Pos, null, color, currentRotation + MathHelper.ToRadians(90), new Vector2(tex.Width / 2, tex.Height / 2), 1, SpriteEffects.None, 1);

        }

        virtual public ref List<WeaponBase> GetWeaponList()
        {
            return ref weapons;
                /*weapons.projectiles;*/
        }    
    }
}
