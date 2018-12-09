using System;
using System.Collections.Generic;
using System.Linq;

namespace EZChat.Master.Database.Sql
{
    public class SqlJoinBuilder : SqlBuilder
    {
        private readonly List<SqlBuilder> _builders;

        public SqlJoinBuilder()
        {
            _builders = new List<SqlBuilder>();
        }

        public SqlJoinBuilder Append(SqlBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            _builders.Add(builder);

            return this;
        }

        public override string ToSql()
        {
            if (!_builders.Any())
            {
                throw new Exception("Empty data");
            }

            var sql = _builders.Select(b => b.ToSql().TrimEnd(';'));

            return string.Join("; ", sql);
        }
    }
}
