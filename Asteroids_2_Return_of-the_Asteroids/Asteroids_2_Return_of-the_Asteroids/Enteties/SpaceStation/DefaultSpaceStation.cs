using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids_2_Return_of_the_Asteroids
{
    class DefaultSpaceStation:GameObject
    {
        Texture2D tex;
        public DefaultSpaceStation(Vector2 pos):base(pos)
        {
            tex = AssetsManager.spaceStationTex;
            hitbox = new Rectangle((int)pos.X, (int)pos.Y, 50, 50);
            this.pos = pos;
        }

        public override void Update(GameTime gt)
        {
            base.Update(gt);
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(tex, pos, Color.White);
            base.Draw(sb);
        }
    }
}
