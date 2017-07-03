using System;
using System.Collections.Generic;
using System.Text;

namespace DataSuit.Interfaces
{
    public enum TextSource
    {
        Lorem,
    }

    public interface IDummyTextProvider : IDataProvider<string>
    {
        TextSource Source { get; }
        int MaxLength { get; }
    }
}
