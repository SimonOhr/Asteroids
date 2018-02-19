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
        PlayerShip target;
        Fuzzy_StateAttack_Low stateAttackLow;
        Fuzzy_StateAttack_Med stateAttackMed;
        Fuzzy_StateAttack_High stateAttackHigh;
        Fuzzy_StateChase_High stateChase;
        Fuzzy_StateIdle stateidle;

        List<IFusmState> states;
        public enum LikelyhoodToAttack { low, medium, high }
        LikelyhoodToAttack likelyhoodToAttack;
        public enum LikelyhoodToChase { low, medium, high }
        LikelyhoodToChase likelyhoodToChase;
        public enum LikelyhoodToEscape { low, medium, high }
        LikelyhoodToEscape likelyhoodToEscape;
        public enum LikelyhoodToIdle { low, medium, high }
        LikelyhoodToIdle likelyhoodToIdle;

        float dist;


        public Fusm_RuleBook(FusmMachine fusm, Pirate actor, PlayerShip target)
        {
            this.fusm = fusm;
            this.actor = actor;
            this.target = target;
        }

        public void States()
        {
            fusm.AddState(new Fuzzy_StateChase_Low(actor, target));
            fusm.AddState(new Fuzzy_StateChase_Med(actor, target));
            fusm.AddState(new Fuzzy_StateChase_High(actor, target));
            fusm.AddState(new Fuzzy_StateAttack_Low(actor, target));
            fusm.AddState(new Fuzzy_StateAttack_Med(actor, target));
            fusm.AddState(new Fuzzy_StateAttack_High(actor, target));
           // fusm.AddState(new Fuzzy_StateEscape(actor, target));
         //   fusm.AddState(new Fuzzy_StateIdle(actor, target));
            //RuleBook();
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
