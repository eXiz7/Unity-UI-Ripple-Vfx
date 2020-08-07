using System;

namespace VadimskyiLab.SimpleUI
{
    public interface ITweenPlayStyleStrategy : IDisposable
    {
        void InitializeState();
        bool CanComplete();
        TweenerPlayStyle GetPlayStyle();
        void Update(float deltaTime);
    }

    public enum TweenerPlayStyle
    {
        Once,
        Loop,
        PingPong
    }
}