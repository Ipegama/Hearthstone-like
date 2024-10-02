namespace Gameplay.Interfaces
{
    public interface IHighlightable
    {
        void Highlight(bool value);
        Card GetCard();
        bool CanBeHighlight(Player controllerPlayer, Card selectedCard);
    }
}