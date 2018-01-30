using System;
using System.Collections.Generic;
using System.Text;

namespace DataSuit.Providers
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
