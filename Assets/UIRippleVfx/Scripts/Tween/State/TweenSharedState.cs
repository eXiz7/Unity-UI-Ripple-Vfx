
namespace VadimskyiLab.SimpleUI
{
    internal sealed class TweenSharedState<T> : ITweenSharedState
    {
        public T FromValue;
        public T ToValue;
        public float Duration;
        public int CycleCount;

        public TweenSharedState()
        {
            CycleCount = 0;
        }

        public void SetDuration(float val)
        {
            Duration = val;
        }

        public float GetDuration() => Duration;

        public int GetCycleCount() => CycleCount;

        public void Swap()
        {
            var tmp = FromValue;
            FromValue = ToValue;
            ToValue = tmp;
        }
    }
}