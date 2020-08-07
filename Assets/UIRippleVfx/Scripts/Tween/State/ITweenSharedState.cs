
namespace VadimskyiLab.SimpleUI
{
    public interface ITweenSharedState
    {
        void SetDuration(float val);
        float GetDuration();
        int GetCycleCount();
        void Swap();
    }
}