using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids_2_Return_of_the_Asteroids
{
    static class EffectsManager
    {        

        static ParticleEngine afterburnerEffect;
        static List<Texture2D> afterBurnerTextures;

        static ParticleEngine asteroidIsHitParticles;
        static List<ParticleEngine> asteroidIsHitList = asteroidIsHitList = new List<ParticleEngine>();


        static ParticleEngine asteroidExplosion;
        static List<ParticleEngine> asteroidExplosionList = asteroidExplosionList = new List<ParticleEngine>();

        public static void CreateAfterBurnerEffect(Vector2 pos)
        {
            afterBurnerTextures = new List<Texture2D>();
            afterBurnerTextures.Add(AssetsManager.particleCircleTex);
            afterBurnerTextures.Add(AssetsManager.particleDiamondTex);

            afterburnerEffect = new ParticleEngine(afterBurnerTextures, pos, TypeOfEffect.AfterBurner);
        }

        public static void UpdateAfterBurnerEffect(Vector2 Pos)
        {
            afterburnerEffect.EmitterLocation = Pos;
            //afterburnerEffect.EmitterLocation = Pos + speed * GetDirection();
            afterburnerEffect.Update();
        }


        public static void CreateASteroidIsHitEffect(Vector2 pos)
        {
            asteroidIsHitParticles = new ParticleEngine(AssetsManager.textures, pos, TypeOfEffect.AsteroidHit);
            asteroidIsHitList.Add(asteroidIsHitParticles);
        }

        public static void UpdateAsteroidIsHitEffect()
        {
            if (asteroidIsHitList.Count > 0)
            {
                // int iterable = 0;

                foreach (ParticleEngine hitEffect in asteroidIsHitList)
                {
                    hitEffect.Update();
                }
                for (int i = 0; i < asteroidIsHitList.Count; i++)
                {
                    //  Console.WriteLine(asteroidIsHitList.Count);
                    if (asteroidIsHitList[i].particles.Count <= 0)
                    {
                        asteroidIsHitList.RemoveAt(i);
                    }
                }
            }
        }

        public static void CreateAsteroidExplosionEffect(Vector2 pos)
        {
            asteroidExplosion = new ParticleEngine(AssetsManager.textures, pos, TypeOfEffect.AsteroidExplosion);
            asteroidExplosionList.Add(asteroidExplosion);
        }

        public static void UpdateAsteroidExplosionEffect()
        {
            if (asteroidExplosionList.Count > 0)
            {
                foreach (ParticleEngine explosionEffect in asteroidExplosionList)
                {
                    explosionEffect.Update();
                }
                for (int i = 0; i < asteroidExplosionList.Count; i++)
                {
                    //  Console.WriteLine(asteroidExplosionList.Count); 
                    if (asteroidExplosionList[i].particles.Count <= 0)
                    {
                        asteroidExplosionList.RemoveAt(i);
                    }
                }
            }
        }

        public static void Draw(SpriteBatch sb)
        {
            afterburnerEffect.Draw(sb);

            if (asteroidIsHitList.Count > 0)
            {
                asteroidIsHitParticles.Draw(sb);
            }
            if (asteroidExplosionList.Count > 0)
            {
                asteroidExplosion.Draw(sb);
            }
        }

    }

}
