using FluentScheduler;

namespace NewsNotify.Registries
{
    public class SiteRegistry : Registry
    {
        public SiteRegistry(IJob job, int minutes)
        {
            if (job is null)
                throw new ArgumentNullException(nameof(job));

            if (minutes <= 0)
                throw new ArgumentOutOfRangeException(nameof(minutes));

            this.Schedule(job).NonReentrant().ToRunNow().AndEvery(minutes).Minutes();
        }
    }
}
