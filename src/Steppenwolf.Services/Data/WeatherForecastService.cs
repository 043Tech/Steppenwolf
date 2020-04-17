using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Steppenwolf.CosmosRepositories.Interfaces;
using Steppenwolf.Models;
using Steppenwolf.Services.Models;

namespace Steppenwolf.Services.Data
{
    public class WeatherForecastService
    {
        private readonly IRepository<Test> repository;

        public WeatherForecastService(IRepository<Test> repository)
        {
            this.repository = repository;
        }
        
        public async Task<WeatherForecast[]> GetForecastAsync(DateTime startDate)
        {
            var summaries = await this.repository.Query().ToListAsync();
            var rng = new Random();
            return await Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = startDate.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = summaries[rng.Next(summaries.Count)].Type
            }).ToArray());
        }
    }
}