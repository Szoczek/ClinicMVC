using Database.Models;
using MongoDB.Bson.Serialization;

namespace Database
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
        }
    }
}