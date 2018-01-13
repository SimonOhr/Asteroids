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

        Color colorStates, color;
        List<Color> colourPallet = new List<Color>();        

        float velMultiplier, angularVelMultiplier, size, angularVelocity, angle, particlesPerTick;
        public float intensifier;
        int velSpeed, ttlMaxLength, ttl, totalTicks;     
       
        Random rnd = new Random();

        public ParticleEngine(List<Texture2D> textures, Vector2 location, TypeOfEffect effect)
        {
            this.textures = textures;
            EmitterLocation = location;

            this.particles = new List<Particle>();
            random = new Random();

            
            this.effect = effect;
        }

        private Particle GenerateNewParticle(Color color)
        {
            Texture2D texture = textures[random.Next(textures.Count)];
            Vector2 position = EmitterLocation;

            switch (effect)
            {
                case TypeOfEffect.AfterBurner:
                    velMultiplier = 0.5f;
                    velSpeed = 5;
                    angle = 0f;
                    angularVelMultiplier = 0.1f;
                    ttlMaxLength = 10;

                    colourPallet.Add(Color.Yellow);
                    colourPallet.Add(Color.OrangeRed);
                    colourPallet.Add(Color.Red);                    
                    color = colourPallet[rnd.Next(0, colourPallet.Count)];                   
                    size = (float)random.NextDouble()/3;

                    break;
                case TypeOfEffect.AsteroidExplosion:
                    velMultiplier = 10f;
                    velSpeed = 2;
                    angle = 90f;
                    angularVelMultiplier = 20f;
                    ttlMaxLength = 25;
                    this.color = color; // colorpallet
                    size = (float)random.NextDouble()/2;
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
                    size = (float)random.NextDouble()/2;
                    break;
            }

            velocity = new Vector2(
                       velMultiplier * (float)(random.NextDouble() * velSpeed - 1),
                       velMultiplier * (float)(random.NextDouble() * velSpeed - 1));
            //angle = 0f;
            angularVelocity = angularVelMultiplier * (float)(random.NextDouble() * 2 - 1);          
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

                    particlesPerTick = 3;
                    particlesPerTick *= intensifier;

                    for (int i = 0; i < particlesPerTick; i++)
                    {
                        particles.Add(GenerateNewParticle(Color.White));
                    }
                    break;
                case TypeOfEffect.AsteroidExplosion:
                    while (totalTicks < 50)
                    {
                        particlesPerTick = 2;

                        if (totalTicks <= 20)
                        {
                            colorStates = Color.Gray;                            
                        }
                        else if (totalTicks > 20 )
                        {                           
                            colorStates = colourPallet[random.Next(0, colourPallet.Count)];
                        }                       

                        for (int i = 0; i < particlesPerTick; i++)
                        {
                            particles.Add(GenerateNewParticle(colorStates));
                        }
                        totalTicks++;
                    }
                    break;
                case TypeOfEffect.AsteroidHit:
                    while (totalTicks < 20)
                    {
                        particlesPerTick = 1;

                        for (int i = 0; i < particlesPerTick; i++)
                        {
                            particles.Add(GenerateNewParticle(Color.White));
                        }
                        totalTicks++;
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
