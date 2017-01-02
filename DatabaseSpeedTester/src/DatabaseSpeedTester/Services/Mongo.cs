using DatabaseSpeedTester.ServiceModels;
using MongoDB.Driver;
using System.Linq;

namespace DatabaseSpeedTester.Services
{
    class Mongo
    {
        /// <summary>
        /// Connection and handling of persistence to MongoDB.
        /// 
        /// To change MongoDB edit the connectionstring
        /// To change specific database on MongoDB change the dbName
        /// To cahnge collection on set db change collection
        /// </summary>
        private string connectionString     = "mongodb://ondomongotest2:O9StUXi3ZeAw5pgnSxAv5pyZvasLtmc248sIIoTiQaDwhwUQmlBnQl0BlmMhWsAgZs8OlKSatIRIgk2jiv09Eg==@ondomongotest2.documents.azure.com:10250/?ssl=true";
        private string dbName               = "testDb";
        private string collection           = "clubs";

        /// <summary>
        /// Takes a connection string and returns an instance of a MongoClient
        /// </summary>
        /// <returns></returns>
        private MongoClient connect()
        {
            MongoClient client = new MongoClient(connectionString);

            return client;
        }

        /// <summary>
        /// Retrieve an instance of a specific IMongoDatabase
        /// 
        /// To change the database edit the dbName variable in top
        /// </summary>
        /// <returns></returns>
        private IMongoDatabase connectToDb()
        {
            return connect().GetDatabase(dbName);
        }

        /// <summary>
        /// Retrive specific collection on db, the collection will be created if not existing
        /// 
        /// To change the collection edit the collection variable in top
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public IMongoCollection<ClubEntity> getCollectionClub()
        {
            return connectToDb().GetCollection<ClubEntity>(collection);
        }

        /// <summary>
        /// Retrieve a clubEntity from a ondoID
        /// </summary>
        /// <param name="ondoId"></param>
        /// <returns></returns>
        public ClubEntity findByProp(int ondoId)
        {
            var filter = Builders<ClubEntity>.Filter.Eq("OndoId", ondoId);
            var collection = getCollectionClub().Find(filter);

            return collection.First();
        }
    }
}
