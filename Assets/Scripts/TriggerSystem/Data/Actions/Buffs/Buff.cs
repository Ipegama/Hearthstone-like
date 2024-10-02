using Gameplay;

namespace TriggerSystem
{
    public abstract class Buff
    {
        public abstract void OnApply(IBuffable entity);
        public abstract void OnRemove(IBuffable entity);
    }
}
