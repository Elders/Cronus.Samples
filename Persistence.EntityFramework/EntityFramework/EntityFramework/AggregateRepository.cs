using System.Linq;
using Elders.Cronus;

namespace EntityFramework
{
    public class AggregateRepository : IAggregateRepository
    {
        readonly Context context;

        public AggregateRepository(Context context)
        {
            this.context = context;
        }

        public ReadResult<AR> Load<AR>(IAggregateRootId id) where AR : IAggregateRoot
        {
            var ar = context.Find(typeof(AR), id.ToString());
            return new ReadResult<AR>((AR)ar);
        }

        public void Save<AR>(AR aggregateRoot) where AR : IAggregateRoot
        {
            var isTracked = context.ChangeTracker.Entries().Any(x => x.Entity.Equals(aggregateRoot));
            if (isTracked == false)
                context.Add(aggregateRoot);
            else
                context.Update(aggregateRoot);

            context.SaveChanges();
        }
    }
}
