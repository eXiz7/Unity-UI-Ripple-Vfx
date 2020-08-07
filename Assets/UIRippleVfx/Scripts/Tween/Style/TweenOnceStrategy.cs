
namespace VadimskyiLab.SimpleUI
{
    internal sealed class TweenOnceStrategy : ITweenPlayStyleStrategy
    {
        private TweenerPlayStyle _style;
        private ITweenSharedState _state;

        public TweenOnceStrategy(ITweenSharedState sharedState)
        {
            _style = TweenerPlayStyle.Once;
            _state = sharedState;
        }

        public void InitializeState()
        {

        }

        public bool CanComplete()
        {
            return _state.GetCycleCount() > 0;
        }

        public TweenerPlayStyle GetPlayStyle()
        {
            return _style;
        }

        public void Update(float deltaTime)
        {

        }

        public void Dispose()
        {
        }
    }
}