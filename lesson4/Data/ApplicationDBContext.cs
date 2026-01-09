using Npgsql;
public class ApplicationDBContext
{
    private readonly string connString = "Host=localhost;Port=5432;Database=Lesson2_week5;Username=postgres;Password=1234";
    public NpgsqlConnection Connection() => new NpgsqlConnection(connString);
}