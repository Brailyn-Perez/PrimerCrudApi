namespace CrudApi.Services
{
    public class ConnectionDB
    {
        string connectionString = "Data Source = DESKTOP-G43PN29\\SQLEXPRESS; Initial Catalog = APICRUD; Integrated Security = True";
        public string ConnectionString()
        {
            return connectionString;
        }
    }
}
