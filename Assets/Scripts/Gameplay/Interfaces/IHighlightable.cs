namespace Gameplay.Interfaces
{
    public interface IHighlightable
    {
        void Highlight(bool value);
        bool CanBeHighlighted(Player controllerPlayer, Card selectedCard);
        Card GetCard();
    }
}
