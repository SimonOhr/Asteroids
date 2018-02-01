using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids_2_Return_of_the_Asteroids
{
    class Drone : ShipBase
    {
        enum droneState { patrol, attack, evade, save /*, chase*/ }
        droneState currentState;
        int id;
        Asteroid enemyTarget, scanTarget;
        // Vector2 targetPos;
        PlayerShip ship;
        LaserCanon weapon;
        Texture2D tex;
        int patrolRadius, attackRadius, scanRadius, collisionRadius;
        double time;
        float currentRotation;
        Random rnd;
        bool save, evade;
        bool EVASIVEMANOUVER = false;
        Vector2 moveTo, velocity, direction, impactVector;
        int radius = 50;
        Vector2 ahead;
        Vector2 ahead2;
        public Drone(Vector2 pos, PlayerShip ship) : base(pos)
        {
            this.ship = ship;
            weapon = new LaserCanon(pos);
            tex = AssetsManager.droneTex;
            patrolRadius = 200;
            attackRadius = 150;
            speed = 0.5f;
            hitbox = new Rectangle((int)pos.X, (int)pos.Y, tex.Width, tex.Height);

            rnd = new Random();
            collisionRadius = 100;
            scanRadius = 300;
            velocity = new Vector2(5, 5);
            currentState = droneState.patrol;
        }

        public override void Update(GameTime gt)
        {
            weapon.SetPos(pos);
            weapon.Update(gt);


            switch (currentState)
            {
                case droneState.patrol:
                    Rotation(gt);
                    UpdateTargetList(gt);
                    direction = GetDirection(SetPos(gt), pos);
                    UpdatePosition(gt);
                  //  if (enemyTarget != null) CheckIfCollisionImminent(gt, ref enemyTarget);
                    //if (save) currentState = droneState.save;
                    if (evade)
                    {
                        currentState = droneState.evade;
                    }
                    break;
                case droneState.attack:
                    //CheckIfCollisionImminent(gt, ref enemyTarget); 
                    UpdatePosition(gt);
                    EngageTarget(ref enemyTarget);
                    break;
                case droneState.evade:
                    //if (enemyTarget != null) CheckIfCollisionImminent(gt, ref enemyTarget);                    
                    //EvadeCollision();
                    UpdatePosition(gt);
                    if (Vector2.Distance(enemyTarget.pos, pos) > collisionRadius)
                    {
                        currentState = droneState.patrol;
                        Console.WriteLine("Evasive Action Done");
                    }
                    //currentState = droneState.patrol;
                    break;
                case droneState.save:
                    // if (enemyTarget != null) CheckIfCollisionImminent(ref enemyTarget);
                    // ProtectMaster();
                    currentState = droneState.patrol;
                    break;
            }
            base.Update(gt);
        }

        private void UpdatePosition(GameTime gt)
        {
            var steering = CollisionAvoidance(gt);
            //if (steering.X > 0 || steering.Y > 0)
            //    direction = Vector2.Normalize(steering);
            pos += velocity * direction + steering;
            //else
            //    pos = SetPos(gt);
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
                moveTo = SetPos(gt);
            else nextPos = Vector2.Zero;

            Vector2 directionOfShip = moveTo - (pos);
            currentRotation = (float)Math.Atan2(directionOfShip.Y, directionOfShip.X);
        }

        private void UpdateTargetList(GameTime gt)
        {
            for (int i = 0; i < GameplayManager.asteroids.Count - 1; i++)
            {
                if (Vector2.Distance(pos, GameplayManager.asteroids[i].pos) < attackRadius)
                {
                    enemyTarget = GameplayManager.asteroids[i];
                    id = i;
                    // if (Vector2.Distance(pos, enemyTarget.pos) < 100) currentState = droneState.evade;
                    currentState = droneState.attack;
                }
                if (Vector2.Distance(ship.Pos, GameplayManager.asteroids[i].pos) < scanRadius)
                {
                    scanTarget = GameplayManager.asteroids[i];
                  //  CheckIfCollisionImminent(gt, ref scanTarget);
                }
            }
        }

        private void EngageTarget(ref Asteroid a)
        {
            //  pos += speed * GetDirection(enemyTarget.pos);

            if (Vector2.Distance(a.pos, pos) > attackRadius || a.hitPoints == 0)
            {
                weapon.IsInRange(false);
                currentState = droneState.patrol;
            }
            else
            {
                weapon.TargetPos(a.pos);
                weapon.IsInRange(true);
            }
        }
        //private void CheckIfCollisionImminent(GameTime gt, ref Asteroid collidingObj)
        //{
        //    Vector2 tempCollisionPos = collidingObj.pos;
        //    Vector2 tempPos = pos;
        //    Vector2 setPos = SetPos(gt);
        //    for (int i = 0; i < 1; i++)
        //    {
        //        tempCollisionPos += collidingObj.velocity * collidingObj.direction;
        //        tempPos += velocity * GetDirection(setPos, tempPos);

        //        if (Vector2.Distance(ship.Pos, tempCollisionPos) < collidingObj.radius + ship.tex.Width / 2)
        //        {
        //            save = true;
        //            Console.WriteLine("save true");
        //            break;
        //        }

        //        else if (Vector2.Distance(pos, tempCollisionPos) < collisionRadius)
        //        {
        //            evade = true;
        //            Console.WriteLine("evade true");
        //            impactVector = tempPos;
        //            break;
        //        }
        //        save = false;
        //        evade = false;
        //        EVASIVEMANOUVER = false;
        //    }
        //}

        // asteroids[i].radius + (gm.GetShipTex().Width / 2
        private Vector2 CollisionAvoidance(GameTime gt)
        {
            ahead = pos + GetDirection(SetPos(gt), pos) * 30; // max_see_ahead
            ahead2 = pos + GetDirection(SetPos(gt), pos) * 30 * 0.5f;
            var mostThreatening = FindMTO();
            var avoidance = new Vector2(0, 0);
            if (mostThreatening != null)
            {
                avoidance.X = ahead.X - mostThreatening.pos.X;
                avoidance.Y = ahead.Y - mostThreatening.pos.Y;

                avoidance.Normalize();
                avoidance *= 3; // max_avoid_force
            }
            else
                avoidance *= 0;
            return avoidance;
        }

        private Asteroid FindMTO()
        {
            Asteroid mostThreatening = null;
            for (int i = 0; i < GameplayManager.asteroids.Count - 1; i++)
            {
                Asteroid obstacle = GameplayManager.asteroids[i];
                bool collision = LineIntersectsCircle(ahead, ahead2, obstacle);
                if (collision && (mostThreatening == null ||
                    Vector2.Distance(pos, obstacle.pos) < Vector2.Distance(pos, mostThreatening.pos)))
                {
                    mostThreatening = obstacle;
                }
            }
            return mostThreatening;
        }

        private bool LineIntersectsCircle(Vector2 ahead, Vector2 ahead2, Asteroid obstacle)
        {
            return Vector2.Distance(obstacle.pos, ahead) <= obstacle.radius + radius ||
               Vector2.Distance(obstacle.pos, ahead2) <= obstacle.radius + radius;
        }

        //private void EvadeCollision()
        //{

        //    while (evade && !EVASIVEMANOUVER)
        //    {
        //       // direction = new Vector2(0, 0);
        //        EVASIVEMANOUVER = true;
        //        Console.WriteLine("EVASIVEMANOUVER : direction: " + direction);
        //    }
        //}

        private void ProtectMaster()
        {

        }

        public override void Draw(SpriteBatch sb)
        {
            weapon.Draw(sb);
            sb.Draw(tex, pos, null, Color.White, currentRotation, new Vector2(tex.Width / 2, tex.Height / 2), 1, SpriteEffects.FlipVertically, 1);
            base.Draw(sb);
        }
    }
}
