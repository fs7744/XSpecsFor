using Microsoft.Extensions.DependencyInjection;

namespace XSpecsFor
{
    /// <summary>
    /// a interface help find custom CommonServicesSetuper
    /// </summary>
    /// <example> 
    /// <code>
    ///     public class CommonServicesSetuper : ICommonServicesSetuper
    ///     {
    ///         public void SetUp(ServiceCollection services)
    ///         {
    ///             services.AddSingleton(i =>
    ///             {
    ///                 var mock = new Mock<IConstantInt>();
    ///                 mock.SetupGet(j => j.ConstantInt).Returns(20);
    ///                 return mock.Object;
    ///             });
    ///         }
    ///     }
    /// </code>
    /// </example> 
    public interface ICommonServicesSetuper
    {
        /// <summary>
        /// a method to setup custom common services
        /// <example> 
        /// <code>
        ///     public void SetUp(ServiceCollection services)
        ///     {
        ///         services.AddSingleton(i =>
        ///         {
        ///             var mock = new Mock<IConstantInt>();
        ///             mock.SetupGet(j => j.ConstantInt).Returns(20);
        ///             return mock.Object;
        ///         });
        ///     }
        /// </code>
        /// </example> 
        /// <param name="services"><see cref="ServiceCollection"/></param>
        void SetUp(ServiceCollection services);
    }
}