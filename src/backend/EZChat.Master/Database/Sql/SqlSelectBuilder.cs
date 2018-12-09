using System;
using System.Collections.Generic;
using System.Linq;

namespace EZChat.Master.Database.Sql
{
    public class SqlSelectBuilder : SqlBuilder
    {
        private readonly string _table;
        private readonly List<SqlCondition> _whereConditions;

        public SqlSelectBuilder(string table)
        {
            if (string.IsNullOrWhiteSpace(table))
            {
                throw new ArgumentNullException(nameof(table));
            }

            _table = table;
            _whereConditions = new List<SqlCondition>();
        }

        public SqlSelectBuilder Where(string column, string operation, string value)
        {
            _whereConditions.Add(new SqlCondition(column, operation, value));
            return this;
        }

        public override string ToSql()
        {
            var where = string.Join(" AND ", _whereConditions.Select(x => x.ToString()));
            var sql = $"SELECT * FROM [{_table}]" + (string.IsNullOrWhiteSpace(where) ? string.Empty : $" {where}");

            return sql;
        }
    }
}
