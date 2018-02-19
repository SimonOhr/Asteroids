using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids_2_Return_of_the_Asteroids
{
    class Fuzzy_StateIdle : IFusmState
    {
        float activationLevel;
        string name;
        Pirate actor;
        PlayerShip target;

        public Fuzzy_StateIdle(Pirate actor, PlayerShip target)
        {
            this.actor = actor;
            this.target = target;
            name = "Idle";
        }
        public float CalculateActivation()
        {
            float dist = Vector2.Distance(actor.Pos, target.Pos);

            return ((dist - actor.GetObjectRadius())) / actor.GetSearchRadius(); ;
        }

        public bool canActivate()
        {
            if (CalculateActivation() == 1)
                return true;
            else return false;
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
           
        }

        public void Update()
        {
            actor.SetNewDirection(Vector2.Zero);
        }
    }
}
