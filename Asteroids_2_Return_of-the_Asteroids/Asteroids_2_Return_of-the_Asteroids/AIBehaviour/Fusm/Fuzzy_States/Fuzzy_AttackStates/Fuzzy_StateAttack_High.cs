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
        public bool CanActivate { get; private set; }
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
           // Console.WriteLine(activationLevel);
            return activationLevel;
        } 
        
        public void SetIsActive(bool isActiveState)
        {
            CanActivate = isActiveState;
        }

        public bool GetIsActive()
        {
            return CanActivate;
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
            float temp = 0;
            if (activationLevel > ubound)
            {
                if (activationLevel > 2)
                    activationLevel = 2;

                temp = activationLevel - 1; // creates a triangle
                activationLevel = ubound - temp;
                Console.WriteLine(activationLevel);
            }
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
