using System.Collections.Generic;

namespace ColorSequenceGenerator
{
    public interface IColorSequenceGenerator
    {
        IEnumerable<RGB> ColorSequence();
    }
}