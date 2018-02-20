using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Asteroids_2_Return_of_the_Asteroids
{
    class State_Idle : IState
    {
        Pirate actor;
      //  PlayerShip target;
     //  int searchRadius;
     //   FSM fsm;
        public State_Idle(Pirate actor/*, FSM fsm*/)
        {
            this.actor = actor;
            //this.target = target;
          //  this.searchRadius = actor.GetSearchRadius();
          //  this.fsm = fsm;
        }
        public void Enter()
        {

        }

        public void Execute()
        {
           // actor.direction = Vector2.Zero;
            actor.Direction = Vector2.Zero;
          //  fsm.ChangeState(new State_SearchForTarget(actor, target, fsm));
        }
       

        public void Exit()
        {
           
        }
    }
}
