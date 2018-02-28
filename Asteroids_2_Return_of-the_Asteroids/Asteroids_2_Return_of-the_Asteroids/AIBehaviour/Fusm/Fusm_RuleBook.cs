using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids_2_Return_of_the_Asteroids
{
    class Fusm_RuleBook
    {
        FusmMachine fusm;
        Pirate actor;
        PlayerShip playerShip;
        //float urgencyHigh = 0.66f, urgencyMedium = 0.33f, urgencyLow = 0.0f;
        int escapeAtHealth = 1;
        //Fuzzy_StateAttack_Low stateAttackLow;
        //Fuzzy_StateAttack_Med stateAttackMed;
        //Fuzzy_StateAttack_High stateAttackHigh;
        //Fuzzy_StateChase_High stateChase;
        //Fuzzy_StateIdle stateidle;

        //List<IFusmState> states;
        //public enum LikelyhoodToAttack { low, medium, high }
        //LikelyhoodToAttack likelyhoodToAttack;
        //public enum LikelyhoodToChase { low, medium, high }
        //LikelyhoodToChase likelyhoodToChase;
        //public enum LikelyhoodToEscape { low, medium, high }
        //LikelyhoodToEscape likelyhoodToEscape;
        //public enum LikelyhoodToIdle { low, medium, high }
        //LikelyhoodToIdle likelyhoodToIdle;

        //float dist;


        public Fusm_RuleBook(FusmMachine fusm, Pirate actor, PlayerShip playerShip)
        {
            this.fusm = fusm;
            this.actor = actor;
            this.playerShip = playerShip;
        }

        public void States()
        {
            
            fusm.AddState(new Fuzzy_StateChase(actor, playerShip));          
            fusm.AddState(new Fuzzy_StateAttack(actor, playerShip));
            fusm.AddState(new Fuzzy_StateEscape(actor, playerShip));          
            //   fusm.AddState(new Fuzzy_StateIdle(actor, target));
            //RuleBook();
        }

        public void CheckRules(IFusmState state)
        {
            if (state is Fuzzy_StateAttack)
            {
                AttackStateFuzzyRules(state);
            }
            else if (state is Fuzzy_StateChase)
            {
                ChaseStateFuzzyRules(state);
            }
            else if (state is Fuzzy_StateEscape)
            {
                EscapeStateFuzzyRules(state);
            }
        }

        private void AttackStateFuzzyRules(IFusmState state)
        {
            List<WeaponBase> ammoForEachWeaponSystem = actor.GetWeapons();
            
            foreach (WeaponBase weapon in ammoForEachWeaponSystem)
            {
                if (weapon.canShoot())
                {
                    if (actor.GetHealth() >= playerShip.GetHealth() && actor.GetHealth() > escapeAtHealth)
                        state.SetCanActivate(true);
                }
            }

            //else if (urgency <= urgencyHigh && urgency > urgencyMedium && actor.GetHealth() > playerShip.GetHealth())
            //    state.IsActive(true);
            //else if (urgency <= urgencyMedium && urgency > urgencyLow && actor.GetHealth() > playerShip.GetHealth() && playerShip.GetHealth() == escapeAtHealth)
            //    state.IsActive(true);

        }

        private void ChaseStateFuzzyRules(IFusmState state)
        {
            if (/*actor.GetHealth() >= playerShip.GetHealth() &&*/ actor.GetHealth() > escapeAtHealth)
                state.SetCanActivate(true);
        }

        private void EscapeStateFuzzyRules(IFusmState state)
        {
            float tempDist = Vector2.Distance(actor.Pos, playerShip.Pos);
            if (tempDist < actor.GetSearchRadius() && actor.GetHealth() <= escapeAtHealth)
                state.SetCanActivate(true);
        }

        //private void RuleBook()
        //{
        //    dist = Vector2.Distance(actor.Pos, target.Pos);

        //    AttackRules();

        //   // likelyhoodToChase = ChaseRules();

        //   // likelyhoodToEscape = EscapeRules();

        //  //  likelyhoodToIdle = IdleRules();
        //}      

        //private void AttackRules()
        //{
        //    if (actor.GetHealth() > target.GetHealth() && dist <= actor.GetAttackRadius())
        //         fusm.AddState(new Fuzzy_StateAttack_High(actor, target));

        //    else if (actor.GetHealth() == target.GetHealth() &&)
        //        return likelyhoodToAttack = LikelyhoodToAttack.high;

        //    else if (actor.GetAmmoCount() == 2 && actor.GetHealth() == target.GetHealth())
        //        return likelyhoodToAttack = LikelyhoodToAttack.medium;

        //    else if (actor.GetAmmoCount() == 2 && actor.GetHealth() > target.GetHealth())
        //        return likelyhoodToAttack = LikelyhoodToAttack.medium;

        //    else if (actor.GetAmmoCount() == 1 && actor.GetHealth() > target.GetHealth())
        //        return likelyhoodToAttack = LikelyhoodToAttack.low;

        //    else if (actor.GetAmmoCount() == 3 && actor.GetHealth() == 1)
        //        return likelyhoodToAttack = LikelyhoodToAttack.low;

        //    else if (actor.GetAmmoCount() == 3 && actor.GetHealth() < target.GetHealth())
        //        return likelyhoodToAttack = LikelyhoodToAttack.medium;

        //    else if (actor.GetAmmoCount() == 2 && actor.GetHealth() < target.GetHealth())
        //        return likelyhoodToAttack = LikelyhoodToAttack.low;

        //    else if (actor.GetAmmoCount() == 1 && actor.GetHealth() < target.GetHealth())
        //        return likelyhoodToAttack = LikelyhoodToAttack.low;

        //    else
        //        return likelyhoodToAttack = LikelyhoodToAttack.low; // default
        //}

        //private LikelyhoodToChase ChaseRules()
        //{

        //    if (dist < actor.GetSearchRadius() && actor.GetAmmoCount() == 3)
        //        return likelyhoodToChase = LikelyhoodToChase.high;

        //    else if (dist < actor.GetSearchRadius() && actor.GetAmmoCount() == 2)
        //        return likelyhoodToChase = LikelyhoodToChase.medium;

        //    else if (dist < actor.GetSearchRadius() && actor.GetAmmoCount() == 1)
        //        return likelyhoodToChase = LikelyhoodToChase.low;

        //    else if ((dist / 2) < actor.GetSearchRadius() && actor.GetAmmoCount() == 3)
        //        return likelyhoodToChase = LikelyhoodToChase.high;

        //    else if ((dist / 2) < actor.GetSearchRadius() && actor.GetAmmoCount() == 2)
        //        return likelyhoodToChase = LikelyhoodToChase.medium;

        //    else if ((dist / 2) < actor.GetSearchRadius() && actor.GetAmmoCount() == 1)
        //        return likelyhoodToChase = LikelyhoodToChase.low;

        //    else if ((dist / 4) < actor.GetSearchRadius() && actor.GetAmmoCount() == 3)
        //        return likelyhoodToChase = LikelyhoodToChase.high;

        //    else if ((dist / 4) < actor.GetSearchRadius() && actor.GetAmmoCount() == 2)
        //        return likelyhoodToChase = LikelyhoodToChase.medium;

        //    else if ((dist / 4) < actor.GetSearchRadius() && actor.GetAmmoCount() == 1)
        //        return likelyhoodToChase = LikelyhoodToChase.low;

        //    else
        //        return likelyhoodToChase = LikelyhoodToChase.low; // default
        //}

        //private LikelyhoodToEscape EscapeRules()
        //{
        //    if (actor.GetHealth() == 3 && target.GetHealth() == 3/* && actor.GetAmmoCount() == 3*/)
        //        return likelyhoodToEscape = LikelyhoodToEscape.low;

        //    else if (actor.GetHealth() == 3 && target.GetHealth() == 2 /*&& actor.GetAmmoCount() == 3*/)
        //        return likelyhoodToEscape = LikelyhoodToEscape.low;

        //    else if (actor.GetHealth() == 3 && target.GetHealth() == 1 /*&& actor.GetAmmoCount() == 3*/)
        //        return likelyhoodToEscape = LikelyhoodToEscape.low;

        //    else if (actor.GetHealth() == 3 && target.GetHealth() == 3/* && actor.GetAmmoCount() == 2*/)
        //        return likelyhoodToEscape = LikelyhoodToEscape.medium;

        //    else if (actor.GetHealth() == 3 && target.GetHealth() == 3 /*&& actor.GetAmmoCount() == 1*/)
        //        return likelyhoodToEscape = LikelyhoodToEscape.high;

        //    else if (actor.GetHealth() == 3 && target.GetHealth() == 3 /*&& actor.GetAmmoCount() == 0*/)
        //        return likelyhoodToEscape = LikelyhoodToEscape.high;

        //    else if (actor.GetHealth() == 3 && target.GetHealth() == 2 /*&& actor.GetAmmoCount() == 2*/)
        //        return likelyhoodToEscape = LikelyhoodToEscape.medium;

        //    else if (actor.GetHealth() == 3 && target.GetHealth() == 2 /*&& actor.GetAmmoCount() == 1*/)
        //        return likelyhoodToEscape = LikelyhoodToEscape.high;

        //    else if (actor.GetHealth() == 3 && target.GetHealth() == 1 /*&& actor.GetAmmoCount() == 1*/)
        //        return likelyhoodToEscape = LikelyhoodToEscape.high;

        //    else if (actor.GetHealth() == 2 && target.GetHealth() == 3 /*&& actor.GetAmmoCount() == 3*/)
        //        return likelyhoodToEscape = LikelyhoodToEscape.low;

        //    else if (actor.GetHealth() == 2 && target.GetHealth() == 2/* && actor.GetAmmoCount() == 2*/)
        //        return likelyhoodToEscape = LikelyhoodToEscape.low;

        //    else if (actor.GetHealth() == 2 && target.GetHealth() == 1/* && actor.GetAmmoCount() == 1*/)
        //        return likelyhoodToEscape = LikelyhoodToEscape.high;

        //    else if (actor.GetHealth() == 2 && target.GetHealth() == 2 /*&& actor.GetAmmoCount() == 3*/)
        //        return likelyhoodToEscape = LikelyhoodToEscape.low;

        //    else if (actor.GetHealth() == 3 && target.GetHealth() == 3 /*&& actor.GetAmmoCount() == 1*/)
        //        return likelyhoodToEscape = LikelyhoodToEscape.high;

        //    else if (actor.GetHealth() == 1 && target.GetHealth() == 3 /*&& actor.GetAmmoCount() == 3*/)
        //        return likelyhoodToEscape = LikelyhoodToEscape.low;

        //    else if (actor.GetHealth() == 1 && target.GetHealth() == 3 /*&& actor.GetAmmoCount() == 2*/)
        //        return likelyhoodToEscape = LikelyhoodToEscape.low;

        //    else if (actor.GetHealth() == 1 && target.GetHealth() == 3/* && actor.GetAmmoCount() == 1*/)
        //        return likelyhoodToEscape = LikelyhoodToEscape.low;

        //    else if (actor.GetHealth() == 1 && target.GetHealth() == 2/* && actor.GetAmmoCount() == 3*/)
        //        return likelyhoodToEscape = LikelyhoodToEscape.low;

        //    else if (actor.GetHealth() == 1 && target.GetHealth() == 1 /*&& actor.GetAmmoCount() == 3*/)
        //        return likelyhoodToEscape = LikelyhoodToEscape.medium;

        //    else if (actor.GetHealth() == 1 && target.GetHealth() == 2 /*&& actor.GetAmmoCount() == 2*/)
        //        return likelyhoodToEscape = LikelyhoodToEscape.medium;

        //    else if (actor.GetHealth() == 1 && target.GetHealth() == 1 /*&& actor.GetAmmoCount() == 1*/)
        //        return likelyhoodToEscape = LikelyhoodToEscape.high;
        //    else
        //        return likelyhoodToEscape = LikelyhoodToEscape.low;
        //}
        //private LikelyhoodToIdle IdleRules()
        //{
        //    if (dist > actor.GetSearchRadius() && target.GetHealth() > actor.GetHealth())
        //        return likelyhoodToIdle = LikelyhoodToIdle.high;
        //    else if (dist > actor.GetSearchRadius() && target.GetHealth() <= actor.GetHealth())
        //        return likelyhoodToIdle = LikelyhoodToIdle.high;
        //    else if (dist <= actor.GetSearchRadius() && target.GetHealth() < actor.GetHealth())
        //        return likelyhoodToIdle = LikelyhoodToIdle.low;
        //    else if (dist <= actor.GetSearchRadius() && target.GetHealth() == actor.GetHealth())
        //        return likelyhoodToIdle = LikelyhoodToIdle.medium;
        //    else
        //        return likelyhoodToIdle = LikelyhoodToIdle.high; // default
        //}

        //public LikelyhoodToAttack GetLikelyhoodToAttack()
        //{
        //    return likelyhoodToAttack;
        //}

        //public LikelyhoodToChase GetLikelyhoodToChase()
        //{
        //    return likelyhoodToChase;
        //}

        //public LikelyhoodToEscape GetLikelyhoodToEscape()
        //{
        //    return likelyhoodToEscape;
        //}

        //public LikelyhoodToIdle GetLikelyhoodToIdle()
        //{
        //    return likelyhoodToIdle;
        //}

    }
}
