using Gameplay;
using System.Collections;
using UnityEngine;

namespace GameAnimations
{
    public class CardCreatedAnimation : GameAnimation
    {
        private Card _card;
        public CardCreatedAnimation(Card card)
        {
            _card = card;
            _card.gameObject.SetActive(false);
        }

        public override IEnumerator Execute()
        {
            _card.gameObject.SetActive(true);
            yield break;
        }

        public override void ExecuteWithoutAnimation()
        {
            _card.gameObject.SetActive(true);
        }
    }
}
