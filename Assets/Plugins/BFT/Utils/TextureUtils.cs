using UnityEngine;

namespace BFT
{
    public static class TextureUtils
    {
        public static Texture2D ColorTexture(int width, int height, Color col)
        {
            //remove color variation for versions under 2017
            var pix = new Color[width * height];
            for (int i = 0; i < pix.Length; i++)
            {
                pix[i] = QualitySettings.activeColorSpace == ColorSpace.Linear ? col.linear : col;

                //       pix[i] = col;
            }

            var result = new Texture2D(width, height, TextureFormat.RGBA32, false, true);
            result.SetPixels(pix);
            result.Apply();
            return result;
        }
    }
}
