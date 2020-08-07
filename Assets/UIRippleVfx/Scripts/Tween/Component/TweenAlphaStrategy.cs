using UnityEngine.UI;
using VadimskyiLab.Utils;

namespace VadimskyiLab.SimpleUI
{
    internal sealed class TweenAlphaStrategy : ITweenComponentStrategy
    {
        private Graphic _target;
        private TweenRemoteControl _remote;
        private TweenComponentState _state;
        private IValueModifier<float> _mod;
        private ITweenPlayStyleStrategy _style;
        private TweenSharedState<float> _sharedState;

        public TweenAlphaStrategy(Graphic target, ITweenSharedState sharedState, IValueModifier<float> modHandler, ITweenPlayStyleStrategy style)
        {
            _target = target;
            _mod = modHandler;
            _style = style;
            _sharedState = (TweenSharedState<float>)sharedState;
            _state = TweenComponentState.None;
            _remote = new TweenRemoteControl();
            _style.InitializeState();
            SubscribeToRemote();
        }

        public void UpdateComponent(float deltaTime)
        {
            if (CanComplete())
            {
                _sharedState.CycleCount++;
                if (_style.CanComplete())
                {
                    TweenCompleted();
                    return;
                }
                _style.InitializeState();
                _mod.Reset();
            }
            _target.SetAlpha(_mod.ModifyValue(deltaTime));
            _state = TweenComponentState.Processing;
        }

        public object GetComponent() => _target;

        public ITweenPlayStyleStrategy GetPlayStyle() => _style;

        private bool CanComplete() => _mod.TimeElapsed() >= _sharedState.Duration;

        public TweenComponentState GetState() => _state;

        public ITweenRemoteControl GetRemote() => _remote;

        public void Dispose()
        {
            _mod?.Dispose();
            _style?.Dispose();
        }

        private void Kill()
        {
            _state = TweenComponentState.Killed;
            Dispose();
        }

        private void TweenCompleted()
        {
            _state = TweenComponentState.Completed;
            _remote.Complete();
            Dispose();
        }

        private void SubscribeToRemote()
        {
            _remote.OnKill(Kill);
        }
    }
}