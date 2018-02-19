using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids_2_Return_of_the_Asteroids.AIBehaviour.Fusm
{
    class DecisionNode
    {
        int dist, actorHealth, targetHealth;
        DecisionNode left, right;
        public DecisionNode(int dist, int actorHealth, int targetHealth)
        {
            this.dist = dist;
            this.actorHealth = actorHealth;
            this.targetHealth = targetHealth;
        }

        public void Insert(int dist, int actorHealth, int targetHealth)
        {
            if (dist <= this.dist && actorHealth <= this.actorHealth && targetHealth <= this.targetHealth)
            {
                if (left == null) left = new DecisionNode(dist, actorHealth, targetHealth);
                else left.Insert(dist, actorHealth, targetHealth);
            }
            else
            {
                if (right == null) right = new DecisionNode(dist, actorHealth, targetHealth);
                else right.Insert(dist, actorHealth, targetHealth);
            }                    
            
            //if (value <= data)
            //{
            //    if (left == null) left = new DecisionNode(value);
            //    else left.Insert(value);
            //}
            //else
            //{
            //    if (right == null) right = new DecisionNode(value);
            //    else right.Insert(value);
            //}
        }

        //public bool Contains(int value)
        //{
        //    if (value == data) return true;
        //    else if (value < data) if (left == null) return false;
        //        else return left.Contains(value);

        //    else
        //    {
        //        if (right == null) return false;
        //        else return right.Contains(value);
        //    }
        //}
    }
}
