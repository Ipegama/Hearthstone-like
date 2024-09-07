using Gameplay;
using UnityEngine;

namespace GameAnimations
{
    public class CardCreatedAnimation : GameAnimation
    {
        private Card _card;
        public CardCreatedAnimation(Card card)
        {
            _card = card;
            card.UI.Hide();
        }
    }
}
