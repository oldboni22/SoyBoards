#region

using Microsoft.EntityFrameworkCore;

#endregion

namespace Common.Data;

public static class DataExtensions
{
    extension(DbContextOptionsBuilder optionsBuilder)
    {
        public DbContextOptionsBuilder AddTimeStampInterceptor()
        {
            return optionsBuilder
                .AddInterceptors(new TimeStampInterceptor());
        }
    }
}
