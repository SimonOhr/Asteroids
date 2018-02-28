using System;
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
        FSM fsm;
        List<WeaponBase> weapons;
        bool isDt;
        public State_AttackPlayer(Pirate actor, PlayerShip target, bool isDt)
        {
            this.actor = actor;
            this.target = target;
            this.isDt = isDt;
            if (!isDt)
                fsm = actor.pirateFSM;
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
            if (!isDt)
                fsm.RevertToPreviousState();
        }

        public void Exit()
        {

        }
    }
}
