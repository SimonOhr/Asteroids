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
    class MiniMissileProjectile : ProjectileBase
    {
        double timer;
        float timerMax, reset, accelerationMultiplier;

        int curvePointMultiplier;
        Vector2 curvePoint;        
        
        Vector2 wayPoint = new Vector2(0, 0);
        List<Vector2> curvePath = new List<Vector2>();
        int listCount, it, aimAssistMultiplier, curveLocationMultipler;
       
        Vector2 aimAssist;

        Random rnd;

        public MiniMissileProjectile(Vector2 pos, Vector2 targetPos) : base(pos, targetPos)
        {
            this.pos = pos;
            this.targetPos = targetPos;

            velocity = new Vector2(1, 1);

            aimAssistMultiplier = 250;
            gunRange = 1200;
            rotation = 90;
            timer = 0;
            timerMax = 0.005f;
            reset = 0;
            curvePointMultiplier = 500;
            it = 1;
            accelerationMultiplier = 1.02f;
            curveLocationMultipler = 200;

            aimAssist = targetPos + (velocity) * (GetDirection() * aimAssistMultiplier);
            originalPos = pos;

            projectileTex = AssetsManager.missileDebuggTex;
                      
            hitbox = new Rectangle((int)pos.X, (int)pos.Y, 20, 20);
           
            rnd = new Random();
          
            GetRotation();
            curvePoint = CurveLocation();
                        
            CreatePath();
            
            Console.WriteLine(curvePath.Count);
        }

        public override void Update(GameTime gt)
        {
            GetRotation();

            pos += velocity * direction;
            hitbox.X = (int)pos.X;
            hitbox.Y = (int)pos.Y;
            CheckIfOutOfRange();

            timer += gt.ElapsedGameTime.TotalSeconds;
            if (timer > timerMax)
            {
                if (it < listCount) GetTargetNewVector();
                if (it == listCount) velocity = new Vector2(accelerationMultiplier * velocity.X, accelerationMultiplier * velocity.Y);
                timer = reset;
            }

            Console.WriteLine(pos);
            base.Update(gt);
        }

        private Vector2 CurveLocation()
        {                     
            return pos - (AngleToVector(MathHelper.ToDegrees(rotation)) * curveLocationMultipler); 
        }
        
        private Vector2 AngleToVector(float angle)
        {           
            return new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
        }

        private void CreatePath()
        {
            for (double t = 0; t <= 1; t += 0.005)
            {
                CalcQuadBezierPath(originalPos, curvePoint, targetPos, (float)t);
            }
            listCount = curvePath.Count -1;
            GetTargetNewVector();
        }
        private void CalcQuadBezierPath(Vector2 p0, Vector2 p1, Vector2 p2, float t)
        {
            wayPoint.X = (float)Math.Pow(1 - t, 2) * p0.X + (1 - t) * 2 * t * p1.X + t * t * p2.X;
            wayPoint.Y = (float)Math.Pow(1 - t, 2) * p0.Y + (1 - t) * 2 * t * p1.Y + t * t * p2.Y;             
            curvePath.Add(wayPoint);
        }

        private void GetTargetNewVector()
        {           
            it++;
            if (it == (listCount / 2))
            {
                it = listCount;
                targetPos = aimAssist;               
            }
            else targetPos = curvePath[it];

            direction = GetDirection();
        }      

        override protected void GetRotation()
        {
            Vector2 directionOfProjectile = targetPos - pos;
            rotation = (float)Math.Atan2(directionOfProjectile.Y, directionOfProjectile.X);
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(projectileTex, pos, null, Color.White, rotation, new Vector2(projectileTex.Width / 2, projectileTex.Height / 2), 1, SpriteEffects.None, 1);
           
        }
    }
}
