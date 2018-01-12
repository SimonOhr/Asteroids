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
            hitbox = new Rectangle((int)pos.X, (int)pos.Y, 100, 100);

            tex = AssetsManager.shipTex;

            EffectsManager.CreateAfterBurnerEffect(Pos);

            weapon = new LaserCanon(Pos);

            hitPoints = 3;
            speed = 4;
            originalSpeed = speed;           
        }

        public override void Update(GameTime gt)
        {            
            weapon.SetPos(Pos);
            weapon.Update(gt);               
            Console.WriteLine(speed);
            base.Update(gt);
        }        

        public override void Draw(SpriteBatch sb)
        {
            weapon.Draw(sb);
            sb.Draw(tex, Pos, null, color, currentRotation + MathHelper.ToRadians(90), new Vector2(tex.Width / 2, tex.Height / 2), 1, SpriteEffects.FlipVertically, 1);
            //base.Draw(sb);
        }
    }
}
