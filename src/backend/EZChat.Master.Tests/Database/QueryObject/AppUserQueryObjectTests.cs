using EZChat.Master.Database.QueryObject;
using EZChat.Master.Identity.Models;

using Xunit;

namespace EZChat.Master.Tests.Database.QueryObject
{
    public class AppUserQueryObjectTests
    {
        [Fact]
        public void Insert_should_generate_correct_insert_sql()
        {
            // Given
            const string expected = "INSERT INTO \"users\" (\"username\", \"normalized_username\", \"display_name\", \"password_hash\") " +
                                    "VALUES ('john_doe', 'JOHN_DOE', 'John Doe', 'PASSWORD_HASH');" +
                                    "SELECT lastval()";

            var queryObject = new AppUserQueryObject();
            var user = new AppUser
            {
                UserName = "john_doe",
                NormalizedUserName = "JOHN_DOE",
                DisplayName = "John Doe",
                PasswordHash = "PASSWORD_HASH"
            };

            // When
            var sql = queryObject.Insert(user);

            // Then
            Assert.Equal(expected, sql);
        }

        [Fact]
        public void Update_should_generate_correct_update_sql()
        {
            // Given
            const string expected = "UPDATE \"users\" " +
                                    "SET \"username\" = 'john_doe', " +
                                    /**/"\"normalized_username\" = 'JOHN_DOE', " +
                                    /**/"\"display_name\" = 'John Doe', " +
                                    /**/"\"password_hash\" = 'PASSWORD_HASH' " +
                                    "WHERE \"id\" = 1";

            var queryObject = new AppUserQueryObject();
            var user = new AppUser
            {
                Id = 1,
                UserName = "john_doe",
                NormalizedUserName = "JOHN_DOE",
                DisplayName = "John Doe",
                PasswordHash = "PASSWORD_HASH"
            };

            // When
            var sql = queryObject.Update(user);

            // Then
            Assert.Equal(expected, sql);
        }

        [Fact]
        public void Delete_should_generate_correct_delete_sql()
        {
            // Given
            const string expected = "DELETE FROM \"users\" WHERE \"id\" = 1";

            var queryObject = new AppUserQueryObject();
            var user = new AppUser { Id = 1 };

            // When
            var sql = queryObject.Delete(user);

            // Then
            Assert.Equal(expected, sql);
        }

        [Fact]
        public void ById_should_generate_correct_by_id_sql()
        {
            // Given
            const string expected = "SELECT * FROM \"users\" WHERE \"id\" = 1";

            var queryObject = new AppUserQueryObject();

            // When
            var sql = queryObject.ById(1);

            // Then
            Assert.Equal(expected, sql);
        }

        [Fact]
        public void ByNormalizedUserName_should_generate_correct_by_normalized_username_sql()
        {
            // Given
            const string expected = "SELECT * FROM \"users\" WHERE \"normalized_username\" = 'JOHN_DOE'";

            var queryObject = new AppUserQueryObject();

            // When
            var sql = queryObject.ByNormalizedUserName("JOHN_DOE");

            // Then
            Assert.Equal(expected, sql);
        }
    }
}
