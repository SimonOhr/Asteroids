using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids_2_Return_of_the_Asteroids
{
    class Fuzzy_StateChase : IFusmState
    {
        string name;
        Pirate actor;
        PlayerShip target;
        float activationLevel;
        public bool CanActivate { get; private set; }
        int maxDistanceForActivation;


        public Fuzzy_StateChase(Pirate actor, PlayerShip target)
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

            if (dist > actor.GetSearchRadius() * 2)
                return activationLevel = 0;
            if (dist <= 0)
                return activationLevel = 0;
            if (dist / actor.GetSearchRadius() < 1)
            {
               // Console.WriteLine("TEST ACTIAVTION LEVEL: " + (dist / actor.GetSearchRadius()));
                return activationLevel = dist / actor.GetSearchRadius();
            }

            else
            {
                float temp = (1 - dist / actor.GetSearchRadius());
               // Console.WriteLine("TEST ACTIVATION LEVEL TEMP" + temp);
               // Console.WriteLine("TEST ACTIAVTION LEVEL: " + (activationLevel + 1 + temp));
                return activationLevel += 1 + temp;
            }

            //CheckBounds();
            ////Console.WriteLine(activationLevel);
            //return activationLevel;    // if closer than searchRadius activation > 1, add to activatedStates        
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
            float temp = 0;
            if (activationLevel > ubound)
            {
                if (activationLevel > 2)
                    activationLevel = 2;

                temp = activationLevel - 1; // creates a triangle
                activationLevel = ubound - temp;
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

          //  actor.SetSpeed(new Vector2(actor.GetSpeed().X * activationLevel, actor.GetSpeed().Y * activationLevel));
            Vector2 temp = actor.GetBaseVelocity();
            Console.WriteLine("ACTOR NEW SPEED " + temp * actor.GetBaseVelocity());
            actor.SetSpeed(temp * activationLevel);
        }

        public string GetName()
        {
            return name;
        }
    }
}
