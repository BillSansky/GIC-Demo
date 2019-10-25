using System;

namespace BFT
{
    public class ColorUsagePropertyAttribute : Attribute
    {
        public bool hdr;
        public bool showAlpha;


        public ColorUsagePropertyAttribute(bool showAlpha)
        {
            this.showAlpha = showAlpha;
        }

        public ColorUsagePropertyAttribute(bool showAlpha, bool hdr)
        {
            this.showAlpha = showAlpha;
            this.hdr = hdr;
        }
    }
}
