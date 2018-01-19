using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids_2_Return_of_the_Asteroids
{
    class LaserProjectile: ProjectileBase
    {
        
        public LaserProjectile(Vector2 pos, Vector2 targetPos):base(pos, targetPos)
        {
            projectileTex = AssetsManager.laserProjectileTex;

            velocity = new Vector2(15, 15);

            gunRange = 800;

            hitbox = new Rectangle((int)pos.X, (int)pos.Y, 20, 30);
            
        }       

        public override void Update(GameTime gt)
        {                   
            base.Update(gt);
        }      

        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
        }
    }
}
