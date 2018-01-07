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
    class Projectile
    {
        Texture2D projectileTex;

        public Vector2 projectilePos;

        Vector2 speed;

        Vector2 direction;

        Vector2 originalPos;

        Vector2 targetPos;

        int gunRange;

        public bool removeProjectile;

        Rectangle projectileHitbox;

        Vector2 directionOfProjectile;
        float rotation;        

        public Projectile(Vector2 projectilePos, Vector2 targetPos)
        {
            projectileTex = AssetsManager.projectileTex;

            this.projectilePos = projectilePos;

            originalPos = projectilePos;

            this.targetPos = targetPos;

            speed = new Vector2(10, 10);

            direction = GetDirection();

            gunRange = 800;

            GetRotation();

            projectileHitbox = new Rectangle((int)projectilePos.X, (int)projectilePos.Y, 20, 30);
        }

        public void Update(GameTime gt)
        {
            projectilePos += speed * direction;
            projectileHitbox.X = (int)projectilePos.X;
            projectileHitbox.Y = (int)projectilePos.Y;

            if (Vector2.Distance(projectilePos, originalPos) >= gunRange)
            {
                removeProjectile = true;
            }         
        }

        private Vector2 GetDirection()
        {
            Vector2 Direction = new Vector2(targetPos.X - projectilePos.X, targetPos.Y - projectilePos.Y);
            return Vector2.Normalize(Direction);
        }

        private void GetRotation()
        {
            directionOfProjectile = Mouse.GetState().Position.ToVector2() - projectilePos;
            rotation = (float)Math.Atan2(directionOfProjectile.Y, directionOfProjectile.X);
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(projectileTex, projectilePos,null, Color.White,rotation + MathHelper.ToRadians(90), new Vector2(projectileTex.Width / 2 , projectileTex.Height / 2), 1, SpriteEffects.None, 1);
        }
    }
}
