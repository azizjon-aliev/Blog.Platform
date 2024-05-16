using Microsoft.EntityFrameworkCore;

namespace Identity.DataProvider;

public class DatabaseInitializer
{
    public static void Initialize(ApplicationDbContext context)
    {
        context.Database.EnsureCreated();
    }
}