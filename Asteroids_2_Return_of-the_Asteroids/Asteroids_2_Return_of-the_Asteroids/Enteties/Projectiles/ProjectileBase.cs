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
    class ProjectileBase:Weapon
    {
        protected Texture2D projectileTex;

        public Vector2 pos;

        protected Vector2 vSpeed, direction, originalPos, targetPos;       

        protected int gunRange;

        public bool doRemove;               
      
        protected float rotation;        

        public ProjectileBase(Vector2 pos, Vector2 targetPos):base(pos)
        {
            //projectileTex = AssetsManager.laserProjectileTex;

            this.pos = pos;

            originalPos = pos;

            this.targetPos = targetPos;

          //  vSpeed = new Vector2(10, 10);

            direction = GetDirection();

            gunRange = 800;

            GetRotation();

          //  hitbox = new Rectangle((int)pos.X, (int)pos.Y, 20, 30);
        }

        virtual protected Vector2 GetDirection()
        {
            Vector2 Direction = new Vector2(targetPos.X - pos.X, targetPos.Y - pos.Y);
            return Vector2.Normalize(Direction);
        }

        virtual protected void GetRotation()
        {
            Vector2 directionOfProjectile = Mouse.GetState().Position.ToVector2() - pos;
            rotation = (float)Math.Atan2(directionOfProjectile.Y, directionOfProjectile.X);
        }

        public override void Update(GameTime gt)
        {
            pos += vSpeed * direction;
            hitbox.X = (int)pos.X;
            hitbox.Y = (int)pos.Y;
            CheckIfOutOfRange();
        }

        virtual protected void CheckIfOutOfRange()
        {
            if (Vector2.Distance(pos, originalPos) >= gunRange)
            {
                doRemove = true;
            }
        }       

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(projectileTex, pos, null, Color.White,rotation + MathHelper.ToRadians(90), new Vector2(projectileTex.Width / 2 , projectileTex.Height / 2), 1, SpriteEffects.None, 1);
        }
    }
}
