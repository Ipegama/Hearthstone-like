using System.Collections;
using TriggerSystem;

namespace GameAnimations
{
    internal class ProjectileAnimation : GameAnimation
    {
        private ProjectileActionData data;
        private ActionContext context;

        public ProjectileAnimation(ProjectileActionData data, ActionContext context)
        {
            this.data = data;
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