﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids_2_Return_of_the_Asteroids
{
    class PirateBase:ShipBase
    {                
        public PirateBase(Vector2 pos):base(pos)
        {
            
        }

        virtual public Vector2 CollisionAvoidance(Vector2 pos,Vector2 direction, int radius)
        {
            var maxSeeAhead = 100;
            var ahead = pos + direction * maxSeeAhead; // max_see_ahead          
            Asteroid d = null;
            //var ahead2 = pos + direction * maxSeeAhead * 0.5f;
            for (int a = 0; a < 300; a++)
            {
                var ahead2 = pos + direction * a;
                d = FindMTO(pos, ahead, ahead2, radius);
            }

            var mostThreatening = d;
            //var mostThreatening = FindMTO(pos, ahead, ahead2, radius);
            var avoidance = new Vector2(0, 0);
            if (mostThreatening != null)
            {
                avoidance.X = ahead.X - mostThreatening.Pos.X;
                avoidance.Y = ahead.Y - mostThreatening.Pos.Y;

                avoidance.Normalize();
                avoidance *= 2f; // max_avoid_force
            }
            else
                avoidance *= 0;
            return avoidance;
        }

        private Asteroid FindMTO(Vector2 pos, Vector2 ahead, Vector2 ahead2, int radius)
        {
            Asteroid mostThreatening = null;
            for (int i = 0; i < GameplayManager.asteroids.Count - 1; i++)
            {
                Asteroid obstacle = GameplayManager.asteroids[i];
               // if(Vector2.Distance(obstacle.pos, pos) < 200)
              //  {
                    bool collision = LineIntersectsCircle(ahead, ahead2, obstacle, radius);
                    if (collision && (mostThreatening == null ||
                        Vector2.Distance(pos, obstacle.Pos) < Vector2.Distance(pos, mostThreatening.Pos)))
                    {
                        mostThreatening = obstacle;
                    }
               // }                
            }
            return mostThreatening;
        }

        private bool LineIntersectsCircle(Vector2 ahead, Vector2 ahead2, Asteroid obstacle, int radius)
        {
            return Vector2.Distance(obstacle.Pos, ahead) <= obstacle.Radius + radius ||
           Vector2.Distance(obstacle.Pos, ahead2) <= obstacle.Radius + radius;          
        }
    }
}
