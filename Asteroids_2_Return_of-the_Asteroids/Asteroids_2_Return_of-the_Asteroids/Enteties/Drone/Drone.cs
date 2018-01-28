using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids_2_Return_of_the_Asteroids
{
    class Drone : MovingObject
    {
        enum droneState { patrol, attack, evade, save /*, chase*/ }
        droneState currentState;
        int id;
        Asteroid enemyTarget;
        // Vector2 targetPos;
        PlayerShipBase ship;
        LaserCanon weapon;
        Texture2D tex;
        int patrolRadius, attackRadius;
        double time;
        float currentRotation, speed;
        public Drone(Vector2 pos, PlayerShipBase ship) : base(pos)
        {
            this.ship = ship;
            weapon = new LaserCanon(pos);
            tex = AssetsManager.droneTex;
            patrolRadius = 200;
            attackRadius = 150;
            speed = 1;
            hitbox = new Rectangle((int)pos.X, (int)pos.Y, tex.Width, tex.Height);
            currentState = droneState.patrol;
        }

        public override void Update(GameTime gt)
        {
            weapon.SetPos(pos);
            weapon.Update(gt);
            pos = SetPos(gt);
            switch (currentState)
            {
                case droneState.patrol:                    
                    Rotation(gt);
                    UpdateTargetList();
                    break;
                case droneState.attack:
                    EngageTarget(enemyTarget);
                    break;
                case droneState.evade:
                    break;
                case droneState.save:
                    break;
            }
            base.Update(gt);
        }

        private Vector2 SetPos(GameTime gt)
        {
            Vector2 temp;
            time += gt.ElapsedGameTime.TotalSeconds;
            temp.X = ship.Pos.X + (float)(patrolRadius * Math.Cos((speed * time) + MathHelper.ToDegrees(90)));
            temp.Y = ship.Pos.Y + (float)(patrolRadius * Math.Sin((speed * time) + MathHelper.ToDegrees(90)));
            return temp;
        }

        virtual protected void Rotation(GameTime gt)
        {
            Vector2 nextPos;
            if (currentState == droneState.patrol)
                nextPos = SetPos(gt);
            else nextPos = Vector2.Zero;

            Vector2 directionOfShip = nextPos - (pos);
            currentRotation = (float)Math.Atan2(directionOfShip.Y, directionOfShip.X);
        }

        private void UpdateTargetList()
        {
            List<Asteroid> asteroidList = GameplayManager.asteroids;
            for (int i = 0; i < asteroidList.Count - 1; i++)
            {
                if (Vector2.Distance(pos, asteroidList[i].pos) < attackRadius)
                {
                    enemyTarget = asteroidList[i];
                    id = i;
                    currentState = droneState.attack;
                }
            }
        }

        private void EngageTarget(Asteroid a)
        {
            if (id < GameplayManager.asteroids.Count)
                if (GameplayManager.asteroids[id] != null)
                    enemyTarget.pos = GameplayManager.asteroids[id].pos;

          //  pos += speed * GetDirection(enemyTarget.pos);

            weapon.TargetPos(enemyTarget.pos);
            weapon.IsInRange(true);
            if (Vector2.Distance(enemyTarget.pos, pos) > attackRadius)
            {
                weapon.IsInRange(false);
                currentState = droneState.patrol;
            }
            if (enemyTarget.isOutOfPlay)
                currentState = droneState.patrol;
        }

        virtual protected Vector2 GetDirection(Vector2 targetPos)
        {
            Vector2 Direction = new Vector2(targetPos.X - pos.X, targetPos.Y - pos.Y);
            return Vector2.Normalize(Direction);
        }

        public override void Draw(SpriteBatch sb)
        {
            weapon.Draw(sb);
            sb.Draw(tex, pos, null, Color.White, currentRotation, new Vector2(tex.Width / 2, tex.Height / 2), 1, SpriteEffects.FlipVertically, 1);
            base.Draw(sb);
        }
    }
}
