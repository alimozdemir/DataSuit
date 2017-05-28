using System;
using Xunit;
using DataSuit.Infrastructures;
using System.Collections.Generic;
using DataSuit.Enums;
using Xunit.Abstractions;
using DataSuit.Interfaces;

namespace DataSuit.Tests
{

    public class SamplerTests
    {
        // Mock Testing would cover sampling scenarios better IMO
        // Test normal sampling case
        [Fact]
        public void testRandomIndistinct()
        {
            int min = 10, max = 20;

            List<int> sampleTarget = new List<int>();
            for( int i = min ; i < max ; i++)
            {
                sampleTarget.Add(i);
            }

            ISampler<int>  sampler = new IndistinctSampler<int>(sampleTarget);
            var sampled = sampler.Sample(3);

            foreach(var sample in sampled)
            {
                Assert.True(sampleTarget.Contains(sample), $"Value of sample is {sample}");
            }
        }

        //Test when sample size is greater than or equal to data size
        [Fact]
        public void testRandomIndistinctPopulated()
        {
            int min = 10, max = 20;

            List<int> sampleTarget = new List<int>();
            for( int i = min ; i < max ; i++)
            {
                sampleTarget.Add(i);
            }

            ISampler<int>  sampler = new IndistinctSampler<int>(sampleTarget);
            var sampled = sampler.Sample(max - min+10);

            foreach(var sample in sampled)
            {
                Assert.True(sampleTarget.Contains(sample), $"Value of sample is {sample}");
            }
        }

        [Fact]
        public void testRandomReservoir()
        {
            int min = 10, max = 20;
            bool[] hit = new bool[max-min];

            List<int> sampleTarget = new List<int>();
            for( int i = min ; i < max ; i++)
            {
                sampleTarget.Add(i);
            }

            ISampler<int>  sampler = new ReservoirSampler<int>(sampleTarget);
            var sampled = sampler.Sample(max - min - 3 );

            foreach(var sample in sampled)
            {
                Assert.True(sampleTarget.Contains(sample), $"Value of sample is {sample}");
                Assert.False(hit[sample-min]);
                hit[sample-min] = true;
            }
        }

        [Fact]
        public void testGaussian()
        {
            int mean = 10, variance = 2;
            IGaussianSampler<double> sampler = new GaussianSampler<double>();
            var sampled = sampler.Sample(40,mean,variance);
            Console.WriteLine("Starting GaussianSampler Tests");
            foreach(var sample in sampled)
            {
                //Assert.True(sampleTarget.Contains(sample), $"Value of sample is {sample}");
                Console.WriteLine(sample);
            }
        }
    }
}
