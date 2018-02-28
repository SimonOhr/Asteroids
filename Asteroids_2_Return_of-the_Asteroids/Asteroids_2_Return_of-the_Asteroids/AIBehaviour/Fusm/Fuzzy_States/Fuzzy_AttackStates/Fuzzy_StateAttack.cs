using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids_2_Return_of_the_Asteroids
{
    class Fuzzy_StateAttack : IFusmState
    {
        string name;
        Pirate actor;
        PlayerShip target;
        float activationLevel;
        public bool CanActivate { get; private set; }
        List<WeaponBase> weapons;
        int maxDistanceForActivation;



        public Fuzzy_StateAttack(Pirate actor, PlayerShip target)
        {
            this.actor = actor;
            this.target = target;
            name = "Attack";
        }

        public float CalculateActivation()
        {
            //0.00166 p pixel
            // y = (1/ 400) * x + 0;

            float dist = Vector2.Distance(actor.Pos, target.Pos);

            if (dist > actor.GetAttackRadius() * 2)
                return activationLevel = 0;
            if (dist <= 0)
                return activationLevel = 0;
            if (dist / actor.GetAttackRadius() < 1)
            {
              //  Console.WriteLine("TEST ACTIAVTION LEVEL: " + (dist / actor.GetAttackRadius()));
                return activationLevel = dist / actor.GetAttackRadius();
            }

            else
            {
                float surplusActivation = (1 - dist / actor.GetAttackRadius());
                //Console.WriteLine("TEST ACTIVATION LEVEL TEMP" + temp);
                //Console.WriteLine("TEST ACTIAVTION LEVEL: " + (activationLevel + 1 + temp ));
                return activationLevel += 1 + surplusActivation;
            }





            //activationLevel = actor.GetAttackRadius() / (dist - actor.GetObjectRadius());
            // CheckBounds();
            // Console.WriteLine(activationLevel);
            //  return activationLevel;
        }

        public void SetCanActivate(bool isActiveState)
        {
            CanActivate = isActiveState;
        }

        public bool GetCanActivate()
        {
            return CanActivate;
        }

        public void CheckBounds(float lb = 0.05f, float ub = 1.0f)
        {
            CheckLowerBound(lb);
            CheckUpperBound(ub);
        }

        public void CheckLowerBound(float lbound = 0.05f)
        {
            if (activationLevel < lbound)
                activationLevel = 0;
        }

        public void CheckUpperBound(float ubound = 1.0f)
        {
            if (activationLevel > ubound)
                activationLevel = ubound;
            //float temp = 0;
            //if (activationLevel > ubound)
            //{
            //    if (activationLevel > 2)
            //        activationLevel = 2;

            //    temp = activationLevel - 1; // creates a triangle
            //    activationLevel = ubound - temp;
            //    if (activationLevel < 0)
            //        CheckLowerBound();
            //}
        }

        public void Enter()
        {
            weapons = actor.GetWeapons();
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

        }

        public void Update()
        {
            //foreach (WeaponBase weapon in weapons)
            //{
            //    if (activationLevel > 0.8)
            //        if (weapon.canShoot() && weapon.Name == "MiniMissileLauncher" && activationLevel > 0.8)
            //        {
            //            //Console.WriteLine("FIRE MISSILES");
            //            weapon.SetTargetPos(target.Pos);
            //            weapon.Shoot(true);
            //        }

            //    if (weapon.canShoot() && weapon.Name == "LaserCanon")
            //    {
            //        weapon.SetTargetPos(target.Pos);
            //        weapon.Shoot(true);
            //    }

            //}
        }
    }
}
