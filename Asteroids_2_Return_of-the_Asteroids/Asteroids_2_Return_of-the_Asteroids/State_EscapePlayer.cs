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

        public State_EscapePlayer(Pirate actor, PlayerShip playerShip)
        {
            this.actor = actor;
            this.playerShip = playerShip;
        }

        public void Enter()
        {
          
        }

        public void Execute()
        {
            actor.Direction *= -1;
        }

        public void Exit()
        {
          
        }
    }
}
