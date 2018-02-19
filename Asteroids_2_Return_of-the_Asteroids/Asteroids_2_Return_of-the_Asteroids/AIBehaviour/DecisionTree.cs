using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids_2_Return_of_the_Asteroids
{
    class DecisionTree
    {
        Pirate actor;
        PlayerShip target;         
        public DecisionTree(Pirate actor, PlayerShip target)
        {
            this.actor = actor;
            this.target = target;

        }

        public IState CommenceIfStatements()
        {
            actor.GetHealth();
            target.GetHealth();
            var dist = Vector2.Distance(actor.Pos, target.Pos);

            if (dist < actor.GetSearchRadius() || dist < actor.GetAttackRadius())
            {
                if (actor.GetHealth() > target.GetHealth() && dist >= (actor.GetAttackRadius() / 2))
                {
                    if (actor.GetAmmoCount() > 0)
                    {
                        //attack
                        return new State_AttackPlayer(actor, target);
                    }
                    //chase
                    return new State_FollowPlayer(actor, target);
                }
                if (dist < (actor.GetAttackRadius() / 2) && dist >= (actor.GetAttackRadius() / 3))
                {
                    if (actor.GetHealth() >= target.GetHealth())
                    {
                        if (actor.GetAmmoCount() > 0)
                        {
                            //attack
                            return new State_AttackPlayer(actor, target);
                        }
                        //chase
                        return new State_FollowPlayer(actor, target);
                    }
                    //run away
                    return new State_Idle(actor);
                }

                if (dist < (actor.GetAttackRadius() / 3))
                {
                    if (actor.GetAmmoCount() > 0)
                    {
                        //attack
                        return new State_AttackPlayer(actor, target);
                    }
                    //run away
                    return new State_Idle(actor);
                }
            }
            //idle
            return new State_Idle(actor);
        }
    }
}



//public IState CommenceIfStatements(int dist, int actorHealth, int targetHealth)
//{
//    if (dist < actor.GetSearchRadius() || dist < actor.GetAttackRadius())
//    {
//        if (actor.GetHealth() > target.GetHealth() && dist >= (actor.GetAttackRadius() / 2))
//        {
//            if (actor.GetAmmoCount() > 0)
//            {
//                //attack
//                return null;

//            }
//            else
//            {
//                //chase
//                return null;
//            }
//        }
//        else
//        {
//            if (dist < (actor.GetAttackRadius() / 2) && dist >= (actor.GetAttackRadius() / 3))
//            {
//                if (actor.GetHealth() >= target.GetHealth())
//                {
//                    if (actor.GetAmmoCount() > 0)
//                    {
//                        //attack
//                        return null;
//                    }
//                    else
//                    {
//                        //chase
//                        return null;
//                    }
//                }
//                else if (actor.GetHealth() < target.GetHealth())
//                {
//                    //run away
//                    return null;
//                }
//            }
//            else
//            {
//                if (dist < (actor.GetAttackRadius() / 3))
//                {
//                    if (actor.GetAmmoCount() > 0)
//                    {
//                        //attack
//                        return null;
//                    }
//                    else
//                    {
//                        //run away
//                        return null;
//                    }
//                }
//            }
//        }
//    }
//    else
//    {
//        //idle
//        return null;
//    }
//}
