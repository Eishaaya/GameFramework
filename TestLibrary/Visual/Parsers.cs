using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseGameLibrary.Visual
{

    public abstract class ParserBase<T> : ICopyable<ParserBase<T>>
    {
        public ParserBase(T value)
        {
            this.value = value;
        }
        public T value;
        public T GetValue => value;
        public override string ToString()
            => value.ToString();
        public static implicit operator T(ParserBase<T> me) => me.value;
        public abstract bool Parse(string input);

        public abstract ParserBase<T> Clone();
    }
    public sealed class IntParser : ParserBase<int>
    {
        public IntParser(int value = 0)
            : base(value) { }

        public override IntParser Clone()
            => new IntParser(value);

        public override bool Parse(string input)
        {
            var oldVal = value;
            if (int.TryParse(input, out value))
            {
                return true;
            }
            value = oldVal;
            return false;
        }
    }
    public sealed class FloatParser : ParserBase<float>
    {
        public FloatParser(float value = 0)
            : base(value) { }
        public override FloatParser Clone()
            => new FloatParser(value);
        public override bool Parse(string input)
        {
            var oldVal = value;
            if (float.TryParse(input, out value))
            {
                return true;
            }
            value = oldVal;
            return false;
        }
    }
    public sealed class DoubleParser : ParserBase<double>
    {
        public DoubleParser(double value = 0)
            : base(value) { }
        public override DoubleParser Clone()
            => new DoubleParser(value);

        public override bool Parse(string input)
        {
            var oldVal = value;
            if (double.TryParse(input, out value))
            {
                return true;
            }
            value = oldVal;
            return false;
        }
    }
    public sealed class StringParser : ParserBase<string>
    {
        public StringParser(string value = "")
            : base(value) { }
        public override StringParser Clone()
            => new StringParser(value);
        public override bool Parse(string input)
        {
            value = input;
            return true;
        }
    }
    public sealed class CharParser : ParserBase<char>
    {
        public CharParser(char value = ' ')
            : base(value) { }
        public override CharParser Clone()
            => new CharParser(value);
        public override bool Parse(string input)
        {
            var oldVal = value;
            if (char.TryParse(input, out value))
            {
                return true;
            }
            value = oldVal;
            return false;
        }
    }
}
