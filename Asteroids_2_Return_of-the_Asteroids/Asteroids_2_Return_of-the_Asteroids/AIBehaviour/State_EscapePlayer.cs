using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids_2_Return_of_the_Asteroids
{
    class State_EscapePlayer : IState
    {
        Pirate actor;
        PlayerShip playerShip;
        FSM fsm;
        bool isDt;

        public State_EscapePlayer(Pirate actor, PlayerShip playerShip, bool isDt)
        {
            this.actor = actor;
            this.playerShip = playerShip;
            this.isDt = isDt;
            if (!isDt)
                fsm = actor.pirateFSM;

        }

        public void Enter()
        {

        }

        public void Execute()
        {
            Vector2 Direction = new Vector2(playerShip.Pos.X - actor.Pos.X, playerShip.Pos.Y - actor.Pos.Y);

            actor.Direction = Vector2.Normalize(Direction) * -1;
            if (!isDt)
                fsm.ChangeState(new State_BaseCase(actor, playerShip, fsm));
        }

        public void Exit()
        {

        }
    }
}
