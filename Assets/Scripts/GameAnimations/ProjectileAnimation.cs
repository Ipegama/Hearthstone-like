using System.Collections;
using TriggerSystem;
using TriggerSystem.Data;

namespace GameAnimations
{
    internal class ProjectileAnimation : GameAnimation
    {
        private ProjectileActionData _data;
        private ActionContext context;

        public ProjectileAnimation(ProjectileActionData data, ActionContext context)
        {
            this._data = data;
            this.context = context;
        }

        public override IEnumerator Execute()
        {
            throw new System.NotImplementedException();
        }

        public override void ExecuteWithoutAnimation()
        {
            throw new System.NotImplementedException();
        }
    }
}