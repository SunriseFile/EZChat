using System;

namespace EZChat.Master.Database.Sql
{
    public class SqlCondition
    {
        private readonly string _column;
        private readonly string _operation;
        private readonly string _value;

        public SqlCondition(string column, string operation, string value)
        {
            if (string.IsNullOrWhiteSpace(column))
            {
                throw new ArgumentNullException(nameof(column));
            }

            if (string.IsNullOrWhiteSpace(operation))
            {
                throw new ArgumentNullException(nameof(operation));
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            _column = column;
            _operation = operation;
            _value = value;
        }

        public override string ToString()
        {
            return $"[{_column}] {_operation} {_value}";
        }
    }
}
