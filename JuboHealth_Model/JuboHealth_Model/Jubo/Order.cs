using MongoDB.Bson.Serialization.Attributes;
using MongoGogo.Connection;

namespace JuboHealth_Model.Jubo
{
    [MongoCollection(typeof(JuboHealthMongoDBContext.Jubo),
                     "Order")]
    public class Order
    {
        [BsonId]
        public int Id { get; set; }

        [BsonElement("Message")]
        public string Message { get; set; }
    }
}
