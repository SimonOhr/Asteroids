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

        Vector2 velocity, newDirection;
        public Vector2 direction;
        public Vector2 Pos { get; private set; }
        private Vector2 avoidance;
        int radius = 50, attackRange = 600, searchRadius = 600;
       
        public LaserCanon canon;        
        float rotationSpeed;

        //FSM test-------------------------------
         FSM pirateFSM;

        //Fuzzy test ----------------------------
        // FusmMachine fusm;

        //Decisiontree test----------------------
        DecisionTree dt;

        private Vector2 foundTargetPos;

        public Pirate(Vector2 pos, PlayerShip playerShip) : base(pos)
        {
            this.playerShip = playerShip;
            velocity = new Vector2(3, 3);
            canon = new LaserCanon(pos);
            weapons.Add(canon);
            currentHealth = 3;
            // fusm = new FusmMachine(this, playerShip);
            //fusm.AddState(new Fuzzy_StateChase(this, playerShip));
              pirateFSM = new FSM();
            // pirateFSM.ChangeState(new State_SearchForTarget(this, playerShip, pirateFSM));
            dt = new DecisionTree(this, playerShip);

        }

        public override void Update(GameTime gt)
        {
            Pos = pos;
            canon.SetPos(pos);
            canon.Update(gt);            
            pos += velocity * direction + avoidance;
            ShipRotation(pos + (direction + avoidance), pos);
            pirateFSM.ChangeState(dt.CommenceIfStatements());
          //  var a = UpdateTargetList(gt, attackRange);
              pirateFSM.UpdateState();   
           // fusm.Update();
        }

        public override void Draw(SpriteBatch sb)
        {
            canon.Draw(sb);
            sb.Draw(AssetsManager.shipTex, pos, null, Color.Red, currentRotation + MathHelper.ToRadians(-90), new Vector2(AssetsManager.shipTex.Width / 2, AssetsManager.shipTex.Height / 2), 1, SpriteEffects.None, 0);
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
    
        public WeaponBase GetWeapon()
        {
            return canon;
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

        public void SetNewDirection(Vector2 direction)
        {
           this.direction = direction;
        }

        public int GetAmmoCount()
        {
            return GetWeapon().GetGuncharge();
        }      
    }
}
   

