using System;
using System.Collections.Generic;
using Sirenix.OdinInspector.Editor;
using UnityEngine;

namespace BFT
{
    public class ColorUsagePropertyAttributeProcessor : OdinAttributeProcessor<UnityEngine.Color>
    {
        public override void ProcessSelfAttributes(InspectorProperty property, List<Attribute> attributes)
        {
            for (int i = 0; i < attributes.Count; i++)
            {
                var attr = attributes[i];

                if (attr is ColorUsagePropertyAttribute)
                {
                    var cast = attr as ColorUsagePropertyAttribute;

                    attributes[i] = new ColorUsageAttribute(cast.showAlpha, cast.hdr);
                }
            }
        }
    }
}
