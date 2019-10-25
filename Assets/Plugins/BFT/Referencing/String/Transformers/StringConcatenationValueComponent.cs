using System.Collections.Generic;
using System.Text;
using Sirenix.OdinInspector;

namespace BFT
{
    public class StringConcatenationValueComponent : SerializedMonoBehaviour, IValue<string>
    {
        private StringBuilder builder = new StringBuilder();

        public StringValue Separator = new StringValue() {LocalValue = " "};
        [BoxGroup("References")] public List<StringValue> Strings;

        [ShowInInspector]
        public string Value
        {
            get
            {
                builder.Clear();
                foreach (var s in Strings)
                {
                    builder.Append(s.Value);
                    builder.Append(Separator.Value);
                }

                return builder.ToString();
            }
        }
    }
}
