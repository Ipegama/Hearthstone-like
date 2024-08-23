using Data;
using DG.Tweening;
using Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Gameplay
{
    public class Card : MonoBehaviour
    {
        public TMP_Text cardNameText;
        public TMP_Text cardManaText;
        public TMP_Text cardAttackText;
        public TMP_Text cardHealthText;

        public Image cardImage;
        public GameObject cardFront;
        public GameObject cardBack;

        private CardData _cardData;
        private Player _owner;
        private CardStatus _status;
        private ICardHandler _cardHandler;
        public void SetData(CardData data)
        {
            _cardData = data;
            
            cardNameText.text = data.cardName;
            cardManaText.text = data.manaCost.ToString();
            cardAttackText.text = data.attack.ToString();
            cardHealthText.text = data.maxHealth.ToString();    

            cardImage.sprite = data.cardSprite;
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

            cardBack.SetActive(_status == CardStatus.InDeck);
            cardFront.SetActive(_status == CardStatus.InHand);

            transform.SetParent(_cardHandler.GetTransform());

            UpdatePosition();
        }

        public void UpdatePosition()
        {
            var targetPosition = _cardHandler.GetPosition(this);
            transform.DOKill();
            transform.DOLocalMove(targetPosition,0.4f);
        }
    }
    public enum CardStatus
    {
        InGame,InHand,InDeck,InGraveyard,Removed
    }
}
