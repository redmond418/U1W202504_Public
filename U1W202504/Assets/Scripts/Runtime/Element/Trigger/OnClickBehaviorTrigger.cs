using System;
using R3;
using ReactiveInputSystem;

namespace U1W
{
    [Serializable]
    public class OnClickBehaviorTrigger : IBehaviorTrigger
    {
        public Observable<ITriggerInfo> OnTrigger => InputRx.OnMouseButtonDown(UnityEngine.InputSystem.LowLevel.MouseButton.Left).Select(_ => null as ITriggerInfo);
    }
}
