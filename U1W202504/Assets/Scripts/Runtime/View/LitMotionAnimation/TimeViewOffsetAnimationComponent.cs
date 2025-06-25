using System;
using LitMotion.Animation;

namespace U1W
{
    [Serializable]
    [LitMotionAnimationComponentMenu("Custom/Time View Offset")]
    public class TimeViewOffsetAnimationComponent : FloatPropertyAnimationComponent<CurrentTimeView>
    {
        protected override float GetValue(CurrentTimeView target) => target.OffsetSecond;
        protected override void SetValue(CurrentTimeView target, in float value) => target.OffsetSecond = value;
    }
}
