using System;

namespace DataSuit
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class TestSetupAttribute : Attribute
    {
        public TestSetupAttribute(Type suit)
        {
            Suit = suit;
        }

        public Type Suit { get; private set; }
    }

    public interface IAttributeSuit
    {
        DataSuit Suit { get; }
    }
}