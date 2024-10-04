﻿using System.Collections;
using Gameplay;
using Gameplay.Interfaces;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Player controlledPlayer;

    private Coroutine _coroutine;

    private IHighlightable _highlightedEntity;
    private Card _selectedCard;
    private HeroPower _selectedHeroPower;

    private void Update()
    {
        UpdateHighlightedEntity();
        UpdateControls();
    }

    public void UpdateControls()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartTargetSelection();
        }

        if (_selectedCard != null || _selectedHeroPower != null)
        {
            UpdateArc();
        }
    }

    private void UpdateHighlightedEntity()
    {
        var entity = SelectionManager.GetObjectAtCursor<IHighlightable>();
        if (entity != null)
        {
            if (entity.CanBeHighlighted(controlledPlayer, _selectedCard))
            {
                Highlight(entity);
            }
        }
        else
        {
            Highlight(null);
        }
    }

    private void StopTargetSelection()
    {
        SelectCard(null);
        SelectHeroPower(null);
        ArcManager.Instance.ShowArc(false);
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }

    private void StartTargetSelection()
    {
        if (_coroutine != null) return;

        var card = SelectionManager.GetObjectAtCursor<Card>();
        if (card != null)
        {
            if (card.CanBeSelectedBy(controlledPlayer))
            {
                _coroutine = StartCoroutine(TargetSelection(card));
                return;
            }
        }

        var heroPowerHitbox = SelectionManager.GetObjectAtCursor<HeroPowerHitbox>();
        if (heroPowerHitbox != null && heroPowerHitbox.player == controlledPlayer)
        {
            var heroPower = controlledPlayer.heroPower;
            if (heroPower.CanBeUsed())
            {
                _coroutine = StartCoroutine(TargetSelection(heroPower));
                return;
            }
        }
    }

    private IEnumerator TargetSelection(Card card)
    {
        SelectCard(card);
        var filter = card.GetTargetFilter();
        var hasTarget = filter.HasTarget();

        ArcManager.Instance.ShowArc(true);
        ArcManager.Instance.SetStartPoint(card.transform.position);

        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (hasTarget)
                {
                    var target = SelectionManager.GetObjectAtCursor<ITargetable>();

                    if (target != null && target.CanBeTargeted() && filter.Match(card, target))
                    {
                        controlledPlayer.DoAction(card, target);
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

            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
            {
                break;
            }

            yield return null;
        }
        StopTargetSelection();
    }

    private IEnumerator TargetSelection(HeroPower heroPower)
    {
        SelectHeroPower(heroPower);
        var filter = heroPower.Data.targetFilter;
        var hasTarget = filter.HasTarget();

        ArcManager.Instance.ShowArc(true);
        ArcManager.Instance.SetStartPoint(heroPower.transform.position);

        yield return new WaitUntil(() => !Input.GetMouseButton(0));

        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (hasTarget)
                {
                    var target = SelectionManager.GetObjectAtCursor<ITargetable>();

                    if (target != null && target.CanBeTargeted() && filter.Match(null, target))
                    {
                        controlledPlayer.DoAction(heroPower, target);
                        break;
                    }
                }
                else
                {
                    controlledPlayer.DoAction(heroPower, null);
                    break;
                }
            }

            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
            {
                break;
            }

            yield return null;
        }
        StopTargetSelection();
    }

    private void UpdateArc()
    {
        if ((_selectedCard != null || _selectedHeroPower != null) && ArcManager.Instance.IsArcVisible())
        {
            Vector3 startPos = _selectedCard != null ? _selectedCard.transform.position : _selectedHeroPower.transform.position;
            Vector3 endPos = GetMouseWorldPosition();

            ArcManager.Instance.UpdateArcPositions(startPos, endPos);
        }
    }

    private void Highlight(IHighlightable entity)
    {
        if (_highlightedEntity == entity) return;

        _highlightedEntity?.Highlight(false);
        _highlightedEntity = entity;
        _highlightedEntity?.Highlight(true);
    }

    private void SelectCard(Card card)
    {
        if (card == _selectedCard) return;

        if (_selectedCard) _selectedCard.UI.Select(false);
        _selectedCard = card;
        if (_selectedCard) _selectedCard.UI.Select(true);

        if (_highlightedEntity != null && _highlightedEntity.GetCard() != _selectedCard)
        {
            _highlightedEntity.Highlight(true);
        }
    }

    private void SelectHeroPower(HeroPower heroPower)
    {
        if (heroPower == _selectedHeroPower) return;

        _selectedHeroPower?.Highlight(false);
        _selectedHeroPower = heroPower;
        _selectedHeroPower?.Highlight(true);

        if (_highlightedEntity != null)
        {
            _highlightedEntity.Highlight(true);
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        if (groundPlane.Raycast(ray, out float distance))
        {
            Vector3 worldPosition = ray.GetPoint(distance);
            return worldPosition;
        }

        return Vector3.zero;
    }
}
