using Barber.Api.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Barber.Api.Extensions;

internal static class StartupHelperExtensions
{
   public static async Task ResetDatabaseAsync(this WebApplication app)
   {
       using (var scope = app.Services.CreateScope())
       {
           try
           {
               var context = scope.ServiceProvider.GetService<CustomerContext>();
               if (context != null)
               {
                   await context.Database.EnsureDeletedAsync();
                   await context.Database.MigrateAsync();
               }
           }
           catch (Exception ex)
           {
               var logger = scope.ServiceProvider.GetRequiredService<ILogger>();
               logger.LogError(ex, "An error occurred while migrating the database.");
           }
       }
   }
}
