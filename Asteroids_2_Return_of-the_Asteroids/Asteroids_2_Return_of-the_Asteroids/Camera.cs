using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids_2_Return_of_the_Asteroids
{
    class Camera
    {
        private Matrix transform;
        private Vector2 position;
        private Viewport view;
               
        public Camera(Viewport view)
        {
            this.view = view;
        }
      
        public void SetPosition(Vector2 position)
        {
            this.position = position;
            transform = Matrix.CreateTranslation(-position.X + view.Width / 2, -position.Y + view.Height / 2, 0);
        }
      
        public Vector2 GetPosition()
        {
            return position;
        }
       
        public Matrix GetTransform()
        {
            return transform;
        }
    }
}
