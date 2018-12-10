using System;

using EZChat.Master.Database.Sql;

using Xunit;

namespace EZChat.Master.Tests.Database.Sql
{
    public class SqlInsertBuilderTests
    {
        [Fact]
        public void ShouldThrowErrorWhenConstructorParametersIsInvalid()
        {
            // Given

            // When
            var ex = Assert.Throws<ArgumentNullException>(() => new SqlInsertBuilder(null));

            // Then
            Assert.Equal("table", ex.ParamName);
        }

        [Fact]
        public void ShouldThrowErrorWhenMethodParametersIsInvalid()
        {
            // Given

            // When
            var columnNullEx = Assert.Throws<ArgumentNullException>(() => new SqlInsertBuilder("table").Values(null, null));
            var valueNullEx = Assert.Throws<ArgumentNullException>(() => new SqlInsertBuilder("table").Values("a", null));
            var duplicateColumnsEx = Assert.Throws<InvalidOperationException>(() => new SqlInsertBuilder("table")
                                                                                    .Values("a", "a")
                                                                                    .Values("a", "b"));
            var emptyDataEx = Assert.Throws<Exception>(() => new SqlInsertBuilder("table").ToSql());

            // Then
            Assert.Equal("column", columnNullEx.ParamName);
            Assert.Equal("value", valueNullEx.ParamName);
            Assert.Equal("Column \"a\" already exists", duplicateColumnsEx.Message);
            Assert.Equal("Empty data", emptyDataEx.Message);
        }

        [Fact]
        public void ShouldCreateInsertSql()
        {
            // Given
            var builder = new SqlInsertBuilder("table");

            // When
            var sql = builder.Values("a", "aValue")
                             .Values("b", "bValue")
                             .Values("c", "cValue")
                             .ToSql();

            // Then
            Assert.Equal("INSERT INTO [table] (a, b, c) VALUES (aValue, bValue, cValue)", sql);
        }
    }
}
