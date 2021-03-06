﻿using Microsoft.Xna.Framework;
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
         PlayerShip playerShip;
        //  int searchRadius;
        FSM fsm;
        bool isDt;
        public State_Idle(Pirate actor, PlayerShip playerShip, bool isDt)
        {
            this.actor = actor;
            this.playerShip = playerShip;
            //  this.searchRadius = actor.GetSearchRadius();
            this.isDt = isDt;
            if (!isDt)
                fsm = actor.pirateFSM;
        }
        public void Enter()
        {

        }

        public void Execute()
        {
            // actor.direction = Vector2.Zero;
            actor.Direction = Vector2.Zero;
            if(!isDt)
              fsm.ChangeState(new State_BaseCase(actor, playerShip, fsm));
        }


        public void Exit()
        {

        }
    }
}
