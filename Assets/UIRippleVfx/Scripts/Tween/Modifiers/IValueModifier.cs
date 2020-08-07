using System;

namespace VadimskyiLab.SimpleUI
{
    internal interface IValueModifier<T> : IDisposable
    {
        float TimeElapsed();
        TweenSharedState<T> GetOptions();
        T GetStartingValue();
        T GetDestinationValue();
        T ModifyValue(float deltaTime);
        void Reset();
    }
}