using Amazon.Runtime.Internal.Endpoints.StandardLibrary;
using MongoDB.Driver;
using Stacks_rework.Models;

namespace Stacks_rework.Data
{
    public class AppDbContext
    {
        public AppDbContext()
        {
      
            var host = Environment.GetEnvironmentVariable("HOST")?? "localhost";
            var port = Environment.GetEnvironmentVariable("PORT")?? "41400";
            var user = Environment.GetEnvironmentVariable("USER")?? "root";
            var password = Environment.GetEnvironmentVariable("PASSWORD")?? "admin";
            var db = Environment.GetEnvironmentVariable("DATABASE")?? "decks";


            var Client = new MongoClient($"mongodb://{user}:{password}@{host}:{port}/?authSource=admin");
            database = Client.GetDatabase(db);
        }

        public IMongoDatabase database { get; set; }
        
        
    }
}
