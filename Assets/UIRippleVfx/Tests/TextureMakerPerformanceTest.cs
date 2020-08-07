using System.Diagnostics;
using NUnit.Framework;
using UnityEngine;
using VadimskyiLab.Utils;

namespace Tests
{
    public class TextureMakerPerformanceTest : MonoBehaviour
    {
        private int _cycles = 100;

        [Test]
        public void Test()
        {
            var sw = Stopwatch.StartNew();
            int width, height, x, y, radius;
            width = 100;
            height = 100;
            x = width/2;
            y = height/2;
            radius = x;
            Test_CreateCircleTexture(width, height, x, y, radius, sw);
        }

        private void Test_CreateCircleTexture(int width, int height, int x, int y, int radius, Stopwatch sw)
        {
            sw.Restart();
            var tex = TextureStaticFactory.CreateCircleTexture(Color.white, width, height, x, y, radius);
            for (int i = 0; i < _cycles; i++)
            {
                TextureStaticFactory.ReturnTexture(tex);
                var _generatedTexture = TextureStaticFactory.CreateCircleTexture(Color.white, width, height, x, y, radius);
                Sprite.Create(
                    _generatedTexture,
                    new Rect(Vector2.zero, new Vector2(width, height)),
                    Vector2.zero,
                    100,
                    1,
                    SpriteMeshType.FullRect);
            }
            print($"{nameof(Test_CreateCircleTexture)} - {sw.ElapsedMilliseconds}ms");
        }
    }
}