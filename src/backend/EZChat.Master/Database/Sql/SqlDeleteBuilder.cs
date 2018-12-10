using System;
using System.Collections.Generic;
using System.Linq;

namespace EZChat.Master.Database.Sql
{
    public class SqlDeleteBuilder : SqlBuilder
    {
        private readonly string _table;
        private readonly List<SqlCondition> _whereConditions;

        public SqlDeleteBuilder(string table)
        {
            if (string.IsNullOrWhiteSpace(table))
            {
                throw new ArgumentNullException(nameof(table));
            }

            _table = table;
            _whereConditions = new List<SqlCondition>();
        }

        public SqlDeleteBuilder Where(string column, string operation, string value)
        {
            _whereConditions.Add(new SqlCondition(column, operation, value));
            return this;
        }

        public override string ToSql()
        {
            var where = string.Join(" AND ", _whereConditions.Select(x => x.ToString()));

            where = string.IsNullOrWhiteSpace(where) ? string.Empty : $" WHERE {where}";

            var sql = $"DELETE FROM [{_table}]{where}";

            return sql;
        }
    }
}
