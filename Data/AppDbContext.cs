using Amazon.Runtime.Internal.Endpoints.StandardLibrary;
using MongoDB.Driver;
using Stacks_rework.Models;

namespace Stacks_rework.Data
{
    public class AppDbContext
    {
        public AppDbContext()
        {
      
            var host = "localhost";
            //Environment.GetEnvironmentVariable("HOST");
            var port = "41400";
                //Environment.GetEnvironmentVariable("PORT");
            var user = "root";
            //Environment.GetEnvironmentVariable("USER");
            var password = "admin";
            //Environment.GetEnvironmentVariable("PWD");

            var db = "decks";
            //Environment.GetEnvironmentVariable("DATABASE");
            var collection = "decks";
            //Environment.GetEnvironmentVariable("COLLECTION");


            var Client = new MongoClient($"mongodb://{user}:{password}@{host}:{port}/?authSource=admin");
            database = Client.GetDatabase(db);
        }

        public IMongoDatabase database { get; set; }
        
        
    }
}
