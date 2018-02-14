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
    class ProjectileBase:GameObject
    {
        protected Texture2D projectileTex;

        public new Vector2 pos;

        protected Vector2 velocity, direction, originalPos, targetPos;       

        protected int projectileRange;

        public bool doRemove;               
      
        protected float rotation;        

        public ProjectileBase(Vector2 pos, Vector2 targetPos):base(pos)
        {           
            this.pos = pos;

            originalPos = pos;

            this.targetPos = targetPos;         

            direction = GetDirection();          

            GetRotation(targetPos, pos);         
        }

        virtual protected Vector2 GetDirection()
        {
            Vector2 Direction = new Vector2(targetPos.X - pos.X, targetPos.Y - pos.Y);
            return Vector2.Normalize(Direction);
        }
        //KeyMouseReader.cursorViewToWorldPosition
        virtual protected void GetRotation(Vector2 targetPos, Vector2 pos)
        {
            Vector2 directionOfProjectile = targetPos - pos;
            rotation = (float)Math.Atan2(directionOfProjectile.Y, directionOfProjectile.X);
        }

        public override void Update(GameTime gt)
        {
            pos += velocity * direction;
            hitbox.X = (int)pos.X;
            hitbox.Y = (int)pos.Y;
            CheckIfOutOfRange();
        }

        virtual protected void CheckIfOutOfRange()
        {
            if (Vector2.Distance(pos, originalPos) >= projectileRange)
            {
                doRemove = true;
            }
        }       

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(projectileTex, pos, null, Color.White, rotation + MathHelper.ToRadians(90), new Vector2(projectileTex.Width / 2 , projectileTex.Height / 2), 1, SpriteEffects.None, 1);
        }
    }
}
