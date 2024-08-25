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
    public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public TMP_Text cardNameText;
        public TMP_Text cardManaText;
        public TMP_Text cardAttackText;
        public TMP_Text cardHealthText;

        public Image cardBackground;

        public Image cardImage;
        public GameObject cardFront;
        public GameObject cardBack;

        private CardData _cardData;
        private Player _owner;
        private CardStatus _status;
        private ICardHandler _cardHandler;

        private Canvas _canvas;
        private Color _defaultBackgroundColor;
        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
            _defaultBackgroundColor = cardBackground.color;
        }
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
            cardFront.SetActive(_status == CardStatus.InHand || _status == CardStatus.InGame);

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
            transform.DOLocalMoveZ(2.5f, 0.4f);
            _canvas.sortingOrder = 1;
        }

        public void Unhighlight()
        {
            if (_status != CardStatus.InHand) return;

            transform.DOScale(Vector3.one * 1f, 0.4f);
            transform.DOLocalMoveZ(0f, 0.4f);
            _canvas.sortingOrder = 0;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {

            _owner.Highlight(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {

            _owner.Unhighlight(this);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_status == CardStatus.InHand)
            {
                _owner.Select(this);
            }
        }

        public void Unselect()
        {
            cardBackground.color = _defaultBackgroundColor;
        }

        public void Select()
        {
            cardBackground.color = Color.yellow;
        }
    }
    public enum CardStatus
    {
        InGame,InHand,InDeck,InGraveyard,Removed
    }
}
