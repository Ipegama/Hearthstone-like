using System.Collections;
using Gameplay;
using Gameplay.Interfaces;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Player controlledPlayer;
    
    private Coroutine _coroutine;
    
    private IHighlightable _highlightedEntity;
    private Card _selectedCard;

    private void Update()
    {
        UpdateHighlightedEntity();
    }

    public void UpdateControls()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartTargetSelection();
        }
    }
    private void UpdateHighlightedEntity()
    {
        var card = SelectionManager.GetObjectAtCursor<IHighlightable>();
        if(card != null)
        {
            if(card.CanBeTargeted(controlledPlayer,_selectedCard))
            {
                Highlight(card);
            }
        }
        else
        {
            Highlight(null);
        }
    }
    private void StopTargetSelection()
    {
        Select(null);
        if(_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }

    private void StartTargetSelection()
    {
        if (_coroutine != null) return;

        var card = SelectionManager.GetObjectAtCursor<Card>();
        if(card != null)
        {
            if (card.CanBeSelectedBy(controlledPlayer))
            {
                _coroutine = StartCoroutine(TargetSelection(card));
            }
        }
    }

    private IEnumerator TargetSelection(Card card)
    {
        Select(card);
        var filter = card.GetTargetFilter();
        var hasTarget = filter.HasTarget();

        while (true)
        {
            if(Input.GetMouseButtonDown(0))
            {
                if(hasTarget)
                {
                    var target = SelectionManager.GetObjectAtCursor<ITargetable>();

                    if(target != null && target.CanBeTargeted() && filter.Match(card, target))
                    {
                        controlledPlayer.DoAction(card,target);
                        break;
                    } 
                }
                else
                {
                    var zone = SelectionManager.GetObjectAtCursor<Zone>();
                    if (zone != null)
                    {
                        controlledPlayer.DoAction(card, zone);
                        break;
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1)) break;

            yield return null;
        }
        StopTargetSelection();
    }

    private void Highlight(IHighlightable entity)
    {
        if(_highlightedEntity == entity) return;

        _highlightedEntity?.Highlight(false);
        _highlightedEntity = entity;
        _highlightedEntity?.Highlight(true);
    }

    private void Select(Card card)
    {
        if (card == _selectedCard) return;

        if(_selectedCard) _selectedCard.UI.Select(false);
        _selectedCard = card;
        if (_selectedCard) _selectedCard.UI.Select(true);

        if(_highlightedEntity != null && _highlightedEntity.GetCard() != _selectedCard)
        {
            _highlightedEntity.Highlight(true);
        }
    }
}