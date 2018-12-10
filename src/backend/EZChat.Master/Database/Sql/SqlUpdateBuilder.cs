using System;
using System.Collections.Generic;
using System.Linq;

namespace EZChat.Master.Database.Sql
{
    public class SqlUpdateBuilder : SqlBuilder
    {
        private readonly string _table;
        private readonly Dictionary<string, string> _values;
        private readonly List<SqlCondition> _whereConditions;

        public SqlUpdateBuilder(string table)
        {
            if (string.IsNullOrWhiteSpace(table))
            {
                throw new ArgumentNullException(nameof(table));
            }

            _table = table;
            _values = new Dictionary<string, string>();
            _whereConditions = new List<SqlCondition>();
        }

        public SqlUpdateBuilder Set(string column, string value)
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
                throw new InvalidOperationException($"Column \"{column}\" already exists");
            }

            _values.Add(column, value);

            return this;
        }

        public SqlUpdateBuilder Where(string column, string operation, string value)
        {
            _whereConditions.Add(new SqlCondition(column, operation, value));
            return this;
        }

        public override string ToSql()
        {
            if (!_values.Any())
            {
                throw new Exception("Empty data");
            }

            var values = string.Join(", ", _values.Select(x => $"[{x.Key}] = {x.Value}"));
            var where = string.Join(" AND ", _whereConditions.Select(x => x.ToString()));

            where = string.IsNullOrWhiteSpace(where) ? string.Empty : $" WHERE {where}";

            var sql = $"UPDATE [{_table}] SET {values}{where}";

            return sql;
        }
    }
}
