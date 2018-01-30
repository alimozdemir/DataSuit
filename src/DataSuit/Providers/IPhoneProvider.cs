using System;
using System.Collections.Generic;
using System.Text;

namespace DataSuit.Providers
{
    public interface IPhoneProvider : IDataProvider<string>
    {
        long AsNumeric();
        string Format { get; }
    }
}
