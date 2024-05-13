using EBikeAppWebAPI.data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBikeAppWebAPI.data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<EBikeDbContext>
    {
        public EBikeDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<EBikeDbContext> dbContextOptionsBuilder = new();
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
            dbContextOptionsBuilder.UseMySql("server=localhost;port=3306;user=root;password=gokhan949;database=ebikeapp;", serverVersion);
            return new EBikeDbContext(dbContextOptionsBuilder.Options);
        }
    }
}
