using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids_2_Return_of_the_Asteroids
{
    class State_BaseCase : IState
    {
        Pirate actor;
        PlayerShip playerShip;
        FSM fsm;
        int searchRadius, attackRadius;

        public State_BaseCase(Pirate actor, PlayerShip playerShip, FSM fsm)
        {
            this.actor = actor;
            this.playerShip = playerShip;
            this.fsm = fsm;

            searchRadius = actor.GetSearchRadius();
            attackRadius = actor.GetAttackRadius();
        }
        public void Enter()
        {

        }

        public void Execute()
        {
            if (Vector2.Distance(actor.Pos, playerShip.Pos) < attackRadius && actor.GetHealth() > 1)
            {
                fsm.ChangeState(new State_FollowPlayer(actor, playerShip, false));
                fsm.ChangeState(new State_AttackPlayer(actor, playerShip, false));
            }
            else if (Vector2.Distance(actor.Pos, playerShip.Pos) < searchRadius && actor.GetHealth() > 1)
            {
                fsm.ChangeState(new State_FollowPlayer(actor, playerShip, false));
            }
            else if (actor.GetHealth() == 1)
            {
                fsm.ChangeState(new State_EscapePlayer(actor, playerShip, false));
            }
          //  else
             //   fsm.ChangeState(new State_Base(actor, target, searchRadius));
        }

        public void Exit()
        {

        }
    }
}
