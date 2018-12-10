using System;

using EZChat.Master.Database.Sql;

using Xunit;

namespace EZChat.Master.Tests.Database.Sql
{
    public class SqlSelectBuilderTests
    {
        [Fact]
        public void ShouldThrowErrorWhenConstructorParametersIsInvalid()
        {
            // Given

            // When
            var ex = Assert.Throws<ArgumentNullException>(() => new SqlSelectBuilder(null));

            // Then
            Assert.Equal("table", ex.ParamName);
        }

        [Fact]
        public void ShouldCreateSqlWithoutWhereConditions()
        {
            // Given
            var builder = new SqlSelectBuilder("table");

            // When
            var sql = builder.ToSql();

            // Then
            Assert.Equal("SELECT * FROM [table]", sql);
        }

        [Fact]
        public void ShouldCreateSqlWithWhereConditions()
        {
            // Given
            var builder1 = new SqlSelectBuilder("table").Where("col1", SqlBuilder.Eq, "val1");
            var builder2 = new SqlSelectBuilder("table")
                           .Where("col1", SqlBuilder.Eq, "val1")
                           .Where("col2", SqlBuilder.Lte, "val2");
            var builder3 = new SqlSelectBuilder("table")
                           .Where("col1", SqlBuilder.Eq, "val1")
                           .Where("col2", SqlBuilder.Lte, "val2")
                           .Where("col3", SqlBuilder.Gt, "val3");

            // When
            var sql1 = builder1.ToSql();
            var sql2 = builder2.ToSql();
            var sql3 = builder3.ToSql();

            // Then
            Assert.Equal("SELECT * FROM [table] WHERE [col1] = val1", sql1);
            Assert.Equal("SELECT * FROM [table] WHERE [col1] = val1 AND [col2] <= val2", sql2);
            Assert.Equal("SELECT * FROM [table] WHERE [col1] = val1 AND [col2] <= val2 AND [col3] > val3", sql3);
        }
    }
}
