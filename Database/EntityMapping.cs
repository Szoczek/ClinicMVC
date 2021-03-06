﻿using Clinic.Database.Models;
using MongoDB.Bson.Serialization;

namespace Clinic.Database
{
    public static class EntityMapping
    {
        public static void Map()
        {
            BsonClassMap.RegisterClassMap<User>(x =>
            {
                x.AutoMap();
                x.MapIdMember(y => y.Id);
                x.SetIgnoreExtraElements(true);
            });

            BsonClassMap.RegisterClassMap<Patient>(x =>
            {
                x.AutoMap();
                x.MapIdMember(y => y.Id);
                x.SetIgnoreExtraElements(true);
            });

            BsonClassMap.RegisterClassMap<Doctor>(x =>
            {
                x.AutoMap();
                x.MapIdMember(y => y.Id);
                x.SetIgnoreExtraElements(true);
            });

            BsonClassMap.RegisterClassMap<Contract>(x =>
            {
                x.AutoMap();
                x.MapIdMember(y => y.Id);
                x.SetIgnoreExtraElements(true);
            });

            BsonClassMap.RegisterClassMap<Visit>(x =>
            {
                x.AutoMap();
                x.MapIdMember(y => y.Id);
                x.SetIgnoreExtraElements(true);
            });

            BsonClassMap.RegisterClassMap<About>(x =>
            {
                x.AutoMap();
                x.MapIdMember(y => y.Id);
                x.SetIgnoreExtraElements(true);
            });
        }
    }
}