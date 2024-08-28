using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Gameplay
{
    public class CardUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public TMP_Text cardNameText;
        public Image cardSpriteImage;
        public GameObject cardBack;
        public Image cardBackground;

        public TMP_Text manaCostText;
        public TMP_Text healthText;
        public TMP_Text attackText;

        public GameObject combatStats;

        private Canvas _canvas;
        private Color _defaultBackgroundColor;

        private float _defaultScale;

        private Card _card;

        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
            _card = GetComponent<Card>();

            _defaultBackgroundColor = cardBackground.color;
            _defaultScale = transform.localScale.x;
        }

        private void Start()
        {
            //UpdateUI();
        }

       /* private void OnEnable()
        {
            _card.HighlightChanged += OnHighlightChanged;
            _card.SelectedChanged += OnSelectChanged;
        }

        private void OnDisable()
        {
            _card.HighlightChanged -= OnHighlightChanged;
            _card.SelectedChanged -= OnSelectChanged;
        }*/


        public void SetCardBack(bool value)
        {
            cardBack.SetActive(value);
            //cardNameText.enabled != value;
        }

       /* private void UpdateUI()
        {
            cardNameText.text = _card.CardData.cardName;
            cardSpriteImage.sprite = _card.CardData.cardSprite;
            manaCostText.text = $"{_card.CardData.manaCost}";

            if(_card.CardData.cardType == CardType.Spell)
            {
                combatStats.SetActive(false);
            }
            else
            {
                combatStats.SetActive(true);
                healthText.text = $"{_card.CardData.maxHealth}";
                attackText.text = $"{_card.CardData.attack}";
            }
        }

        public void SetHealth(int health, int maxHealth)
        {
            var color = Color.white;
            healthText.text = $"{health}";

            if(health < maxHealth)
            {
                color = Color.red;
            }
            healthText.color = color;
        }

        public void SetAttack(int attack)
        {
            var color = Color.white;
            attackText.text = $"{attack}";

            if(attack != _card.CardData.attack)
            {
                color = Color.green;
            }
            healthText.color = color;
        }

       */
        public void OnPointerEnter(PointerEventData eventData)
        {
            throw new System.NotImplementedException();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            throw new System.NotImplementedException();
        }

        internal void SetHealth(int health, int maxHealth)
        {
            throw new NotImplementedException();
        }
    }
}
