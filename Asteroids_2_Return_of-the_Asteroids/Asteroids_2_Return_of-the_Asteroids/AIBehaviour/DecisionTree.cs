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
        PlayerShip playerShip;
        public DecisionTree(Pirate actor, PlayerShip playerShip)
        {
            this.actor = actor;
            this.playerShip = playerShip;

        }

        public IState CommenceIfStatements()
        {
            actor.GetHealth();
            playerShip.GetHealth();
            List<WeaponBase> ammoForEachWeaponSystem = actor.GetWeapons();

            var dist = Vector2.Distance(actor.Pos, playerShip.Pos);

            if (dist < actor.GetSearchRadius() || dist < actor.GetAttackRadius())
            {
                if (actor.GetHealth() > playerShip.GetHealth())
                {
                    foreach (WeaponBase weapon in ammoForEachWeaponSystem)
                    {
                        if (weapon.canShoot())
                        {
                            //attack
                            return new State_AttackPlayer(actor, playerShip);
                        }
                    }
                    //chase
                    return new State_FollowPlayer(actor, playerShip);
                }
                else
                {
                    if (dist <= (actor.GetAttackRadius() * 0.66f) && dist > 0)
                    {
                        if (actor.GetHealth() >= playerShip.GetHealth())
                        {
                            foreach (WeaponBase weapon in ammoForEachWeaponSystem)
                            {
                                if (weapon.canShoot())
                                {
                                    //attack
                                    return new State_AttackPlayer(actor, playerShip);
                                }
                            }
                            //chase                            
                            return new State_FollowPlayer(actor, playerShip);
                        }
                        else
                        {
                            foreach (WeaponBase weapon in ammoForEachWeaponSystem)
                            {
                                if (weapon.canShoot())
                                {
                                    //attack
                                    return new State_AttackPlayer(actor, playerShip);
                                }
                            }
                            //run away                            
                            return new State_EscapePlayer(actor, playerShip);
                        }
                    }
                    //run away
                    else
                        return new State_EscapePlayer(actor, playerShip);
                }
            }
            //idle
            else
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
