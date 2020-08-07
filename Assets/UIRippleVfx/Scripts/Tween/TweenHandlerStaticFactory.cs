/* Copyright (C) 2020 Vadimskyi - All Rights Reserved
 * You may use, distribute and modify this code under the
 * terms of the GPL-3.0 License license.
 */
using System;
using UnityEngine;
using UnityEngine.UI;
using VadimskyiLab.Utils;

namespace VadimskyiLab.SimpleUI
{
    public static class TweenHandlerStaticFactory
    {
        public static ITweenComponentStrategy Create(Transform target, Vector2 toValue, float duration, TweenerPlayStyle style)
        {
            TweenSharedState<Vector2> state = new TweenSharedState<Vector2>()
            {
                FromValue = target.localScale,
                ToValue = toValue,
                Duration = duration
            };
            return new TweenScaleStrategy(
                target,
                state,
                new Vector2ValueModifier(state),
                CreatePlayStyle(state, style));
        }

        public static ITweenComponentStrategy Create(Graphic target, float toValue, float duration, TweenerPlayStyle style)
        {
            TweenSharedState<float> state = new TweenSharedState<float>()
            {
                FromValue = target.GetAlpha(),
                ToValue = toValue,
                Duration = duration
            };
            return new TweenAlphaStrategy(
                target,
                state,
                new FloatValueModifier(state),
                CreatePlayStyle(state, style));
        }

        private static ITweenPlayStyleStrategy CreatePlayStyle(ITweenSharedState state, TweenerPlayStyle style)
        {
            switch (style)
            {
                case TweenerPlayStyle.PingPong: return new TweenPingPongStrategy(state);
                case TweenerPlayStyle.Once: return new TweenOnceStrategy(state);
            }

            throw new NotImplementedException($"Tween play-style \"{style}\" is not implemented!");
        }
    }
}