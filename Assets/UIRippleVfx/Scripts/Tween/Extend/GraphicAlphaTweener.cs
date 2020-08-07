using UnityEngine.UI;

namespace VadimskyiLab.SimpleUI
{
    public static class GraphicAlphaTweener
    {
        public static ITweenRemoteControl TweenAlpha(this Graphic obj, float toValue, float duration, TweenerPlayStyle style)
        {
            var tween = TweenHandlerStaticFactory.Create(obj, toValue, duration, style);
            TweenUpdaterMono.Instance.Subscribe(tween);
            return tween.GetRemote();
        }
    }
}
