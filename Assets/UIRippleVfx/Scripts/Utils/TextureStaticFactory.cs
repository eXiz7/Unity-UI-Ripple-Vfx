/* Copyright (C) 2020 Vadimskyi - All Rights Reserved
 * You may use, distribute and modify this code under the
 * terms of the GPL-3.0 License license.
 */

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VadimskyiLab.Utils
{
    /// <summary>
    /// Creates and reuses(pooling) textures for ButtonRippleEffect
    /// </summary>
    public static class TextureStaticFactory
    {
        private static ConcurrentQueue<Texture2D> _circlePool;
        private static Color _transparent = new Color(255, 255, 255, 0);

        static TextureStaticFactory()
        {
            _circlePool = new ConcurrentQueue<Texture2D>();
        }

        public static void ReturnTexture(Texture2D tex)
        {
            if(tex == null) return;
            if (_circlePool.Contains(tex))
            {
                UnityEngine.Debug.LogError($"Texture was already returned!");
                return;
            }
            _circlePool.Enqueue(tex);
        }

        /// <summary>
        /// Creates a filled circle texture
        /// </summary>
        public static Texture2D CreateCircleTexture(Color color, int width, int height, int x, int y, int radius)
        {
            Texture2D tex = GetCircleTexture(width, height);

            float rSquared = radius * radius;
            var colors = tex.GetPixels32();

            int index = 0;
            for (int u = x - radius; u < x + radius + 1; u++)
            {
                for (int v = y - radius; v < y + radius + 1; v++)
                {
                    index = u * width + v;
                    if (index >= colors.Length) continue;
                    if ((x - u) * (x - u) + (y - v) * (y - v) < rSquared)
                        colors[index] = color;
                    else
                        colors[index] = _transparent;
                }
            }
            tex.SetPixels32(colors);
            tex.Apply();
            return tex;
        }

        private static Texture2D GetCircleTexture(int width, int height)
        {
            Texture2D tex = null;
            if (!_circlePool.IsEmpty && _circlePool.TryDequeue(out tex))
            {
                if(tex.width != width || tex.height != height)
                    tex.Resize(width, height);
            }
            else
            {
                tex = new Texture2D(width, height, TextureFormat.RGBA32, false);
                tex.name = "tex_circle_.jpg";
                tex.wrapMode = TextureWrapMode.Clamp;
                tex.filterMode = FilterMode.Bilinear;
            }
            return tex;
        }
    }
}
