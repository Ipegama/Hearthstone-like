using Data;
using DG.Tweening;
using Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Gameplay
{
    public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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
            transform.DOLocalMove(targetPosition, 0.4f);
        }

        public void Highlight()
        {
            if (_status != CardStatus.InHand) return;

            transform.DOScale(Vector3.one * 4f, 0.4f);
            transform.DOLocalMoveZ(4f, 0.4f);
        }

        public void Unhighlight()
        {
            if (_status != CardStatus.InHand) return;

            transform.DOScale(Vector3.one * 1f, 0.4f);
            transform.DOLocalMoveZ(0f, 0.4f);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {

            _owner.Highlight(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {

            _owner.Unhighlight(this);
        }

       
    }
    public enum CardStatus
    {
        InGame,InHand,InDeck,InGraveyard,Removed
    }
}
