using System;
using System.Linq;
using QNomy.Api.Data;
using QNomy.Domain.Entities;

namespace QNomy.Api.SeedData
{
    public static class SeedData
    {
        public static void PopulateTestData(DataContext context)
        {
            AddClients(context);
        }

        private static void AddClients(DataContext context)
        {
            if (!context.Clients.Any())
            {
                context.Clients.Add(new Client() { Id = 1, FullName = "client1", NumberInLine = 1, CheckInTime = DateTime.Today});
                context.Clients.Add(new Client() { Id = 2, FullName = "client2", NumberInLine = 2, CheckInTime = DateTime.Today.AddHours(1)  });
                context.Clients.Add(new Client() { Id = 3, FullName = "client3", NumberInLine = 3, CheckInTime = DateTime.Today.AddHours(2)  });
                context.Clients.Add(new Client() { Id = 4, FullName = "client3", NumberInLine = 4, CheckInTime = DateTime.Today.AddHours(3) });
                context.SaveChanges();
            }
        }
    }
}