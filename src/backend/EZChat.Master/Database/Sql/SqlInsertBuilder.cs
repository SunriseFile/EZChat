using System;
using System.Collections.Generic;
using System.Linq;

namespace EZChat.Master.Database.Sql
{
    public class SqlInsertBuilder : SqlBuilder
    {
        private readonly string _table;
        private readonly Dictionary<string, string> _values;

        public SqlInsertBuilder(string table)
        {
            if (string.IsNullOrWhiteSpace(table))
            {
                throw new ArgumentNullException(nameof(table));
            }

            _table = table;
            _values = new Dictionary<string, string>();
        }

        public SqlInsertBuilder Values(string column, string value)
        {
            if (string.IsNullOrWhiteSpace(column))
            {
                throw new ArgumentNullException(nameof(column));
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (_values.ContainsKey(column))
            {
                throw new InvalidOperationException($"Column {column} already exists");
            }

            _values.Add(column, value);

            return this;
        }

        public override string ToSql()
        {
            if (!_values.Any())
            {
                throw new Exception("Empty data");
            }

            var keys = string.Join(", ", _values.Keys);
            var values = string.Join(", ", _values.Values);
            var sql = $"INSERT INTO [{_table}] ({keys}) VALUES ({values})";

            return sql;
        }
    }
}
