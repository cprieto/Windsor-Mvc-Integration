using System;
using System.Linq;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Xunit;

namespace Castle.Windsor.Integration.Mvc.Tests
{
    public class DependencyResolverFacts
    {
        public interface INonExistingComponent { }
        public interface ISingleComponent { }
        public class SingleComponent : ISingleComponent { };

        private readonly IDependencyResolver _resolver;

        public DependencyResolverFacts()
        {
            var container = new WindsorContainer();
            container.Register(
                Component.For<ISingleComponent>()
                .ImplementedBy<SingleComponent>());

            _resolver = new WindsorDependencyResolver(container);
        }

        [Fact]
        public void ItShouldReturnCorrectComponent()
        {
            var component = _resolver.GetService<ISingleComponent>();
            Assert.IsType<SingleComponent>(component);
        }

        [Fact]
        public void ItShouldReturnNullWhenComponentIsNotRegistered()
        {
            var component = _resolver.GetService<INonExistingComponent>();
            Assert.Null(component);
        }

        [Fact]
        public void ItShouldReturnNonEmptyCollectionOfComponents()
        {
            var components = _resolver.GetServices<ISingleComponent>();

            Assert.NotEmpty(components);
        }

        [Fact]
        public void ItShouldReturnEmptyCollectionWhenComponentDoesNotExists()
        {
            var components = _resolver.GetServices<INonExistingComponent>();

            Assert.Empty(components);
        }
    }
}