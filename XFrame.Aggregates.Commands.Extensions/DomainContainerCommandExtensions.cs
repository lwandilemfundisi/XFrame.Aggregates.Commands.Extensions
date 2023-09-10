using System.Reflection;
using XFrame.DomainContainers;

namespace XFrame.Aggregates.Commands.Extensions
{
    public static class DomainContainerCommandExtensions
    {
        public static IDomainContainer AddCommands(
            this IDomainContainer domainContainer,
            params Type[] commandTypes)
        {
            return domainContainer.AddCommands(commandTypes);
        }

        public static IDomainContainer AddCommands(
            this IDomainContainer domainContainer,
            Assembly fromAssembly,
            Predicate<Type> predicate)
        {
            predicate = predicate ?? (t => true);
            var commandTypes = fromAssembly
                .GetTypes()
                .Where(t => !t.GetTypeInfo().IsAbstract && typeof(ICommand).GetTypeInfo().IsAssignableFrom(t))
                .Where(t => predicate(t));
            return domainContainer.AddTypes(commandTypes);
        }
    }
}
