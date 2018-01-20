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
    class PlayerShip : PlayerShipBase
    {
        int healthmultiplier = (AssetsManager.healthBarTex.Width / 10);
        public PlayerShip(Vector2 pos, Vector2 mousePos) : base(pos, mousePos)
        {
            tex = AssetsManager.shipTex;

            hitbox = new Rectangle((int)pos.X, (int)pos.Y, tex.Width, tex.Height);

            hitPoints = 10;

            srcHealthbarTex = new Rectangle(0, 0, hitPoints * healthmultiplier, AssetsManager.healthBarTex.Height);

            EffectsManager.CreateAfterBurnerEffect(Pos);

            weapons.Add(new LaserCanon(Pos));
            weapons.Add(new MiniMissileLauncher(pos));

           
            speed = 4;
            originalSpeed = speed;
           
        }

        public override void Update(GameTime gt)
        {
            EffectsManager.UpdateAfterBurnerEffect(Pos - (GetDirection() * 50));

            foreach (WeaponBase w in weapons)
            {
                w.SetPos(Pos);
            }

            AfterburnerIntensifier();

            if (srcHealthbarTex.Width != hitPoints * healthmultiplier)
            {
                srcHealthbarTex.Width = hitPoints * healthmultiplier;
            }

            base.Update(gt);
        }

        public override void Draw(SpriteBatch sb)
        {
            foreach (WeaponBase w in weapons)
            {
                w.Draw(sb);
            }
            sb.Draw(healthbarTex, Vector2.Zero, srcHealthbarTex, Color.White);
            sb.Draw(tex, Pos, null, color, currentRotation + MathHelper.ToRadians(90), new Vector2(tex.Width / 2, tex.Height / 2), 1, SpriteEffects.FlipVertically, 1);
            // base.Draw(sb);
        }

        private void AfterburnerIntensifier()
        {
            EffectsManager.SetAfterBurnerIntensity(speed);
        }
    }
}
