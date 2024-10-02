using DG.Tweening;
using Gameplay.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    public class CardUI : MonoBehaviour
    {
        public TMP_Text cardNameText;
        public Image cardSpriteImage;
        public GameObject cardBack;
        public Image cardBackground;

        public TMP_Text manaCostText;
        public TMP_Text healthText;
        public TMP_Text attackText;

        public GameObject combatStats;
        public GameObject triggerObject;
        public GameObject textsObject;
        public GameObject manaObject;

        public Transform animatedObject;

        public TMP_Text description;

        public GameObject freezeObject;

        private Canvas _canvas;
        private Color _defaultBackgroundColor;

        private float _defaultScale;

        private Card _card;

        private Tween _scaleTween;
        private Tween _punchScaleTween;
        private bool _selected;

        private void Awake()
        {
            _canvas = GetComponent<Canvas>();

            _defaultBackgroundColor = cardBackground.color;
            _defaultScale = animatedObject.localScale.x;
        }

        private void Start()
        {
            UpdateUI();
        }

        public void SetCard(Card card)
        {
            _card = card;
        }

        public void UpdateZoneUI(Zone zone)
        {
            ResetDefaultScale();
            SetCardBack(zone.IsDeck());
            SetBoardStatus(zone.IsBoard());
        }
        private void SetCardBack(bool value)
        {
            cardBack.SetActive(value);
            cardNameText.enabled = !value;
        }
        private void SetBoardStatus(bool value)
        {
            textsObject.SetActive(!value);
            manaObject.SetActive(!value);
        }
        private void UpdateUI()
        {
            cardNameText.text = _card.CardData.cardName;
            description.text = _card.CardData.GetDescription();
            cardSpriteImage.sprite = _card.CardData.cardSprite;
            manaCostText.text = $"{_card.GetCost()}";

            if(_card.CardData is CreatureData creatureData)
            {
                combatStats.SetActive(true);
                healthText.text = $"{creatureData.maxHealth}";
                attackText.text = $"{creatureData.attack}";
                triggerObject.SetActive(creatureData.triggers.Length > 0);
            }
            else
            {
                combatStats.SetActive(false);
            }
        }

        public void SetHealth(int health,int maxHealth)
        {
            if (_card.CardData is CreatureData creatureData)
            {
                var color = Color.white;
                healthText.text = $"{health}";

                if(maxHealth == creatureData.maxHealth)
                {
                    color = Color.white;
                }
                else if(maxHealth > creatureData.maxHealth)
                {
                    color = Color.green;
                }

                if (health < maxHealth)
                {
                    color = Color.red;
                }
                healthText.color = color;

                healthText.transform.DOComplete();
                healthText.transform.DOPunchScale(Vector3.one * 2f, 0.2f, 5);
            }
        }

        public void SetAttack(int attack)
        {
            if(_card.CardData is CreatureData creatureData)
            {
                var color = Color.white;
                attackText.text = $"{attack}";
                if(attack!= creatureData.attack)
                {
                    color = Color.green;
                }
                attackText.color = color;

                attackText.transform.DOComplete();
                attackText.transform.DOPunchScale(Vector3.one * 2f,0.2f,5);
            }
        }

        public void SetManaCost(int amount)
        {
            manaCostText.text = $"{amount}";

            manaCostText.transform.DOComplete();
            manaCostText.transform.DOPunchScale(Vector3.one * 2f, 0.2f, 5);
        }

        public void Highlight(bool value)
        {
            if (_card.IsInHand())
            {
                if(value)
                {
                    _scaleTween?.Kill();
                    _scaleTween = animatedObject.DOScale(_defaultScale * 2f, 0.4f);
                    animatedObject.DOLocalMoveY(120f,0.4f);
                    _canvas.sortingOrder = 1;
                }
                else
                {
                    _scaleTween?.Kill();
                    _scaleTween = animatedObject.DOScale(_defaultScale, 0.4f);
                    animatedObject.DOLocalMoveY(0f, 0.4f);
                    _canvas.sortingOrder = 0;
                }
            }

            else
            {
                ResetDefaultScale();
            }

            if (!_selected)
            {
                if (value)
                {
                    cardBackground.color = Color.blue;

                }
                else
                {
                    cardBackground.color = _defaultBackgroundColor;
                }
            }
        }

        public void Select(bool value)
        {
            _selected = value;
            if (value)
            {
                cardBackground.color = Color.yellow;
            }
            else
            {
                cardBackground.color = _defaultBackgroundColor; 
            }
        }

        public void SetFreeze(bool value)=> freezeObject.SetActive(value);

        private void ResetDefaultScale()
        {
            _scaleTween?.Kill();
            animatedObject.localScale = Vector3.one * _defaultScale;
            _canvas.sortingOrder = 0;
        }

        public void Trigger(float delay, float force)
        {
            var easeIn = Ease.OutExpo;
            var easeOut = Ease.InExpo;

            triggerObject.transform.DOComplete();
            triggerObject.transform.DOScale(force, delay / 2f).SetEase(easeIn)
                .OnComplete(()=>triggerObject.transform.DOScale(1f,delay/2f).SetEase(easeOut));
        }

        public void AnimateDamage(Vector3 scale, float duration)
        {
            ResetDefaultScale();
            _punchScaleTween?.Kill();
            _punchScaleTween = transform.DOPunchScale(scale, duration);
        }
    }
}
