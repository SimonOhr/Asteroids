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

        public new Vector2 Pos { get; set; }

        protected Vector2 velocity, direction, originalPos, targetPos;       

        protected int projectileRange;

        public bool doRemove;               
      
        protected float rotation;

        public ShipBase objectOwner { get; set; }

        public ProjectileBase(Vector2 pos, Vector2 targetPos):base(pos)
        {           
            this.Pos = pos;

            originalPos = pos;

            this.targetPos = targetPos;         

            direction = GetDirection();          

            GetRotation(targetPos, pos);         
        }

        virtual protected Vector2 GetDirection()
        {
            Vector2 Direction = new Vector2(targetPos.X - Pos.X, targetPos.Y - Pos.Y);
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
            Pos += velocity * direction;
            hitbox.X = (int)Pos.X;
            hitbox.Y = (int)Pos.Y;
            CheckIfOutOfRange();
        }

        virtual protected void CheckIfOutOfRange()
        {
            if (Vector2.Distance(Pos, originalPos) >= projectileRange)
            {
                doRemove = true;
            }
        }       

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(projectileTex, Pos, null, Color.White, rotation + MathHelper.ToRadians(90), new Vector2(projectileTex.Width / 2 , projectileTex.Height / 2), 1, SpriteEffects.None, 1);
        }        
    }
}
