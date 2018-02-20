using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids_2_Return_of_the_Asteroids
{
    class State_FollowPlayer : IState
    {
        Pirate actor;
        PlayerShip target;
       // FSM fsm;
        int searchRadius;
        public State_FollowPlayer(Pirate actor, PlayerShip target/*, int searchRadius*//*, FSM fsm*/)
        {
            this.actor = actor;
            this.target = target;
           // this.fsm = fsm;
            this.searchRadius = actor.GetSearchRadius();
        }       

        public void Enter()
        {
           
        }

        public void Execute()
        {
            actor.Direction = GetDirection();
         //   fsm.ChangeState(new State_SearchForTarget(actor, target, fsm));          
        }

        private Vector2 GetDirection()
        {
            actor.Direction = new Vector2(target.Pos.X - actor.Pos.X, target.Pos.Y - actor.Pos.Y);
           return  Vector2.Normalize(actor.Direction);
        }

        public void Exit()
        {
            
        }
    }
}
