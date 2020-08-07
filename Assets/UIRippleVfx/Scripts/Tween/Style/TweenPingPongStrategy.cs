
namespace VadimskyiLab.SimpleUI
{
    internal sealed class TweenPingPongStrategy : ITweenPlayStyleStrategy
    {
        private TweenerPlayStyle _style;
        private ITweenSharedState _state;

        public TweenPingPongStrategy(ITweenSharedState sharedState)
        {
            _style = TweenerPlayStyle.PingPong;
            _state = sharedState;
        }

        public void InitializeState()
        {
            if (_state.GetCycleCount() == 0)
                _state.SetDuration(_state.GetDuration() / 2);
            else
                SwapInitialParams();
        }

        public bool CanComplete()
        {
            return _state.GetCycleCount() > 1;
        }

        public TweenerPlayStyle GetPlayStyle()
        {
            return _style;
        }

        public void Update(float deltaTime)
        {

        }

        private void SwapInitialParams()
        {
            _state.Swap();
        }

        public void Dispose()
        {
        }
    }
}

