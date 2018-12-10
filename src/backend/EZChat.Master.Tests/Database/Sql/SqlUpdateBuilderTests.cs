using System;

using EZChat.Master.Database.Sql;

using Xunit;

namespace EZChat.Master.Tests.Database.Sql
{
    public class SqlUpdateBuilderTests
    {
        [Fact]
        public void ShouldThrowErrorWhenConstructorParametersIsInvalid()
        {
            // Given

            // When
            var ex = Assert.Throws<ArgumentNullException>(() => new SqlUpdateBuilder(null));

            // Then
            Assert.Equal("table", ex.ParamName);
        }

        [Fact]
        public void ShouldThrowErrorWhenMethodParametersIsInvalid()
        {
            // Given

            // When
            var columnNullEx = Assert.Throws<ArgumentNullException>(() => new SqlUpdateBuilder("table").Set(null, null));
            var valueNullEx = Assert.Throws<ArgumentNullException>(() => new SqlUpdateBuilder("table").Set("a", null));
            var duplicateColumnsEx = Assert.Throws<InvalidOperationException>(() => new SqlUpdateBuilder("table").Set("a", "a").Set("a", "b"));
            var emptyDataEx = Assert.Throws<Exception>(() => new SqlUpdateBuilder("table").ToSql());

            // Then
            Assert.Equal("column", columnNullEx.ParamName);
            Assert.Equal("value", valueNullEx.ParamName);
            Assert.Equal("Column \"a\" already exists", duplicateColumnsEx.Message);
            Assert.Equal("Empty data", emptyDataEx.Message);
        }

        [Fact]
        public void ShouldCreateUpdateSql()
        {
            // Given

            // When
            var simpleUpdate = new SqlUpdateBuilder("table")
                               .Set("a", "b")
                               .ToSql();

            var multiplyParameters = new SqlUpdateBuilder("table")
                                     .Set("a", "aValue")
                                     .Set("b", "bValue")
                                     .ToSql();

            var whereCond = new SqlUpdateBuilder("table")
                            .Set("a", "aValue")
                            .Set("b", "bValue")
                            .Where("a", SqlBuilder.Eq, "oldAValue")
                            .ToSql();

            var multiplyWhereCond = new SqlUpdateBuilder("table")
                                    .Set("a", "aValue")
                                    .Set("b", "bValue")
                                    .Where("a", SqlBuilder.Eq, "oldAValue")
                                    .Where("b", SqlBuilder.Eq, "oldBValue")
                                    .ToSql();

            // Then
            Assert.Equal("UPDATE [table] SET [a] = b", simpleUpdate);
            Assert.Equal("UPDATE [table] SET [a] = aValue, [b] = bValue", multiplyParameters);
            Assert.Equal("UPDATE [table] SET [a] = aValue, [b] = bValue WHERE [a] = oldAValue", whereCond);
            Assert.Equal("UPDATE [table] SET [a] = aValue, [b] = bValue WHERE [a] = oldAValue AND [b] = oldBValue", multiplyWhereCond);
        }
    }
}
