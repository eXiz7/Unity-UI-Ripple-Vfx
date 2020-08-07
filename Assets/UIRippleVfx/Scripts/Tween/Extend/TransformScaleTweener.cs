using UnityEngine;

namespace VadimskyiLab.SimpleUI
{
    public static class TransformScaleTweener
    {
        public static ITweenRemoteControl TweenScale2D(this Transform t, Vector2 toValue, float duration, TweenerPlayStyle style)
        {
            var tween = TweenHandlerStaticFactory.Create(t, toValue, duration, style);
            TweenUpdaterMono.Instance.Subscribe(tween);
            return tween.GetRemote();
        }
    }
}