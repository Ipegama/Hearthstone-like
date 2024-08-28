using Gameplay;
using System.Collections;
using System.Collections.Generic;

namespace GameAnimations
{
    internal class ZoneChangedAnimation : GameAnimation
    {
        private Zone zone;
        private List<Card> cards;
        private Card card;
        private float v;

        public ZoneChangedAnimation(Zone zone, List<Card> cards, Card card, float v)
        {
            this.zone = zone;
            this.cards = cards;
            this.card = card;
            this.v = v;
        }

        public override IEnumerator Execute()
        {
            throw new System.NotImplementedException();
        }

        public override void ExecuteWithoutAnimation()
        {
            throw new System.NotImplementedException();
        }
    }
}