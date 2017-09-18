using Microsoft.Extensions.DependencyInjection;
using System;

namespace XSpecsFor
{
    /// <summary>
    /// Spec interface
    /// </summary>
    public interface ISpec : IDisposable
    {
        /// <summary>
        /// <see cref="IServiceProvider"/>
        /// </summary>
        IServiceProvider Provider { get; }

        /// <summary>
        /// init services
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/></param>
        void SetUp(IServiceCollection services);

        /// <summary>
        /// Given: establish state
        /// </summary>
        void Given();

        /// <summary>
        /// When: perform an action
        /// </summary>
        void When();
        
        /// <summary>
        /// clean up
        /// </summary>
        void TearDown();
    }

    /// <summary>
    /// System Under Test Spec interface
    /// </summary>
    /// <typeparam name="T">System Under Test type</typeparam>
    public interface ISpec<T> : ISpec
    {
        /// <summary>
        /// System Under Test
        /// </summary>
        T SUT { get; }
    }
}