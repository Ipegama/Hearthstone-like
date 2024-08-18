using Data;
using Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class Card : MonoBehaviour
    {
        private CardData _cardData;
        private Player _owner;
        private CardStatus _status;
        private ICardHandler _cardHandler;
        public void SetData(CardData data)
        {
            _cardData = data;
        }

        public void SetOwner(Player owner)
        {
            _owner = owner;
            transform.SetParent(owner.transform);
        }

        public void SetStatus(CardStatus status, ICardHandler cardHandler)
        {

            _cardHandler = cardHandler;
           _status = status;
            transform.SetParent(_cardHandler.GetTransform());

            UpdatePosition();
        }

        private void UpdatePosition()
        {
            transform.localPosition = _cardHandler.GetPosition();
        }
    }
    public enum CardStatus
    {
        InGame,InHand,InDeck,InGraveyard,Removed
    }
}
