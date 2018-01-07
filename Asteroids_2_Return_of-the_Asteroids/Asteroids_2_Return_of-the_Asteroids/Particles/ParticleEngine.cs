using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids_2_Return_of_the_Asteroids
{
    public enum TypeOfEffect { AfterBurner, AsteroidExplosion, AsteroidHit }

    public class ParticleEngine
    {
        TypeOfEffect effect;
        private Random random;
        public Vector2 EmitterLocation { get; set; }
        public List<Particle> particles { get; private set; }
        private List<Texture2D> textures;

        Vector2 velocity;
        Color color;
        float angle;
        float angularVelocity;
        float size;
        int ttl;

        Color colorStates;
        List<Color> colourPallet = new List<Color>();

        int total;

        float velMultiplier, angularVelMultiplier;
        int velSpeed, ttlMaxLength;

        int asteroidExplosionTimer;

        public ParticleEngine(List<Texture2D> textures, Vector2 location, TypeOfEffect effect)
        {
            this.textures = textures;
            EmitterLocation = location;

            this.particles = new List<Particle>();
            random = new Random();

            colourPallet.Add(Color.Yellow);
            colourPallet.Add(Color.OrangeRed);
            this.effect = effect;
        }


        private Particle GenerateNewParticle(Color color)
        {
            Texture2D texture = textures[random.Next(textures.Count)];
            Vector2 position = EmitterLocation;

            switch (effect)
            {
                case TypeOfEffect.AfterBurner:
                    velMultiplier = 0.1f;
                    velSpeed = 5;
                    angle = 0f;
                    angularVelMultiplier = 0.1f;
                    ttlMaxLength = 20;
                    color = new Color(
                       (float)random.NextDouble(),
                       (float)random.Next(0, 1),
                       (float)random.Next(0, 1));

                    break;
                case TypeOfEffect.AsteroidExplosion:
                    velMultiplier = 10f;
                    velSpeed = 2;
                    angle = 90f;
                    angularVelMultiplier = 20f;
                    ttlMaxLength = 25;
                    this.color = color; // colorpallet

                    break;
                case TypeOfEffect.AsteroidHit:
                    velMultiplier = 10f;
                    velSpeed = 2;
                    angle = 90f;
                    angularVelMultiplier = 20f;
                    ttlMaxLength = 10;
                    color = new Color(
                    (float)random.NextDouble(),
                       (float)random.Next(10, 50),
                          (float)random.Next(20, 50));

                    break;
            }

            velocity = new Vector2(
                       velMultiplier * (float)(random.NextDouble() * velSpeed - 1),
                       velMultiplier * (float)(random.NextDouble() * velSpeed - 1));
            angle = 0f;
            angularVelocity = angularVelMultiplier * (float)(random.NextDouble() * 2 - 1);
            size = (float)random.NextDouble();
            ttl = 1 + random.Next(ttlMaxLength);

            return new Particle(texture, position, velocity, angle, angularVelocity, color, size, ttl);
        }

        public void NewEfftect(TypeOfEffect effect)
        {
            this.effect = effect;
        }

        public void Update()
        {

            switch (effect)
            {
                case TypeOfEffect.AfterBurner:

                    total = 3;

                    for (int i = 0; i < total; i++)
                    {
                        particles.Add(GenerateNewParticle(Color.White));
                    }
                    break;
                case TypeOfEffect.AsteroidExplosion:
                    while (asteroidExplosionTimer < 50)
                    {
                        total = 2;

                        if (asteroidExplosionTimer <= 10)
                        {
                            colorStates = Color.Gray;
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
                    break;
                case TypeOfEffect.AsteroidHit:
                    while (asteroidExplosionTimer < 20)
                    {
                        total = 1;

                        for (int i = 0; i < total; i++)
                        {
                            particles.Add(GenerateNewParticle(Color.White));
                        }
                        asteroidExplosionTimer++;
                    }
                    break;               
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
