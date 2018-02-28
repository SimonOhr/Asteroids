using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids_2_Return_of_the_Asteroids
{
    class Pirate : PirateBase
    {
        PlayerShip playerShip;

        Vector2 velocity, baseVelocity/*, newDirection*/;
      //  public Vector2 Direction { get; set; }
       // public Vector2 Pos { get; private set; }
        private Vector2 avoidance;
        int radius = 50, attackRange = 300, searchRadius = 400;
       
        public LaserCanon Canon { get; set; }        
       // float rotationSpeed;

        //FSM test-------------------------------
         
        public FSM pirateFSM { get; set; }
                                           //Fuzzy test ----------------------------
        FusmMachine fusm;

        //Decisiontree test----------------------
        DecisionTree dt;

        private Vector2 foundTargetPos;

        public Pirate(Vector2 pos, PlayerShip playerShip) : base(pos)
        {
            this.playerShip = playerShip;
            velocity = new Vector2(3, 3);
            //Canon = new LaserCanon(pos);
            weapons.Add(new LaserCanon(pos, this));
            weapons.Add(new MiniMissileLauncher(pos, this));
            currentHealth = 3;
            fusm = new FusmMachine(this, playerShip);
            baseVelocity = new Vector2(10, 10);
            //fusm.AddState(new Fuzzy_StateChase(this, playerShip));
            pirateFSM = new FSM();
            pirateFSM.ChangeState(new State_BaseCase(this, playerShip, pirateFSM)); //basecase
            dt = new DecisionTree(this, playerShip);

        }

        public override void Update(GameTime gt)
        {
            Pos = base.Pos;
            //Canon.SetPos(base.Pos);
            //Canon.Update(gt);
            foreach (WeaponBase weapon in weapons)
            {
                weapon.SetPos(base.Pos);
                weapon.Update(gt);
            }
            base.Pos += velocity * Direction + avoidance;
            base.ShipRotation(base.Pos + (Direction + avoidance), base.Pos);
           // pirateFSM.ChangeState(dt.CommenceIfStatements());
          //  var a = UpdateTargetList(gt, attackRange);
              pirateFSM.UpdateState();   
           // fusm.Update();
        }

        public override void Draw(SpriteBatch sb)
        {
            //Canon.Draw(sb);
            foreach (WeaponBase weapon in weapons)
            {
                weapon.Draw(sb);
            }
            sb.Draw(AssetsManager.shipTex, base.Pos, null, Color.Red, currentRotation + MathHelper.ToRadians(-90), new Vector2(AssetsManager.shipTex.Width / 2, AssetsManager.shipTex.Height / 2), 1, SpriteEffects.None, 0);
            base.Draw(sb);
        }

        public override int GetHealth()
        {
            return currentHealth;
        }

        public void SetHealth(int deltaHealth)
        {
            currentHealth += deltaHealth;
        }

        //public void SetNewState(IState newState)
        //{
        //    pirateFSM.ChangeState(newState);
        //}
    
        public List<WeaponBase> GetWeapons()
        {
            return weapons;
        }

        public void SetAvoidance(Vector2 avoidance)
        {
            this.avoidance = avoidance;
        }

        public int GetObjectRadius()
        {
            return radius;
        }

        public int GetSearchRadius()
        {
            return searchRadius;
        }

        public int GetAttackRadius()
        {
            return attackRange;
        }
        //Fuzzy Method(test)----------------------------------------

        public void SetTargetFound(Vector2 pos)
        {
            foundTargetPos = pos;
        }

        //public void SetNewDirection(Vector2 direction)
        //{
        //   newDirection = direction;
        //}        

        public override int GetMaxHealth()
        {
            return maxHealth;
        }

        public void SetSpeed(Vector2 newSpeed)
        {
            velocity = newSpeed;
        }

        public Vector2 GetBaseVelocity()
        {
            return baseVelocity;
        }
    }
}
   

