using DevSpot.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSpot.Test
{
    internal class JobPostingRepositoryTest
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;

        public JobPostingRepositoryTest()
        {
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("JobPostingDb")
            .Options;
        }

        private ApplicationDbContext CreatDbContext() => new ApplicationDbContext(_options);
            

    }
}
