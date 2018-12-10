using System;

using EZChat.Master.Database.Sql;

using Xunit;

namespace EZChat.Master.Tests.Database.Sql
{
    public class SqlJoinBuilderTests
    {
        [Fact]
        public void ShouldThrowErrorWhenJoinParametersIsEmpty()
        {
            // Given

            // When
            var ex = Assert.Throws<Exception>(() => new SqlJoinBuilder().ToSql());

            // Then
            Assert.Equal("Empty data", ex.Message);
        }

        [Fact]
        public void ShouldJoinBuilders()
        {
            // Given
            var joinBuilder = new SqlJoinBuilder();
            var insertBuilder = new SqlInsertBuilder("table").Values("a", "b");
            var customBuilder = new SqlCustomBuilder("SELECT CAST(SCOPE_IDENTITY() AS BIGINT);;;");
            var selectBuilder = new SqlSelectBuilder("table").Where("a", SqlBuilder.Eq, "b");

            // When
            var sql = joinBuilder.Append(insertBuilder)
                                 .Append(customBuilder)
                                 .Append(selectBuilder)
                                 .ToSql();

            // Then
            Assert.Equal("INSERT INTO [table] (a) VALUES (b); " +
                         "SELECT CAST(SCOPE_IDENTITY() AS BIGINT); " +
                         "SELECT * FROM [table] WHERE [a] = b", sql);
        }
    }
}
