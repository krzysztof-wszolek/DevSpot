using DevSpot.Data;
using DevSpot.Models;
using DevSpot.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSpot.Test
{
    public class JobPostingRepositoryTest
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;

        public JobPostingRepositoryTest()
        {
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("JobPostingDb")
            .Options;
        }

        private ApplicationDbContext CreatDbContext() => new ApplicationDbContext(_options);


        [Fact]
        public async Task AddAsync_ShouldAddJobPosting()
        {

            //db context
            var db = CreatDbContext();

            //job posting repository instance
            var repository = new JobPostingRepository(db);

            //job posting 

            var jobPosting = new JobPosting
            {
                Title = "Title test",
                Description = "test disc",
                PostedDate = DateTime.Now,
                Company = "Test company",
                Location = "Test location",
                UserId = "123"

            };

            //execute

            await repository.AddAsync(jobPosting);

            //result

            var resault = db.JobPostings.SingleOrDefault(x => x.Title.Equals("Title test"));

            //assert
            Assert.NotNull(resault);
            Assert.Equal("test disc", resault.Description);


        }


        [Fact]
        public async Task GetByIdAsync_ShouldReturnJobPosting()
        {

            var db = CreatDbContext();

            var repository = new JobPostingRepository(db);           

            var jobPosting = new JobPosting
            {
                Title = "Test title",
                Description = "test disc",
                PostedDate = DateTime.Now,
                Company = "Test company",
                Location = "Test location",
                UserId = "123"

            };

            await db.JobPostings.AddAsync(jobPosting);
            await db.SaveChangesAsync();

            var result = await repository.GetByIdAsync(jobPosting.Id);

            Assert.NotNull(result);
            Assert.Equal("Test title", result.Title);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldThrowKeyNotFoundException()
        {
            var db = CreatDbContext();

            var repository = new JobPostingRepository(db);


           await Assert.ThrowsAsync<KeyNotFoundException>(()=> repository.GetByIdAsync(9999));
        }


        [Fact]
        public async Task GetAllAync_ShoulReturnJobPostings()
        {


            var db = CreatDbContext();

            var repository = new JobPostingRepository(db);

            var jobPosting = new JobPosting
            {
                Title = "Test title",
                Description = "test disc",
                PostedDate = DateTime.Now,
                Company = "Test company",
                Location = "Test location",
                UserId = "123"

            };
            var jobPosting2 = new JobPosting
            {
                Title = "Test title2",
                Description = "test disc2",
                PostedDate = DateTime.Now,
                Company = "Test company2",
                Location = "Test location2",
                UserId = "1232"

            };

            await db.JobPostings.AddRangeAsync(jobPosting,jobPosting2);
            await db.SaveChangesAsync();

            var result = await repository.GetAllAsync();
            Assert.NotNull(result);
            Assert.Equal(2,result.Count());

        }
        [Fact]
        public async Task UpdateAsync_ShouldUpdateJobPosting()
        {

            var db = CreatDbContext();

            var repository = new JobPostingRepository(db);

            var jobPosting = new JobPosting
            {
                Title = "Test title",
                Description = "test disc",
                PostedDate = DateTime.Now,
                Company = "Test company",
                Location = "Test location",
                UserId = "123"

            };

            await db.JobPostings.AddAsync(jobPosting);
            await db.SaveChangesAsync();

            jobPosting.Description = "Updated Description";

            await repository.UpdateAsync(jobPosting);

            var result = db.JobPostings.Find(jobPosting.Id);

            Assert.NotNull(result);
            Assert.Equal("Updated Description", result.Description);

        }


        [Fact]
        public async Task DeleteAsync_ShouldDeleteJobPosting()
        {

            var db = CreatDbContext();

            var repository = new JobPostingRepository(db);

            var jobPosting = new JobPosting
            {
                Title = "Test title",
                Description = "test disc",
                PostedDate = DateTime.Now,
                Company = "Test company",
                Location = "Test location",
                UserId = "123"

            };

            await db.JobPostings.AddAsync(jobPosting);
            await db.SaveChangesAsync();


            await repository.DeleteAsync(jobPosting.Id);

            var result = db.JobPostings.Find(jobPosting.Id);

            Assert.Null(result);
      

        }
    }
}
