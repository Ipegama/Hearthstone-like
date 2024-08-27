using System.Collections;

namespace GameAnimations
{
    public abstract class GameAnimation 
    {
        public abstract IEnumerator Execute();
        public abstract void ExecuteWithoutAnimation();
    }

}
