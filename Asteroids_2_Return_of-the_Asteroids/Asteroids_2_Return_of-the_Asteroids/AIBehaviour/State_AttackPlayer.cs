﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids_2_Return_of_the_Asteroids
{
    class State_AttackPlayer : IState
    {
        Pirate actor;
        PlayerShip target;
       // FSM fsm;
        List<WeaponBase> weapons;
        public State_AttackPlayer(Pirate actor, PlayerShip target/*, FSM fsm*/)
        {
            this.actor = actor;
            this.target = target;
         //   this.fsm = fsm;
        }
        public void Enter()
        {
            weapons = actor.GetWeapons();
        }

        public void Execute()
        {
            foreach (WeaponBase weapon in weapons)
            {
                weapon.canShoot();
                weapon.SetTargetPos(target.Pos);
                weapon.Shoot(true);
            }
                     
          //  fsm.RevertToPreviousState();           
        }

        public void Exit()
        {
            
        }
    }
}
