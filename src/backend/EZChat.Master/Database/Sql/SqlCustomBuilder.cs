using System;

namespace EZChat.Master.Database.Sql
{
    public class SqlCustomBuilder : SqlBuilder
    {
        private readonly string _sql;

        public SqlCustomBuilder(string sql)
        {
            if (string.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentNullException(nameof(sql));
            }

            _sql = sql;
        }

        public override string ToSql()
        {
            return _sql;
        }
    }
}
