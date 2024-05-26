using MongoDB.Bson.Serialization.Attributes;
using MongoGogo.Connection;

namespace JuboHealth_Model.Jubo
{
    [MongoCollection(typeof(JuboHealthMongoDBContext.Jubo),
                     "Patient")]
    public class Patient
    {
        [BsonId]
        public int Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        /// <summary>
        /// 醫囑 id
        /// </summary>
        /// <remarks> foreign to Order collection</remarks>
        [BsonElement("OrderId")]
        public string OrderId { get; set; }
    }
}
