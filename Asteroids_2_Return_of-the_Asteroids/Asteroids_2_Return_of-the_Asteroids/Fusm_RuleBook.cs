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
        Fuzzy_StateAttack stateAttack;
        Fuzzy_StateChase stateChase;

        List<IFusmState> states;
        public enum LikelyhoodToAttack { low, medium, high }
        LikelyhoodToAttack likelyhoodToAttack;
        public enum LikelyhoodToChase { low, medium, high }
        LikelyhoodToChase likelyhoodToChase;
        public enum LikelyhoodToEscape { low, medium, high }
        LikelyhoodToEscape likelyhoodToEscape;


        public Fusm_RuleBook(FusmMachine fusm, Pirate actor, PlayerShip target)
        {
            this.fusm = fusm;
            this.actor = actor;
            this.target = target;
        }

        public void States()
        {
            fusm.AddState(new Fuzzy_StateChase(actor, target));
            fusm.AddState(new Fuzzy_StateAttack(actor, target));
            fusm.AddState(new Fuzzy_StateEscape(actor, target));
            RuleBook();
        }

        private void RuleBook()
        {
            likelyhoodToAttack = AttackProbability();

            likelyhoodToChase = ChaseProbability();

            likelyhoodToEscape = EscapeProbability();
        }               

        private LikelyhoodToAttack AttackProbability()
        {
            if (actor.GetAmmoCount() == 3 && actor.GetHealth() == 3)
                return likelyhoodToAttack = LikelyhoodToAttack.high;

            else if (actor.GetAmmoCount() == 2 && actor.GetHealth() == 2)
                return likelyhoodToAttack = LikelyhoodToAttack.medium;

            else if (actor.GetAmmoCount() == 2 && actor.GetHealth() == 3)
                return likelyhoodToAttack = LikelyhoodToAttack.medium;

            else if (actor.GetAmmoCount() == 1 && actor.GetHealth() == 3)
                return likelyhoodToAttack = LikelyhoodToAttack.low;

            else if (actor.GetAmmoCount() == 3 && actor.GetHealth() == 1)
                return likelyhoodToAttack = LikelyhoodToAttack.low;

            else
                return likelyhoodToAttack = LikelyhoodToAttack.low; // default
        }

        private LikelyhoodToChase ChaseProbability()
        {
            float tempDist = Vector2.Distance(actor.Pos, target.Pos);

            if (tempDist < actor.GetSearchRadius() && actor.GetAmmoCount() == 3)
                return likelyhoodToChase = LikelyhoodToChase.high;

            else if (tempDist < actor.GetSearchRadius() && actor.GetAmmoCount() == 2)
                return likelyhoodToChase = LikelyhoodToChase.medium;

            else if (tempDist < actor.GetSearchRadius() && actor.GetAmmoCount() == 1)
                return likelyhoodToChase = LikelyhoodToChase.low;

            else if ((tempDist / 2) < actor.GetSearchRadius() && actor.GetAmmoCount() == 3)
                return likelyhoodToChase = LikelyhoodToChase.high;

            else if ((tempDist / 2) < actor.GetSearchRadius() && actor.GetAmmoCount() == 2)
                return likelyhoodToChase = LikelyhoodToChase.medium;

            else if ((tempDist / 2) < actor.GetSearchRadius() && actor.GetAmmoCount() == 1)
                return likelyhoodToChase = LikelyhoodToChase.low;

            else if ((tempDist / 4) < actor.GetSearchRadius() && actor.GetAmmoCount() == 3)
                return likelyhoodToChase = LikelyhoodToChase.high;

            else if ((tempDist / 4) < actor.GetSearchRadius() && actor.GetAmmoCount() == 2)
                return likelyhoodToChase = LikelyhoodToChase.medium;

            else if ((tempDist / 4) < actor.GetSearchRadius() && actor.GetAmmoCount() == 1)
                return likelyhoodToChase = LikelyhoodToChase.low;

            else
                return likelyhoodToChase = LikelyhoodToChase.low; // default
        }

        private LikelyhoodToEscape EscapeProbability()
        {
            if (actor.GetHealth() == 3 && target.GetHealth() == 3 && actor.GetAmmoCount() == 3)
                return likelyhoodToEscape = LikelyhoodToEscape.low;

            else if (actor.GetHealth() == 3 && target.GetHealth() == 2 && actor.GetAmmoCount() == 3)
                return likelyhoodToEscape = LikelyhoodToEscape.low;

            else if (actor.GetHealth() == 3 && target.GetHealth() == 1 && actor.GetAmmoCount() == 3)
                return likelyhoodToEscape = LikelyhoodToEscape.low;

            else if (actor.GetHealth() == 3 && target.GetHealth() == 3 && actor.GetAmmoCount() == 2)
                return likelyhoodToEscape = LikelyhoodToEscape.medium;

            else if (actor.GetHealth() == 3 && target.GetHealth() == 3 && actor.GetAmmoCount() == 1)
                return likelyhoodToEscape = LikelyhoodToEscape.high;

            else if (actor.GetHealth() == 3 && target.GetHealth() == 3 && actor.GetAmmoCount() == 0)
                return likelyhoodToEscape = LikelyhoodToEscape.high;

            else if (actor.GetHealth() == 3 && target.GetHealth() == 2 && actor.GetAmmoCount() == 2)
                return likelyhoodToEscape = LikelyhoodToEscape.medium;

            else if (actor.GetHealth() == 3 && target.GetHealth() == 2 && actor.GetAmmoCount() == 1)
                return likelyhoodToEscape = LikelyhoodToEscape.high;

            else if (actor.GetHealth() == 3 && target.GetHealth() == 1 && actor.GetAmmoCount() == 1)
                return likelyhoodToEscape = LikelyhoodToEscape.high;

            else if (actor.GetHealth() == 2 && target.GetHealth() == 3 && actor.GetAmmoCount() == 3)
                return likelyhoodToEscape = LikelyhoodToEscape.low;

            else if (actor.GetHealth() == 2 && target.GetHealth() == 2 && actor.GetAmmoCount() == 2)
                return likelyhoodToEscape = LikelyhoodToEscape.low;

            else if (actor.GetHealth() == 2 && target.GetHealth() == 1 && actor.GetAmmoCount() == 1)
                return likelyhoodToEscape = LikelyhoodToEscape.high;

            else if (actor.GetHealth() == 2 && target.GetHealth() == 2 && actor.GetAmmoCount() == 3)
                return likelyhoodToEscape = LikelyhoodToEscape.low;

            else if (actor.GetHealth() == 3 && target.GetHealth() == 3 && actor.GetAmmoCount() == 1)
                return likelyhoodToEscape = LikelyhoodToEscape.high;

            else if (actor.GetHealth() == 1 && target.GetHealth() == 3 && actor.GetAmmoCount() == 3)
                return likelyhoodToEscape = LikelyhoodToEscape.low;

            else if (actor.GetHealth() == 1 && target.GetHealth() == 3 && actor.GetAmmoCount() == 2)
                return likelyhoodToEscape = LikelyhoodToEscape.low;

            else if (actor.GetHealth() == 1 && target.GetHealth() == 3 && actor.GetAmmoCount() == 1)
                return likelyhoodToEscape = LikelyhoodToEscape.low;

            else if (actor.GetHealth() == 1 && target.GetHealth() == 2 && actor.GetAmmoCount() == 3)
                return likelyhoodToEscape = LikelyhoodToEscape.low;

            else if (actor.GetHealth() == 1 && target.GetHealth() == 1 && actor.GetAmmoCount() == 3)
                return likelyhoodToEscape = LikelyhoodToEscape.medium;

            else if (actor.GetHealth() == 1 && target.GetHealth() == 2 && actor.GetAmmoCount() == 2)
                return likelyhoodToEscape = LikelyhoodToEscape.medium;

            else if (actor.GetHealth() == 1 && target.GetHealth() == 1 && actor.GetAmmoCount() == 1)
                return likelyhoodToEscape = LikelyhoodToEscape.high;
            else
                return likelyhoodToEscape = LikelyhoodToEscape.low;
        }

        public LikelyhoodToAttack GetLikelyhoodToAttack()
        {
            return likelyhoodToAttack;
        }

        public LikelyhoodToChase GetLikelyhoodToChase()
        {
            return likelyhoodToChase;
        }

        public LikelyhoodToEscape GetLikelyhoodToEscape()
        {
            return likelyhoodToEscape;
        }

    }
}
