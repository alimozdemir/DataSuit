using System;
using Xunit;

namespace DataSuit.Tests
{
    public class DataSuitTest
    {
        [Fact]
        public void Test1()
        {
            DataSuit ds = new DataSuit(new Infrastructures.Settings());

            ds.Build()
                .Range("Age", 1, 20)
                .Set("Name", "Alim")
                .Set("Gender", 1);

            foreach (var item in ds.Test())
            {
                Console.WriteLine(item.Key);
            }
        }
    }
}