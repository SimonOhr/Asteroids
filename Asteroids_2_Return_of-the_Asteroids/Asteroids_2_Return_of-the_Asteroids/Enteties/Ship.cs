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
    class Ship
    {
        Vector2 mousePos;
        Color color;
        public Texture2D ShipTex { get; private set; }
        public Vector2 ShipPos { get; private set; }
        Rectangle shipHitBox;

        public static int hitPoints;
        public bool isHit;

        public int DamageOutput { get; private set; }

        float speed;

        TypeOfEffect effect;
        ParticleEngine shipAfterburnerParticles;
        List<Texture2D> afterBurnerTextures;

        Vector2 directionOfShip;
        Vector2 rotationTarget;
        float currentRotation;

        public Ship(Vector2 pos, Vector2 mousePos)
        {
            this.mousePos = mousePos;
            color = Color.White;
            ShipTex = AssetsManager.shipTex;
            ShipPos = pos;
            shipHitBox = new Rectangle((int)ShipPos.X, (int)ShipPos.Y, 50, 50);
            hitPoints = 2;
            speed = 3;

            afterBurnerTextures = new List<Texture2D>();
            afterBurnerTextures.Add(AssetsManager.particleCircleTex);
            afterBurnerTextures.Add(AssetsManager.particleDiamondTex);

            shipAfterburnerParticles = new ParticleEngine(afterBurnerTextures, ShipPos, effect = TypeOfEffect.AfterBurner);
            //shipAfterburnerParticles.isAfterBurner = true;

        }
        public void Update(GameTime gt)
        {
            shipHitBox.X = (int)ShipPos.X;
            shipHitBox.Y = (int)ShipPos.Y;

            mousePos.X = Mouse.GetState().X;
            mousePos.Y = Mouse.GetState().Y;

            MovementInput();

            ShipPos += speed * GetDirection();

            if (hitPoints <= 0)
            {
                color = Color.Blue;
            }

            shipAfterburnerParticles.EmitterLocation = ShipPos + speed * GetDirection();
            shipAfterburnerParticles.Update();
        }

        private void MovementInput()
        {

            if (Vector2.Distance(mousePos, ShipPos) < 4)
            {
                speed = 0;
            }
            else if (Vector2.Distance(mousePos, ShipPos) >= 4)
            {
                speed = 3;
            }

            if (isHit)
            {
                color = Color.Red;
            }
            else if (!isHit)
            {
                color = Color.White;
            }

            ShipRotation();
            GetDirection();
        }

        private void ShipRotation()
        {
            directionOfShip = mousePos - (ShipPos);
            currentRotation = (float)Math.Atan2(directionOfShip.Y, directionOfShip.X);
        }

        private Vector2 GetDirection()
        {
            Vector2 Direction = new Vector2(Mouse.GetState().X - ShipPos.X, Mouse.GetState().Y - ShipPos.Y);
            return Vector2.Normalize(Direction);
        }

        public void Draw(SpriteBatch sb)
        {
            shipAfterburnerParticles.Draw(sb);

            sb.Draw(ShipTex, ShipPos, null, color, currentRotation + MathHelper.ToRadians(90), new Vector2(ShipTex.Width / 2, ShipTex.Height / 2), 1, SpriteEffects.None, 1);

        }
    }
}
