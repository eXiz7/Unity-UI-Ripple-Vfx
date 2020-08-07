using System;

namespace VadimskyiLab.SimpleUI
{
    internal sealed class TweenRemoteControl : ITweenRemoteControl
    {
        private Action _onComplete;
        private Action _onKill;

        public void OnComplete(Action callback)
        {
            _onComplete += callback;
        }

        public void OnKill(Action callback)
        {
            _onKill += callback;
        }

        public void Kill()
        {
            _onKill?.Invoke();
        }

        public void Complete()
        {
            _onComplete?.Invoke();
        }
    }
}