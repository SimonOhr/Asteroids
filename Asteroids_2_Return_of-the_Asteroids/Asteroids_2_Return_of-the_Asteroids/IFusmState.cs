using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids_2_Return_of_the_Asteroids
{
    public interface IFusmState
    {
        void Init();

        void Enter();       

        float CalculateActivation();

        void CheckLowerBound(float lbound = 0.0f);

        void CheckUpperBound(float ubound = 1.0f);

        void CheckBounds(float lb = 0.0f, float ub = 1.0f);

        void Update();

        void Exit();
        string GetName();
    }
}
