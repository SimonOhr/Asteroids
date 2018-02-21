using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids_2_Return_of_the_Asteroids
{
    class Fuzzy_StateChase_High : IFusmState
    {
        string name;
        Pirate actor;
        PlayerShip target;
        float activationLevel;
        public bool CanActivate { get; private set; }


        public Fuzzy_StateChase_High(Pirate actor, PlayerShip target)
        {
            this.actor = actor;
            this.target = target;
            name = "Chase";
        }

        public void Init()
        {

        }

        public float CalculateActivation()
        {
            //threat based on variable
            float dist = Vector2.Distance(actor.Pos, target.Pos);

            activationLevel = actor.GetSearchRadius() / ((dist - actor.GetObjectRadius()));
            CheckBounds();

            Console.WriteLine(activationLevel);
            return activationLevel;    // if closer than searchRadius activation > 1, add to activatedStates        
        }

        public void SetIsActive(bool isActiveState)
        {
            CanActivate = isActiveState;
        }

        public bool GetIsActive()
        {
            return CanActivate;
        }

        public void CheckBounds(float lb = 0.0f, float ub = 1.0f)
        {
            CheckLowerBound(lb);
            CheckUpperBound(ub);
        }

        public void CheckLowerBound(float lbound = 0.0f)
        {
            if (activationLevel < lbound)
                activationLevel = lbound;
        }

        public void CheckUpperBound(float ubound = 1.0f)
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

        public void Update()
        {
            Vector2 tempDir = new Vector2(target.Pos.X - actor.Pos.X, target.Pos.Y - actor.Pos.Y);
            actor.Direction = Vector2.Normalize(tempDir);
        }

        public string GetName()
        {
            return name;
        }
    }
}
