namespace Gameplay
{
    internal interface IHighlightable
    {
        bool CanBeTargeted(Player controllerPlayer, Card selectedCard);
        void Highlight(bool value);

    }
}