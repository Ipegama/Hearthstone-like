using TriggerSystem;
using UnityEngine;

namespace Gameplay.Interfaces
{
    public interface IPlayable
    {
        void Select(bool value);
        TargetFilter GetTargetFilter();
        Transform GetTransform();
        bool CanBeUsed(Player player);
        void ExecuteAction(Player player, ITargetable target);
    }
}