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
        FSM fsm;
        bool isDt;
        int searchRadius;
        public State_FollowPlayer(Pirate actor, PlayerShip target, bool isDt)
        {
            this.actor = actor;
            this.target = target;
            this.isDt = isDt;
            if (!isDt)
                fsm = actor.pirateFSM;
            this.searchRadius = actor.GetSearchRadius();
        }

        public void Enter()
        {

        }

        public void Execute()
        {
            actor.Direction = GetDirection();
            if (!isDt)
                fsm.ChangeState(new State_BaseCase(actor, target, fsm));
        }

        private Vector2 GetDirection()
        {
            actor.Direction = new Vector2(target.Pos.X - actor.Pos.X, target.Pos.Y - actor.Pos.Y);
            return Vector2.Normalize(actor.Direction);
        }

        public void Exit()
        {

        }
    }
}
