using System;

using EZChat.Master.Database.Sql;

using Xunit;

namespace EZChat.Master.Tests.Database.Sql
{
    public class SqlCustomBuilderTests
    {
        [Fact]
        public void ShouldThrowErrorWhenConstructorParametersIsInvalid()
        {
            // Given

            // When
            var ex = Assert.Throws<ArgumentNullException>(() => new SqlCustomBuilder(null));

            // Then
            Assert.Equal("sql", ex.ParamName);
        }

        [Fact]
        public void ShouldReturnSqlFromConstructor()
        {
            // Given
            var builder = new SqlCustomBuilder("SELECT * FROM [table]");

            // When
            var sql = builder.ToSql();

            // Then
            Assert.Equal("SELECT * FROM [table]", sql);
        }
    }
}
