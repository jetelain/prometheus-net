namespace Prometheus;

public class InfoConfiguration : MetricConfiguration
{
    internal static readonly InfoConfiguration Default = new InfoConfiguration();

    /// <summary>
    /// Includes <see cref="CollectorRegistry.StaticLabels"/> in the info metric. 
    /// </summary>
    public bool UseStaticLabels { get; set; } = false;
}