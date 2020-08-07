using UnityEngine;

namespace VadimskyiLab.SimpleUI
{
    internal sealed class Vector2ValueModifier : IValueModifier<Vector2>
    {
        private float _timeFromStart;
        private TweenSharedState<Vector2> _sharedState;

        public Vector2ValueModifier(TweenSharedState<Vector2> sharedState)
        {
            _timeFromStart = 0;
            _sharedState = sharedState;
        }

        public float TimeElapsed()
        {
            return _timeFromStart;
        }

        public TweenSharedState<Vector2> GetOptions() => _sharedState;

        public Vector2 GetStartingValue()
        {
            return _sharedState.FromValue;
        }

        public Vector2 GetDestinationValue()
        {
            return _sharedState.ToValue;
        }

        public Vector2 ModifyValue(float deltaTime)
        {
            _timeFromStart += deltaTime;

            return Vector2.Lerp(_sharedState.FromValue, _sharedState.ToValue, _timeFromStart / _sharedState.Duration);
        }

        public void Reset()
        {
            _timeFromStart = 0;
        }

        public void Dispose()
        {
        }
    }
}