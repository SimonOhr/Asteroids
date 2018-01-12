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
    class PlayerShipLevelOne:PlayerShipBase
    {        
        public PlayerShipLevelOne(Vector2 pos, Vector2 mousePos):base(pos, mousePos)
        {
            hitbox = new Rectangle((int)pos.X, (int)pos.Y, 50, 50);

            tex = AssetsManager.shipTex;

            EffectsManager.CreateAfterBurnerEffect(Pos);

            weapon = new LaserCanon(Pos);

            hitPoints = 2;
            speed = 3;
            originalSpeed = speed;           
        }

        public override void Update(GameTime gt)
        {            
            weapon.SetPos(Pos);
            weapon.Update(gt);
            MovementInput();
            base.Update(gt);
        }

        protected override void MovementInput()
        {

            if (Vector2.Distance(mousePos, Pos) < 4)
            {
                speed = 0;
            }
            else if (Vector2.Distance(mousePos, Pos) >= 4)
            {
                speed = originalSpeed;
            }           
        }       

        public override void Draw(SpriteBatch sb)
        {
            weapon.Draw(sb);
            base.Draw(sb);
        }
    }
}
