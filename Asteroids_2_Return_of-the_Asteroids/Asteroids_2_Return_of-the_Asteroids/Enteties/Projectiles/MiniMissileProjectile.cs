using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids_2_Return_of_the_Asteroids
{
    class MiniMissileProjectile: ProjectileBase
    {

        public MiniMissileProjectile(Vector2 pos, Vector2 targetPos):base(pos, targetPos)
        {
            projectileTex = AssetsManager.missileDebuggTex;

            vSpeed = new Vector2(1, 1);

            gunRange = 1200;

            hitbox = new Rectangle((int)pos.X, (int)pos.Y, 20, 20);

            //this.pos = pos;
            //originalPos = pos;
        }

        public override void Update(GameTime gt)
        {
            for (double t = 0; t <= 1; t += 0.01)
            {
                CalcQuadBezierPath(originalPos, Vector2.Zero, targetPos, (float)t);
            }

            base.Update(gt);
        }

        private Vector2 CalcQuadBezierPath(Vector2 p0, Vector2 p1, Vector2 p2, float t)
        {
            pos.X = (float)Math.Pow(1 - t, 2) * p0.X + (1 - t) * 2 * t * p1.X + t * t * p2.X;
            pos.Y = (float)Math.Pow(1 - t, 2) * p0.Y + (1 - t) * 2 * t * p1.Y + t * t * p2.Y;
            return pos;
        }

        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
        }
    }
}
