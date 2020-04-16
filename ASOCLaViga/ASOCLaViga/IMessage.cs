using System;
using System.Collections.Generic;
using System.Text;

namespace ASOCLaViga
{
    public interface IMessage
    {
        void LongTime(string message);
        void ShortTime(string message);
    }
}
