using DG.Tweening;
using Gameplay;
using System.Collections;
using System.Collections.Generic;
using TriggerSystem;
using UnityEngine;

namespace GameAnimations
{
    public class DrawCardAnimation : GameAnimation
    {
        private Player _player;
        private Card _card;

        public DrawCardAnimation(Player player, Card card)
        {
            _player = player;
            _card = card;
        }

        public override IEnumerator Execute()
        {
          /*  if (_player != GameManager.Instance.players[0])           
            {
                ExecuteWithoutAnimation();
                yield break;
            }
            _player.deck.RemoveCard(_card);

            var tf = _card.transform;
            var deckTransform = _player.deck.GetTransform();
            tf.SetParent(null);
            tf.position = deckTransform.position;
            tf.rotation = deckTransform.rotation;

            var handTransform = _player.hand.GetTransform();
            var halfwayPosition = Vector3.Lerp(deckTransform.position, handTransform.position, 0.5f);

            yield return tf.DOMove(halfwayPosition, 0.4f).SetEase(Ease.Linear).WaitForCompletion();

            yield return new WaitForSeconds(1f);

            yield return tf.DOMove(handTransform.position, 0.4f).SetEase(Ease.Linear).WaitForCompletion();

            tf.SetParent(_player.hand.GetTransform(), true);

            _player.hand.AddCard(_card);

            _player.hand.UpdateCardsPosition(_player.hand.Cards, true);
          */
            yield break;
        }

        public override void ExecuteWithoutAnimation()
        {
            _player.deck.RemoveCard(_card);

            var tf = _card.transform;
            tf.SetParent(_player.hand.GetTransform(), false);

            _player.hand.AddCard(_card);

            _player.hand.UpdateCardsPosition(_player.hand.Cards, false);
        }
    }
}