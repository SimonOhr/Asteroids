
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
      //  protected Rectangle srcHealthbarTex; // will be used as hoovering healthbar for each object instance

        protected Drone drone;
        protected List<WeaponBase> weapons; //Note* might need a list of lists? to make it possible for multiple ships with different weaponry        
        protected Color color;
        public Texture2D Tex { get; set; }
       // protected Vector2 oldPos;

        protected float originalSpeed;


        //public static int health, maxHealth;
        protected int currentHealth, maxHealth;
        virtual public bool isHit { get; set; }

        protected float currentRotation;
       
        public ShipBase(Vector2 pos) : base(pos)
        {
            this.Pos = pos;
           
            color = Color.White;
            weapons = new List<WeaponBase>();
        }
       new virtual public void Update(GameTime gt) // unsure if I should have new here, nothing breaks, but if I collect all gameobjects into a collection and try to update them all in a bulk, then it might.
        {
            hitbox.X = (int)Pos.X;
            hitbox.Y = (int)Pos.Y;          

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

        virtual public Asteroid UpdateTargetList(GameTime gt, int attackRadius)
        {
            for (int i = 0; i < GameplayManager.asteroids.Count - 1; i++)
            {
                Asteroid a;
                if (Vector2.Distance(Pos, GameplayManager.asteroids[i].Pos) < attackRadius)
                {
                    a = GameplayManager.asteroids[i];
                    //id = i;
                    // if (Vector2.Distance(pos, enemyTarget.pos) < 100) currentState = droneState.evade;
                    //currentState = droneState.Attack;
                    return a;
                }
                //if (Vector2.Distance(ship.Pos, GameplayManager.asteroids[i].pos) < scanRadius)
                //{
                //    scanTarget = GameplayManager.asteroids[i];
                //    //  CheckIfCollisionImminent(gt, ref scanTarget);
                //}
               
            }
            return null;
        }
        public virtual int GetHealth()
        {
            return currentHealth;
        }

        public virtual int GetMaxHealth()
        {
            return maxHealth;
        }
    }

    
}
