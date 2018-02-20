using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids_2_Return_of_the_Asteroids
{
    class MovingObject: GameObject
    {
        protected float speed;
        public Vector2 Direction { get; set; }        
        public MovingObject(Vector2 pos):base(pos)
        {
           
        }
    }
}
