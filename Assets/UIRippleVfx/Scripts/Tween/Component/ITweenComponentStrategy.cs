using System;

namespace VadimskyiLab.SimpleUI
{
    public interface ITweenComponentStrategy : IDisposable
    {
        object GetComponent();
        ITweenPlayStyleStrategy GetPlayStyle();
        void UpdateComponent(float deltaTime);
        TweenComponentState GetState();
        ITweenRemoteControl GetRemote();
    }

    public enum TweenComponentState
    {
        None,
        Processing,
        Completed,
        Killed
    }
}