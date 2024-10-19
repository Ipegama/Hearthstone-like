using UnityEngine;
using Gameplay;
using Gameplay.Interfaces;
using TriggerSystem;

public class HeroPowerHitbox : MonoBehaviour, IHighlightable, IPlayable
{
    public Player player;

    private HeroPower _heroPower;

    private void Awake()
    {
        if (player.heroPower == null)
        {
            player.heroPower = GetComponentInChildren<HeroPower>();
            player.heroPower.owner = player;
        }
        _heroPower = player.heroPower;
    }

    public bool CanBeHighlighted(Player controllerPlayer, Card selectedCard) =>
        _heroPower.CanBeHighlighted(controllerPlayer, selectedCard);

    public void Highlight(bool value) => _heroPower.Highlight(value);

    public Card GetCard() => null;

    public void Select(bool value) => _heroPower.Select(value);

    public TargetFilter GetTargetFilter() => _heroPower.GetTargetFilter();

    public Transform GetTransform() => _heroPower.GetTransform();

    public bool CanBeUsed(Player player) => _heroPower.CanBeUsed(player);

    public void ExecuteAction(Player player, ITargetable target) =>
        _heroPower.ExecuteAction(player, target);
}
