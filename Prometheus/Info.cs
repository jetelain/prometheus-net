namespace Prometheus;

/// <summary>
/// Info metrics are used to expose textual information which should not change during process lifetime. The value of an Info metric is always 1.
/// </summary>
public sealed class Info : Collector<Info.Child>
{
    internal Info(string name, string help, StringSequence instanceLabelNames, LabelSequence staticLabels, bool suppressInitialValue, ExemplarBehavior exemplarBehavior)
        : base(name, help, instanceLabelNames, staticLabels, suppressInitialValue, exemplarBehavior)
    {

    }

    internal override MetricType Type => MetricType.Info;

    internal override int TimeseriesCount => 0;

    private protected override Child NewChild(LabelSequence instanceLabels, LabelSequence flattenedLabels, bool publish, ExemplarBehavior exemplarBehavior)
    {
        return new Child(this, instanceLabels, flattenedLabels, publish, exemplarBehavior);
    }

    public sealed class Child : ChildBase
    {
        internal Child(Collector parent, LabelSequence instanceLabels, LabelSequence flattenedLabels, bool publish, ExemplarBehavior exemplarBehavior)
            : base(parent, instanceLabels, flattenedLabels, publish, exemplarBehavior)
        {
        }

        private protected override async ValueTask CollectAndSerializeImplAsync(IMetricsSerializer serializer, CancellationToken cancel)
        {
            await serializer.WriteMetricPointAsync(
                Parent.NameBytes,
                FlattenedLabelsBytes,
                CanonicalLabel.Empty,
                1,
                ObservedExemplar.Empty,
                null,
                cancel);
        }
    }
}
