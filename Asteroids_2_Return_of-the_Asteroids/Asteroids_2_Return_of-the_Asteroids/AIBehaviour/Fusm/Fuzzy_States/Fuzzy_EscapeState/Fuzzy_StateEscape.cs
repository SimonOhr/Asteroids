using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids_2_Return_of_the_Asteroids
{
    class Fuzzy_StateEscape : IFusmState
    {
        string name;
        Pirate actor;
        PlayerShip target;
        float activationLevel;
        public bool CanActivate { get; private set; }

        public Fuzzy_StateEscape(Pirate actor, PlayerShip target)
        {
            this.actor = actor;
            this.target = target;
            name = "Escape";
        }
        public float CalculateActivation()
        {           
            activationLevel = (actor.GetMaxHealth() / actor.GetHealth()) -1; //-1 to "start count" when health is -> 0
            
            CheckBounds();

            return activationLevel;
        }

        public void SetIsActive(bool booleanState)
        {
            CanActivate = booleanState;
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
            actor.Direction *= -1;
        }
    }
}
