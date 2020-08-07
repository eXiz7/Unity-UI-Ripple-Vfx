using UnityEngine;

namespace VadimskyiLab.SimpleUI
{
    public static class InputUtils
    {
        public static Vector2 GetInputPosition()
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            return Input.mousePosition;
#else
            if(Input.touchCount > 0)
                return Input.GetTouch(0).position;
            return Vector2.zero;
#endif
        }
    }
}
