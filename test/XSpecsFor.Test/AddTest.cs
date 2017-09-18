using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace XSpecsFor.Test
{
    public class CommonServicesSetuper : ICommonServicesSetuper
    {
        public void SetUp(ServiceCollection services)
        {
            services.AddSingleton(i =>
            {
                var mock = new Mock<IConstantInt>();
                mock.SetupGet(j => j.ConstantInt).Returns(20);
                return mock.Object;
            });
        }
    }

    public interface IAdder
    {
        int Add(int x, int y);
    }

    public interface IConstantInt
    {
        int ConstantInt { get; }
    }

    public class AddTester
    {
        private IAdder _Adder;
        private IConstantInt _ConstantInt;

        public int GivenInt = 0;

        public int GiWhenInt = 0;

        public AddTester(IAdder adder, IConstantInt constantInt)
        {
            _Adder = adder;
            _ConstantInt = constantInt;
        }

        public int Add(int x, int y) => _Adder.Add(x, y) + GivenInt + GiWhenInt + _ConstantInt.ConstantInt;
    }

    public class AddTest : SpecsFor<AddTester>
    {
        public override void SetUp(IServiceCollection services)
        {
            services.AddSingleton(i =>
            {
                var mock = new Mock<IAdder>();
                mock.Setup(j => j.Add(It.IsAny<int>(), It.IsAny<int>()))
                .Returns<int, int>((x, y) => x + y);
                return mock.Object;
            });
        }

        public override void Given()
        {
            SUT.GivenInt = 6;
        }

        public override void When()
        {
            SUT.GiWhenInt = 4;
        }

        [Fact(DisplayName = "4 + 5 + 6 + 4 + 20 = 35")]
        public void Add4And5()
        {
            Assert.Equal(39, SUT.Add(4, 5));
        }

        [Fact(DisplayName = "6 + 6 + 6 + 4 + 20 = 42")]
        public void Add6And6()
        {
            Assert.Equal(42, SUT.Add(6, 6));
        }
    }

    public class Add2Test : SpecsFor<AddTester>
    {
        public override void SetUp(IServiceCollection services)
        {
            services.AddSingleton(i =>
            {
                var mock = new Mock<IAdder>();
                mock.Setup(j => j.Add(It.IsAny<int>(), It.IsAny<int>()))
                .Returns<int, int>((x, y) => x + y);
                return mock.Object;
            });
        }

        public override void Given()
        {
            SUT.GivenInt = 60;
        }

        public override void When()
        {
            SUT.GiWhenInt = 40;
        }

        [Fact(DisplayName = "3 + 2 + 60 + 40 + 20 = 125")]
        public void Add3And2()
        {
            Assert.Equal(125, SUT.Add(3, 2));
        }
    }
}