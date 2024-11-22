using System.Runtime.InteropServices;

namespace Prometheus
{
    public static class DotNetRuntimeInfos
    {
        /// <summary>
        /// Registers the .NET runtime infos in the default registry.
        /// </summary>
        internal static void RegisterDefault()
        {
            Register(Metrics.DefaultFactory);
        }

        /// <summary>
        /// Registers the .NET runtime infos in the specified registry.
        /// </summary>
        /// <param name="registry"></param>
        public static void Register(CollectorRegistry registry)
        {
            Register(Metrics.WithCustomRegistry(registry));
        }

        /// <summary>
        /// Registers the .NET runtime infos in the specified factory.
        /// </summary>
        /// <param name="factory"></param>
        public static void Register(MetricFactory factory)
        {
            factory.CreateInfo("dotnet_info", ".NET runtime", new[] { "version", "framework", "runtime" })
                .WithLabels(Environment.Version.ToString(), RuntimeInformation.FrameworkDescription, RuntimeIdentifier);
        }

#if NETCOREAPP
        private static string RuntimeIdentifier => RuntimeInformation.RuntimeIdentifier;
#else
        private static string RuntimeIdentifier => "unknown";
#endif
    }
}

