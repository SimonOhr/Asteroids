using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids_2_Return_of_the_Asteroids
{
    class State_AvoidIncomingObject : IState
    {
        Pirate actor;       
        int actorRadius;
       // FSM fsm;
        public State_AvoidIncomingObject(Pirate actor/*, FSM fsm*/)
        {
            this.actor = actor;
            actorRadius = actor.GetObjectRadius();
          //  this.actorRadius = actorRadius;
          //  this.fsm = fsm;            
        }
        public void Enter()
        {

        }

        public void Execute()
        {
            actor.SetAvoidance(CollisionAvoidance(actor.Pos, actor.Direction));
           // fsm.RevertToPreviousState();
        }

        private Vector2 CollisionAvoidance(Vector2 pos, Vector2 direction)
        {
            var maxSeeAhead = 400;
            var ahead = pos + direction * maxSeeAhead; // max_see_ahead          
            Asteroid a = null;
            
            var ahead2 = pos + direction * 200;
            a = FindMTO(pos, ahead, ahead2);

            var mostThreatening = a;
            var avoidance = new Vector2(0, 0);
            if (mostThreatening != null)
            {
                avoidance.X = ahead.X - mostThreatening.Pos.X;
                avoidance.Y = ahead.Y - mostThreatening.Pos.Y;

                avoidance.Normalize();
                avoidance *= 2f; // max_avoid_force
            }
            else
                avoidance *= 0;
            return avoidance;
        }

        private Asteroid FindMTO(Vector2 pos, Vector2 ahead, Vector2 ahead2)
        {
            Asteroid mostThreatening = null;
            for (int i = 0; i < GameplayManager.asteroids.Count - 1; i++)
            {
                Asteroid obstacle = GameplayManager.asteroids[i];
                bool collision = LineIntersectsCircle(ahead, ahead2, obstacle);
                if (collision && (mostThreatening == null ||
                    Vector2.Distance(pos, obstacle.Pos) < Vector2.Distance(pos, mostThreatening.Pos)))
                {
                    mostThreatening = obstacle;
                }
            }
            return mostThreatening;
        }

        private bool LineIntersectsCircle(Vector2 ahead, Vector2 ahead2, Asteroid obstacle)
        {
            return Vector2.Distance(obstacle.Pos, ahead) <= obstacle.Radius + actorRadius ||
           Vector2.Distance(obstacle.Pos, ahead2) <= obstacle.Radius + actorRadius;
        }

        public void Exit()
        {

        }
    }
}
