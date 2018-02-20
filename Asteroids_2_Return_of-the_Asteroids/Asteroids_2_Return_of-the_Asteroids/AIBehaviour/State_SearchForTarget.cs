using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids_2_Return_of_the_Asteroids
{
    class State_SearchForTarget : IState
    {
        Pirate actor;
        PlayerShip target;
     //   FSM fsm;
        int searchRadius, attackRadius;        
       
        public State_SearchForTarget(Pirate actor, PlayerShip target/*, FSM fsm*/)
        {
            this.actor = actor;
            this.target = target;
           // this.fsm = fsm;

            searchRadius = actor.GetSearchRadius();
            attackRadius = actor.GetAttackRadius();           
        }
        public void Enter()
        {

        }

        public void Execute()
        {
            //if (Vector2.Distance(actor.Pos, target.Pos) < attackRadius)
            //{
            //    fsm.ChangeState(new State_FollowPlayer(actor, target, searchRadius, fsm));
            //    fsm.ChangeState(new State_AttackPlayer(actor, target, fsm));                
            //}
            //else if (Vector2.Distance(actor.Pos, target.Pos) < searchRadius)
            //{
            //    fsm.ChangeState(new State_FollowPlayer(actor, target, searchRadius, fsm));
            //}
            //else
            //    fsm.ChangeState(new State_Base(actor, target, searchRadius, fsm));
        }       

        public void Exit()
        {

        }
    }
}
