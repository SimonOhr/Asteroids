using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids_2_Return_of_the_Asteroids
{
    class FusmMachine
    {
        Fusm_RuleBook rules;
        protected List<IFusmState> activeStates, nonActiveStates, states;
        float highestActivationLevel;
        IFusmState priorityState;

        public FusmMachine(Pirate actor, PlayerShip target)
        {
            activeStates = new List<IFusmState>();
            nonActiveStates = new List<IFusmState>();
            states = new List<IFusmState>();
            rules = new Fusm_RuleBook(this, actor, target);
        }

        public void Update()
        {
           
            rules.States();            
            if (states.Count != 0)
            {
                activeStates.Clear();
                for (int i = 0; i < states.Count; i++)
                {
                    rules.CheckRules(states[i]);
                    Console.WriteLine(states[i].CalculateActivation() + " " + states[i].GetName());
                    if (/*highestActivationLevel <= states[i].CalculateActivation() &&*/ states[i].GetCanActivate() && states[i].CalculateActivation() > 0)
                    {
                        //if (highestActivationLevel < states[i].CalculateActivation())
                        //{
                        //    highestActivationLevel = states[i].CalculateActivation();
                        //    priorityState = states[i];
                        //  //  Console.WriteLine(states[i].GetName());
                        //}   
                        activeStates.Add(states[i]);
                       // Console.WriteLine(states[i].GetName());
                    }
                    //else
                    //    nonActiveStates.Add(states[i]);                    
                }
               // activeStates.Add(priorityState);

                //if (nonActiveStates.Count > 0)
                //    for (int i = 0; i < nonActiveStates.Count; i++)
                //    {
                //        nonActiveStates[i].Exit();
                //    }
                if (activeStates.Count > 0)
                    for (int i = 0; i < activeStates.Count; i++)
                    {
                        activeStates[i].Enter();
                        activeStates[i].Update();
                        activeStates[i].Exit();
                    }
                //ResetStates();
                nonActiveStates.Clear();
                states.Clear();

            }
        }
        public void AddState(IFusmState state)
        {
            states.Add(state);
        }

        //private void ResetStates()
        //{
        //    for (int i = 0; i < nonActiveStates.Count; i++)
        //    {
        //        states.Add(nonActiveStates[i]);
        //    }

        //    for (int i = 0; i < activeStates.Count; i++)
        //    {
        //        states.Add(activeStates[i]);
        //    }
        //}

        // bool ACtive??

        public void Reset()
        {
            for (int i = 0; i < states.Count; i++)
            {
                states[i].Exit();
                states[i].Init();
            }
        }

        //public List<IFusmState> GetStates()
        //{
        //    return states;
        //}
    }
}
