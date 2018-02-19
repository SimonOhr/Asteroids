using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids_2_Return_of_the_Asteroids
{
    class Fuzzy_StateAttack_High : IFusmState
    {
        string name;
        Pirate actor;
        PlayerShip target;
        float activationLevel;
        WeaponBase weapon;


        public Fuzzy_StateAttack_High(Pirate actor, PlayerShip target)
        {
            this.actor = actor;
            this.target = target;
            name = "Attack";
            Init();
        }

        public float CalculateActivation()
        {
            float dist = Vector2.Distance(actor.Pos, target.Pos);
            activationLevel = actor.GetAttackRadius() / (dist - actor.GetObjectRadius());
            CheckBounds();

            return activationLevel;
        }

        public bool canActivate()
        {
            Console.WriteLine("actor Hp " + actor.GetHealth() + " target Hp " + target.GetHealth());
            if (CalculateActivation() == 1)
                if (actor.GetHealth() > target.GetHealth())
                    return true;
            return false;
        }

        public void CheckBounds(float lb = 0, float ub = 1)
        {
            CheckLowerBound(lb);
            CheckUpperBound(ub);
        }

        public void CheckLowerBound(float lbound = 0)
        {
            if (activationLevel < lbound)
                activationLevel = lbound;
        }

        public void CheckUpperBound(float ubound = 1)
        {
            if (activationLevel > ubound)
                activationLevel = ubound;
        }

        public void Enter()
        {

        }

        public void Exit()
        {

        }

        public string GetName()
        {
            return name;
        }

        public void Init()
        {
            weapon = actor.GetWeapon();
        }

        public void Update()
        {
            weapon.SetTargetPos(target.Pos);
            weapon.Shoot(true);
        }
    }
}
