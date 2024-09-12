namespace Gameplay.Interfaces
{
    public interface IHighlightable
    {
        void Highlight(bool value);
        Card GetCard();
        bool CanBeTargeted(Player controllerPlayer, Card selectedCard);
    }
}