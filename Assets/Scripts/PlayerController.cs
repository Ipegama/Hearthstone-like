using Gameplay.Interfaces;
using Gameplay;
using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public Player controlledPlayer;

    private Coroutine _coroutine;
    private IHighlightable _highlightedEntity;
    private IPlayable _selectedPlayable;

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

        if (_selectedPlayable != null)
        {
            UpdateArc();
        }
    }

    private void UpdateHighlightedEntity()
    {
        var entity = SelectionManager.GetObjectAtCursor<IHighlightable>();
        if (entity != null)
        {
            if (entity.CanBeHighlighted(controlledPlayer, _selectedPlayable as Card))
            {
                Highlight(entity);
            }
        }
        else
        {
            Highlight(null);
        }
    }

    private void StartTargetSelection()
    {
        if (_coroutine != null) return;

        var playable = SelectionManager.GetObjectAtCursor<IPlayable>();
        if (playable != null && playable.CanBeUsed(controlledPlayer))
        {
            _coroutine = StartCoroutine(TargetSelection(playable));
            return;
        }
    }

    private void StopTargetSelection(IPlayable playable)
    {
        SelectPlayable(null);
        ArcManager.Instance.ShowArc(false);
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }

    private IEnumerator TargetSelection(IPlayable playable)
    {
        SelectPlayable(playable);
        var filter = playable.GetTargetFilter();
        var hasTarget = filter != null && filter.HasTarget();

        ArcManager.Instance.ShowArc(true);
        ArcManager.Instance.SetStartPoint(playable.GetTransform().position);

        yield return new WaitUntil(() => !Input.GetMouseButton(0));

        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (hasTarget)
                {
                    var target = SelectionManager.GetObjectAtCursor<ITargetable>();

                    if (target != null && target.CanBeTargeted() && filter.Match(playable as ITargetable, target))
                    {
                        playable.ExecuteAction(controlledPlayer, target);
                        break;
                    }
                }
                else
                {
                    playable.ExecuteAction(controlledPlayer, null);
                    break;
                }
            }

            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
            {
                break;
            }

            yield return null;
        }
        StopTargetSelection(playable);
    }

    private void UpdateArc()
    {
        if (_selectedPlayable != null && ArcManager.Instance.IsArcVisible())
        {
            Vector3 startPos = _selectedPlayable.GetTransform().position;
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

    private void SelectPlayable(IPlayable playable)
    {
        if (_selectedPlayable == playable) return;

        _selectedPlayable?.Select(false);
        _selectedPlayable = playable;
        _selectedPlayable?.Select(true);

        if (_highlightedEntity != null && _highlightedEntity != _selectedPlayable)
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
