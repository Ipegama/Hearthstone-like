using UnityEngine;
using Gameplay;
using Gameplay.Interfaces;
using TriggerSystem;

public class HeroPowerHitbox : MonoBehaviour, IHighlightable 
{
    public Player player;

    private void Awake()
    {
        if (player.heroPower == null)
        {
            player.heroPower = GetComponentInChildren<HeroPower>();
            player.heroPower.owner = player;
        }
    }

    public bool CanBeHighlighted(Player controllerPlayer, Card selectedCard)
    {
        return player == controllerPlayer && player.heroPower.CanBeUsed();
    }
    public void Highlight(bool value)=> player.heroPower.Highlight(value);
    public Card GetCard() => null;
}
