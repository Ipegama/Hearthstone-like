using Gameplay;

namespace TriggerSystem
{
    public abstract class Buff
    {
        public abstract void OnApply(Card card);
        public abstract void OnRemove(Card card);
    }
}
