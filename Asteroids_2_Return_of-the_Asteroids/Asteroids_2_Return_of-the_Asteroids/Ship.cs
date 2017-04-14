using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids_2_Return_of_the_Asteroids
{
    class Ship
    {       
        Vector2 mousePos;

        Texture2D shipTex;
        public Vector2 shipPos;
        Rectangle shipHitBox;

        public int Hitpoints { get; private set; }

        public int DamageOutput { get; private set; }

        float speed;        

        Vector2 directionOfShip;        
        Vector2 rotationTarget;
        float currentRotation;              

        public Ship(Vector2 pos, Vector2 mousePos)
        {
            this.mousePos = mousePos;
            shipTex = AssetsManager.shipTex;
            shipPos = pos;
            shipHitBox = new Rectangle((int)shipPos.X, (int)shipPos.Y, 50, 50);
            speed = 3;

        }
        public void Update(GameTime gt)
        {
            shipHitBox.X = (int)shipPos.X;
            shipHitBox.Y = (int)shipPos.Y;

            mousePos.X = Mouse.GetState().X;
            mousePos.Y = Mouse.GetState().Y;

            rotationTarget = mousePos;

            MovementInput();

            shipPos += speed * GetDirection();   
            
        }

        private void MovementInput()
        {                         

            if (Vector2.Distance(mousePos, shipPos) < 4)
            {                
                speed = 0;
            }
            else if (Vector2.Distance(mousePos, shipPos) >= 4)
            {                
                speed = 3;
            }          

            ShipRotation();
            GetDirection();
        }

        private void ShipRotation()
        {
            directionOfShip = mousePos - (shipPos);
            currentRotation = (float)Math.Atan2(directionOfShip.Y, directionOfShip.X);
        }
       
        private Vector2 GetDirection()
        {
            Vector2 Direction = new Vector2(Mouse.GetState().X - shipPos.X, Mouse.GetState().Y - shipPos.Y);
            return Vector2.Normalize(Direction);
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(shipTex, shipPos, null, Color.White, currentRotation + MathHelper.ToRadians(90), new Vector2(shipTex.Width / 2, shipTex.Height / 2), 1, SpriteEffects.None, 1);
        }       
    }
}
