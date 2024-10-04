using Gameplay;
using Gameplay.Data;
using Gameplay.Interfaces;
using TriggerSystem;
using UnityEngine;
using UnityEngine.UI;

public class HeroPower : MonoBehaviour, IHighlightable
{
    public HeroPowerData Data { get; protected set; }

    public Player owner;

    public Image heroPowerBackground;
    private Color _defaultBackgroundColor;

    private void Awake()
    {
        _defaultBackgroundColor = heroPowerBackground.color;
    }

    public virtual void SetData(HeroPowerData data)
    {
        Data = data;
    }

    public void ExecuteOnPlayAction(ITargetable target)
    {
        if (target == null)
        {
            if (Data.targetFilter.HasTarget())
            {
                throw new System.Exception("Target needed");
            }
        }

        foreach (var action in Data.playActions)
        {
            action.Execute(new ActionContext
            {
                TargetEntity = target,
                TriggerEntity = owner
            });
        }
    }

    public bool CanBeUsed()
    {
        return true; 
    }

    public void Highlight(bool value)
    {
        if (value)
        {
            heroPowerBackground.color = Color.yellow;
        }
        else
        {
            heroPowerBackground.color = _defaultBackgroundColor;
        }
    }

    public bool CanBeHighlighted(Player controllerPlayer, Card selectedCard)
    {
        return owner == controllerPlayer && CanBeUsed();
    }

    public Card GetCard() => null;
}
