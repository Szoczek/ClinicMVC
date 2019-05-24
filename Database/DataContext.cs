using MongoDB.Driver;

namespace Database
{
    public class DataContext
    {
        private readonly IMongoDatabase _database;

        public DataContext()
        {
            MongoDefaults.GuidRepresentation = MongoDB.Bson.GuidRepresentation.Standard;

            var client = new MongoClient("mongodb://localhost:27017");
            _database = client.GetDatabase("Clinic");
        }

        public IMongoCollection<TModel> GetCollection<TModel>()
        {
            return _database.GetCollection<TModel>(typeof(TModel).Name);
        }
    }
}
