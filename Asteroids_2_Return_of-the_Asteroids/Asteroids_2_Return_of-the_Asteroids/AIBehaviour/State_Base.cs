using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Asteroids_2_Return_of_the_Asteroids
{
    class State_Base : IState
    {
        Pirate actor;
        PlayerShip target;
        int searchRadius;
        FSM fsm;
        public State_Base(Pirate actor, PlayerShip target, int searchRadius, FSM fsm)
        {
            this.actor = actor;
            this.target = target;
            this.searchRadius = searchRadius;
            this.fsm = fsm;
        }
        public void Enter()
        {

        }

        public void Execute()
        {
            actor.direction = Vector2.Zero;
            fsm.ChangeState(new State_SearchForTarget(actor, target, fsm));
        }
       

        public void Exit()
        {
           
        }
    }
}
