using Microsoft.Extensions.DependencyInjection;
using System;

namespace XSpecsFor
{
    /// <summary>
    /// System Under Test Spec
    /// </summary>
    /// <typeparam name="T">System Under Test type</typeparam>
    public abstract class SpecsFor<T> : ISpec<T> where T : class
    {
        /// <summary>
        /// System Under Test
        /// </summary>
        public T SUT { get; protected set; }

        /// <summary>
        /// <see cref="IServiceProvider"/>
        /// </summary>
        public IServiceProvider Provider { get; protected set; }

        public SpecsFor()
        {
            SpecsForCommonServices.Instance.RegisterCommonServices(GetType().Assembly);
            var services = SpecsForCommonServices.Instance.Copy();
            SetUp(services);
            services.AddTransient<T, T>();
            Provider = services.BuildServiceProvider();
            SUT = Provider.GetRequiredService<T>();
            Given();
            When();
        }

        /// <summary>
        /// init services
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/></param>
        public virtual void SetUp(IServiceCollection services)
        {
        }

        /// <summary>
        /// clean up
        /// </summary>
        public virtual void TearDown()
        {
        }

        /// <summary>
        /// Given: establish state
        /// </summary>
        public virtual void Given()
        {
        }

        /// <summary>
        /// When: perform an action
        /// </summary>
        public virtual void When()
        {
        }

        #region IDisposable Support

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    TearDown();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion IDisposable Support
    }
}