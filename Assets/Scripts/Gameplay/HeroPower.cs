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

    [HideInInspector] public HeroPowerUI UI;

    public Image heroPowerBackground;
    private Color _defaultBackgroundColor;

    private void Awake()
    {
        UI = GetComponent<HeroPowerUI>();
        
    }

    private void Start()
    {
        UI.SetHeroPower(this);

        _defaultBackgroundColor = heroPowerBackground.color;
    }
    public virtual void SetData(HeroPowerData data)=> Data = data;
    public void ExecuteOnPlayAction(ITargetable target)
    {
        foreach (var action in Data.playActions)
        {
            action.Execute(new ActionContext
            {
                TargetEntity = target,
                TriggerEntity = owner,
            }); ;
        }
    }

    public bool CanBeUsed() => true;
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

    public bool CanBeHighlighted(Player controllerPlayer, Card selectedCard)=> owner == controllerPlayer && CanBeUsed();
    public Card GetCard() => null;
}
