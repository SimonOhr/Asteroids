
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
    class ShipBase : MovingObject
    {        
        protected Texture2D healthbarTex = AssetsManager.healthBarTex;
        protected Rectangle srcHealthbarTex;
       
        
        protected Drone drone;
        protected List<WeaponBase> weapons; //Note* might need a list of lists? to make it possible for multiple ships with different weaponry        
        protected Color color;
        public Texture2D tex;
       // protected Vector2 oldPos;

        protected float originalSpeed;


        public static int hitPoints, maxHealth;
        virtual public bool isHit { get; set; }

        protected float currentRotation;
       
        public ShipBase(Vector2 pos) : base(pos)
        {
            this.pos = pos;

            color = Color.White;
            weapons = new List<WeaponBase>();
        }
        virtual public void Update(GameTime gt)
        {
            hitbox.X = (int)pos.X;
            hitbox.Y = (int)pos.Y;

            foreach (WeaponBase w in weapons)
            {
                w.Update(gt);
            }
        }
        virtual protected void ShipRotation(Vector2 targetPos, Vector2 pos)
        {
            Vector2 directionOfShip = targetPos - (pos);
            currentRotation = (float)Math.Atan2(directionOfShip.Y, directionOfShip.X);
        }

        virtual protected Vector2 GetDirection(Vector2 targetPos, Vector2 pos)
        {
            Vector2 Direction = new Vector2(targetPos.X - pos.X, targetPos.Y - pos.Y);
            return Vector2.Normalize(Direction);
        }

        virtual public ref List<WeaponBase> GetWeaponList()
        {
            return ref weapons;           
        }
    }
}
