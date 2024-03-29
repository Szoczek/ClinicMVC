﻿using Clinic.Database.Models;
using Clinic.Settings;
using MongoDB.Driver;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Threading.Tasks;

namespace Clinic.Database
{
    public class MongoDbContext : IDisposable
    {
        private readonly MongoClient mongoClient;
        private readonly IMongoDatabase mongoDatabase;
        private IClientSessionHandle mongoSession;
        public MongoDbContext(SettingsManager settings)
        {
            MongoDefaults.GuidRepresentation = MongoDB.Bson.GuidRepresentation.Standard;
            var dbSettings = MongoClientSettings.FromConnectionString(settings.DatabaseSettings.ConnectionString);

            mongoClient = new MongoClient(dbSettings);
            mongoDatabase = mongoClient.GetDatabase(settings.DatabaseSettings.DatabaseName);
            StartSession();
        }

        public IMongoCollection<T> DbSet<T>() where T : BaseModel
        {
            var tableName = typeof(T).GetCustomAttribute<TableAttribute>(false).Name;
            return mongoDatabase.GetCollection<T>(tableName);
        }
        private async Task StartSession()
        {
            mongoSession = await mongoClient.StartSessionAsync();
            mongoSession.StartTransaction();
        }
        public void CommitTransaction()
        {
            mongoSession.CommitTransaction();
        }

        public async Task CommitTransactionAsync()
        {
            await mongoSession.CommitTransactionAsync();
        }

        public void Dispose()
        {
            mongoSession.Dispose();
        }
    }
}
