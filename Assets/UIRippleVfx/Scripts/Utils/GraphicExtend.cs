using UnityEngine;
using UnityEngine.UI;

namespace VadimskyiLab.Utils
{
    public static class GraphicExtend
    {
        public static void SetAlpha(this Graphic source, float a)
        {
            source.color = new Color(source.color.r, source.color.g, source.color.b, a);
        }
        public static float GetAlpha(this Graphic source)
        {
            return source.color.a;
        }
    }
}
