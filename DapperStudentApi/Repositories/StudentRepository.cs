using Dapper;
using DapperStudentApi.Models;
using Npgsql;
using System.Data;

namespace DapperStudentApi.Repositories
{
    public class StudentRepository
    {
        private readonly IConfiguration _config;
        public StudentRepository(IConfiguration config)
        {
            _config = config;
        }

        private IDbConnection CreateConnection() =>
            new NpgsqlConnection(_config.GetConnectionString("AppDb"));

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            var sql = "Select * from Students";
            using var conn = CreateConnection();
            return await conn.QueryAsync<Student>(sql);
        }
        public async Task<Student?> GetByIdAsync(int id)
        {
            var sql = "Select * from Students where id = @Id";
            using var conn = CreateConnection();
            return await conn.QueryFirstOrDefaultAsync<Student>(sql, new { Id = id });
        }
        public async Task<int> CreateAsync(Student student)
        {
            var sql = "Insert into Students (fullname, age) values (@FullName, @Age)";
            using var conn = CreateConnection();
            return await conn.ExecuteAsync(sql, student);
        }
        public async Task<int> UpdateAsync(Student student)
        {
            var sql = "Update Students set fullname = @FullName, age = @Age where id = @Id";
            using var conn = CreateConnection();
            return await conn.ExecuteAsync(sql, student);
        }
        public async Task<int> DeleteAsync(int id)
        {
            var sql = "Delete from Students where id = @Id";
            using var conn = CreateConnection();
            return await conn.ExecuteAsync(sql, new { Id = id });
        }
    }
}
