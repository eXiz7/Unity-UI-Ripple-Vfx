using System;

namespace VadimskyiLab.SimpleUI
{
    public interface ITweenRemoteControl
    {
        void OnComplete(Action callback);
        void OnKill(Action callback);
        void Kill();
    }
}