using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids_2_Return_of_the_Asteroids
{
    public class ParticleEngine
    {
        private Random random;
        public Vector2 EmitterLocation { get; set; }
        private List<Particle> particles;
        private List<Texture2D> textures;

        Vector2 velocity;
        Color color;
        float angle;
        float angularVelocity;
        float size;
        int ttl;

        Color colorStates;
        List<Color> colourPallet = new List<Color>();

        public bool isAfterBurner = false;
        public bool isAsteroidExplosion = false;
        public bool isAsteroidHit = false;

        int asteroidExplosionTimer;

        public ParticleEngine(List<Texture2D> textures, Vector2 location)
        {
            this.textures = textures;
            EmitterLocation = location;           

            this.particles = new List<Particle>();
            random = new Random();
                        
            colourPallet.Add(Color.Yellow);            
            colourPallet.Add(Color.OrangeRed);
        }

        private Particle GenerateNewParticle(Color color)
        {
            Texture2D texture = textures[random.Next(textures.Count)];
            Vector2 position = EmitterLocation;

            if (isAfterBurner)
            {
                velocity = new Vector2(
                     0.1f * (float)(random.NextDouble() * 5 - 1),
                     0.1f * (float)(random.NextDouble() * 5 - 1));
                angle = 0f;
                angularVelocity = 0.1f * (float)(random.NextDouble() * 2 - 1);
            }
            else if (isAsteroidExplosion)
            {
                velocity = new Vector2(
                    10f * (float)(random.NextDouble() * 2 - 1),
                    10f * (float)(random.NextDouble() * 2 - 1));
                angle = 90f;                
                angularVelocity = 20f * (float)(random.NextDouble() * 2 - 1);
            }
            else if (isAsteroidHit)
            {
                velocity = new Vector2(
                    10f * (float)(random.NextDouble() * 2 - 1),
                    10f * (float)(random.NextDouble() * 2 - 1));
                angle = 90f;              
                angularVelocity = 20f * (float)(random.NextDouble() * 2 - 1);
            }
            else
            {
                velocity = new Vector2(
                   1f * (float)(random.NextDouble() * 2 - 1),
                   1f * (float)(random.NextDouble() * 2 - 1));
                angle = 0f;
                angularVelocity = 0.1f * (float)(random.NextDouble() * 2 - 1);
            }
            // float angle = 0f;
            // float angularVelocity = 0.1f * (float)(random.NextDouble() * 2 - 1);
            if (isAfterBurner)
            {
                color = new Color(
                   (float)random.NextDouble(),
                   (float)random.Next(0, 1),
                   (float)random.Next(0, 1));
               size = (float)random.NextDouble();
               ttl = 1 + random.Next(20);
            }
            else if (isAsteroidExplosion)
            {
                this.color = color; 
               size = (float)random.NextDouble();
               ttl = 1 + random.Next(25);
            }
            else if (isAsteroidHit)
            {
                color = new Color(
                (float)random.NextDouble(),
                   (float)random.Next(10, 50),
                      (float)random.Next(20, 50));
                size = (float)random.NextDouble();
                ttl = 1 + random.Next(10);
            }
            else
            {
                color = new Color(
                    (float)random.NextDouble(),
                    (float)random.NextDouble(),
                    (float)random.NextDouble());
                size = (float)random.NextDouble();
               ttl = 1 + random.Next(40);
            }

            //float size = (float)random.NextDouble();
            //int ttl = 1 + random.Next(40);

            return new Particle(texture, position, velocity, angle, angularVelocity, color, size, ttl);
        }

        public void Update()
        {
            if (isAsteroidExplosion)
            {
                while (asteroidExplosionTimer < 50)
                {                   
                    int total = 10;

                    if (asteroidExplosionTimer <= 10)
                    {
                        colorStates= Color.Gray;
                    }
                    else if (asteroidExplosionTimer > 20 && asteroidExplosionTimer <= 30)
                    {
                        colorStates = Color.Red;
                    }
                    else if (asteroidExplosionTimer > 30)
                    {
                        colorStates = colourPallet[random.Next(0, colourPallet.Count)];
                    }

                    for (int i = 0; i < total; i++)
                    {                
                        particles.Add(GenerateNewParticle(colorStates));
                    }
                    asteroidExplosionTimer++;
                }
            }
            else if (isAsteroidHit)
            {
                while (asteroidExplosionTimer < 20)
                {
                    int total = 10;

                    for (int i = 0; i < total; i++)
                    {
                        particles.Add(GenerateNewParticle(Color.White));
                    }
                    asteroidExplosionTimer++;
                }
            }
            else if (isAfterBurner)
            {
                int total = 10;

                for (int i = 0; i < total; i++)
                {
                    particles.Add(GenerateNewParticle(Color.White));
                }
            }

            else
            {
                int total = 10;

                for (int i = 0; i < total; i++)
                {
                    particles.Add(GenerateNewParticle(Color.White));
                }
            }

            for (int particle = 0; particle < particles.Count; particle++)
            {
                particles[particle].Update();
                if (particles[particle].TTL <= 0)
                {
                    particles.RemoveAt(particle);
                    particle--;
                }
            }
        }

        public void Draw(SpriteBatch sb)
        {

            for (int index = 0; index < particles.Count; index++)
            {
                particles[index].Draw(sb);
            }

        }
    }
}
