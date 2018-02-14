using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids_2_Return_of_the_Asteroids
{
    public interface IState
    {       
        void Enter();

        void Execute();       

        void Exit();
    }
}
