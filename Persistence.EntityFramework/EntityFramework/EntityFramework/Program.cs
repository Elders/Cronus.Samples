using System;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = new DbContextOptionsBuilder<Context>();
            builder.UseInMemoryDatabase("Sample");

            using (var context = new Context(builder.Options))
            {
                var repository = new AggregateRepository(context);
                var id = new RootId("123");
                var root = new Root(id);

                repository.Save(root);

                var result = repository.Load<Root>(id);
                if (result.IsSuccess)
                    Console.WriteLine(result.Data.Key);
                else
                    Console.WriteLine(result.Error);

                root.ChangeValue("new value");
                root.ChangeValue("initial value");

                repository.Save(root);
                result = repository.Load<Root>(id);

                if (result.IsSuccess)
                    Console.WriteLine(result.Data.Value);
                else
                    Console.WriteLine(result.Error);
            }
        }
    }
}
