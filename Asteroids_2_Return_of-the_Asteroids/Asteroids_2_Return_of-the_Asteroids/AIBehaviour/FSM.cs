using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids_2_Return_of_the_Asteroids
{
    public class FSM
    {
        private IState currentState, previousState;
        private bool stateDone;

        public void ChangeState(IState activeState)
        {
            if (currentState != null)
                currentState.Exit();
            previousState = currentState;

            currentState = activeState;
            currentState.Enter();
        }

        public void UpdateState()
        {
            if (currentState != null)
                currentState.Execute();
        }

        public void RevertToPreviousState()
        {
            currentState.Exit();
            currentState = previousState;
            currentState.Enter();
        }       
    }
}
