using MongoGogo.Connection;

namespace JuboHealth_Model
{
    public class JuboHealthMongoDBContext : GoContext<JuboHealthMongoDBContext>
    {
        public JuboHealthMongoDBContext(string connectionString) : base(connectionString)
        {
        }

        //databases
        [MongoDatabase("Jubo")]
        public class Jubo {}
    }
}
