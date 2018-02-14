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

        public FusmMachine(Pirate actor, PlayerShip target)
        {
            activeStates = new List<IFusmState>();
            nonActiveStates = new List<IFusmState>();
            states = new List<IFusmState>();
            rules = new Fusm_RuleBook(this, actor, target);
        }

        public void Update()
        {
            //if (states.Count != 0)
            //{
            //    activeStates.Clear();
            //    for (int i = 0; i < states.Count; i++)
            //    {
            //        if (states[i].CalculateActivation() > 1)
            //            activeStates.Add(states[i]);
            //        else
            //            nonActiveStates.Add(states[i]);
            //    }
            //    if (nonActiveStates.Count > 0)
            //        for (int i = 0; i < nonActiveStates.Count; i++)
            //        {
            //            nonActiveStates[i].Exit();
            //        }
            //    if (activeStates.Count > 0)
            //        for (int i = 0; i < activeStates.Count; i++)
            //        {
            //            activeStates[i].Update();
            //        }
            //}
            rules.States();
            if (states.Count != 0)
            {
                activeStates.Clear();
                for (int i = 0; i < states.Count; i++)
                {
                    if (states[i].GetName() is "Attack")
                    {
                        var temp = rules.GetLikelyhoodToAttack();
                        if (temp == Fusm_RuleBook.LikelyhoodToAttack.low)
                            nonActiveStates.Add(states[i]);

                        else if (temp == Fusm_RuleBook.LikelyhoodToAttack.medium)
                        {
                            var tempActivationLevel = states[i].CalculateActivation();
                            if (tempActivationLevel + 0.5f >= 1)
                                activeStates.Add(states[i]);
                        }

                        else
                            activeStates.Add(states[i]);
                    }
                    else if (states[i].GetName() is "Chase")
                    {
                        var temp = rules.GetLikelyhoodToChase();
                        if (temp == Fusm_RuleBook.LikelyhoodToChase.low)
                            nonActiveStates.Add(states[i]);

                        else if (temp == Fusm_RuleBook.LikelyhoodToChase.medium)
                        {
                            var tempActivationLevel = states[i].CalculateActivation();
                            if (tempActivationLevel + 0.5f >= 1)
                                activeStates.Add(states[i]);
                        }

                        else
                            activeStates.Add(states[i]);
                    }
                    else
                    {
                        var temp = rules.GetLikelyhoodToEscape();
                        if (temp == Fusm_RuleBook.LikelyhoodToEscape.low)
                            nonActiveStates.Add(states[i]);

                        else if (temp == Fusm_RuleBook.LikelyhoodToEscape.medium)
                        {
                            var tempActivationLevel = states[i].CalculateActivation();
                            if (tempActivationLevel + 0.5f >= 1)
                                activeStates.Add(states[i]);
                        }

                        else
                            activeStates.Add(states[i]);
                    }
                }
                if (nonActiveStates.Count > 0)
                    for (int i = 0; i < nonActiveStates.Count; i++)
                    {
                        nonActiveStates[i].Exit();
                    }
                if(activeStates.Count > 0)
                    for (int i = 0; i < activeStates.Count; i++)
                    {
                        activeStates[i].Update();
                    }
                //ResetStates();
                nonActiveStates.Clear();

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
