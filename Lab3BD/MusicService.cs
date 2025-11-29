using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Lab3BD
{
    public class CustomDatabaseException : Exception
    {
        public CustomDatabaseException(string message) : base(message) { }

        public CustomDatabaseException(string message, Exception inner)
            : base(message, inner) { }
    }

    public class MusicService
    {
        private readonly MusicContext _context;
        public MusicService(MusicContext context) => _context = context;


        public async Task AddNewLabelAsync(int labelId, string name, string country, int foundationYear)
        {
            var sql = "EXEC AddNewLabel @label_id, @name, @country, @foundation_year";
            var parameters = new[]
            {
                new SqlParameter("@label_id", labelId),
                new SqlParameter("@name", name),
                new SqlParameter("@country", country),
                new SqlParameter("@foundation_year", foundationYear)
            };
            await _context.Database.ExecuteSqlRawAsync(sql, parameters);
        }

        public async Task<string> ChangeAlbumLabelAsync(int albumId, string albumTitle, int artistId, int newLabelId)
        {
            var connection = _context.Database.GetDbConnection() as SqlConnection;

            if (connection.State != ConnectionState.Open)
                await connection.OpenAsync();

            using (var cmd = new SqlCommand("ChangeAlbumLabel", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@albumId", albumId);
                cmd.Parameters.AddWithValue("@AlbumTitle", albumTitle);
                cmd.Parameters.AddWithValue("@ArtistId", artistId);
                cmd.Parameters.AddWithValue("@NewLabelId", newLabelId);

                try
                {
                    var result = await cmd.ExecuteScalarAsync();
                    return result?.ToString() ?? "Операцію виконано успішно.";
                }
                catch (SqlException ex)
                {
                    throw;
                }
            }
        }


        public async Task<string> UpdateAlbumTitleAsync(int albumId, string suffix)
        {
            string newTitle = null;

            var connection = _context.Database.GetDbConnection() as SqlConnection;

            if (connection.State != ConnectionState.Open)
                await connection.OpenAsync();

            using (var command = new SqlCommand("UpdateAlbumTitle", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@albumId", albumId);
                command.Parameters.AddWithValue("@suffix", suffix);

                try
                {
                    var result = await command.ExecuteScalarAsync();
                    return result?.ToString() ?? "Операцію виконано успішно.";
                }
                catch (SqlException ex)
                {
                    throw;
                }
            }
        }

        public async Task<int> GetTracksBelowAverageAsync()
        {
            var connection = _context.Database.GetDbConnection() as SqlConnection;
            await connection.OpenAsync();

            using var cmd = new SqlCommand("SELECT dbo.LESS_THAN_AVG()", connection);
            var result = await cmd.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }
        public async Task<int> GetTracksLongerThanAsync(int duration)
        {
            var connection = _context.Database.GetDbConnection() as SqlConnection;
            await connection.OpenAsync();

            using var cmd = new SqlCommand("SELECT dbo.fn_GetTracksLongerThan(@duration)", connection);
            cmd.Parameters.AddWithValue("@duration", duration);

            var result = await cmd.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }
        public async Task<List<object>> GetTopAlbumsByArtistAsync(string artistName)
        {
            var connection = _context.Database.GetDbConnection() as SqlConnection;
            await connection.OpenAsync();

            var list = new List<object>();

            using var cmd = new SqlCommand("SELECT * FROM dbo.GetTopAlbumsByArtist(@artistName)", connection);
            cmd.Parameters.AddWithValue("@artistName", artistName);

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                list.Add(new
                {
                    AlbumTitle = reader["AlbumTitle"].ToString(),
                    TrackCount = Convert.ToInt32(reader["TrackCount"])
                });
            }

            return list;
        }
    }
}
