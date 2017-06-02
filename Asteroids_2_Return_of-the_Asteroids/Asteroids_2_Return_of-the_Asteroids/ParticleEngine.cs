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

        public bool isAfterBurner;
        public bool isAsteroidExplosion;

        public ParticleEngine(List<Texture2D> textures, Vector2 location, bool isAfterBurner, bool isAsteroidExplosion)
        {
            this.textures = textures;
            EmitterLocation = location;

            this.isAfterBurner = isAfterBurner;
            this.isAsteroidExplosion = isAsteroidExplosion;

            this.particles = new List<Particle>();
            random = new Random();
        }

        private Particle GenerateNewParticle()
        {
            Texture2D texture = textures[random.Next(textures.Count)];
            Vector2 position = EmitterLocation;

            if (isAfterBurner)
            {
                velocity = new Vector2(
                     0.1f * (float)(random.NextDouble() * 5 - 1),
                     0.1f * (float)(random.NextDouble() * 5 - 1));
            }
            else if (isAsteroidExplosion)
            {
                velocity = new Vector2(
                    5f * (float)(random.NextDouble() * 5 - 1),
                    5f * (float)(random.NextDouble() * 5 - 1));
            }
            else
            {
                velocity = new Vector2(
                   1f * (float)(random.NextDouble() * 2 - 1),
                   1f * (float)(random.NextDouble() * 2 - 1));
            }
            float angle = 0f;
            float angularVelocity = 0.1f * (float)(random.NextDouble() * 2 - 1);
            if (isAfterBurner)
            {
                color = new Color(
                   (float)random.NextDouble(),
                   (float)random.Next(0, 1),
                   (float)random.Next(0, 1));
            }
            else if (isAsteroidExplosion)
            {
                color = new Color(
                (float)random.NextDouble(),
                   (float)random.Next(10,50),
                      (float)random.Next(20,50));
            }
            else
            {
                color = new Color(
                    (float)random.NextDouble(),
                    (float)random.NextDouble(),
                    (float)random.NextDouble());
            }

            float size = (float)random.NextDouble();
            int ttl = 1 + random.Next(40);

            return new Particle(texture, position, velocity, angle, angularVelocity, color, size, ttl);
        }

        public void Update()
        {
            int total = 10;

            for (int i = 0; i < total; i++)
            {
                particles.Add(GenerateNewParticle());
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
