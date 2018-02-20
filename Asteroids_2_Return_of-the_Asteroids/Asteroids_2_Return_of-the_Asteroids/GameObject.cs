using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids_2_Return_of_the_Asteroids
{
    class GameObject
    {       
        public Vector2 Pos { get; set; }
        protected Rectangle hitbox;
        public GameObject(Vector2 pos)
        {          
          
        }

        public virtual void Update(GameTime gt)
        {

        }

        public virtual void Draw(SpriteBatch sb)
        {

        }
    }
}
