namespace EZChat.Master.Database.Sql
{
    public abstract class SqlBuilder
    {
        public const string Eq = "=";
        public const string Lt = "<";
        public const string Lte = "<=";
        public const string Gt = ">";
        public const string Gte = ">=";
        public const string Ne = "<>";
        public const string Like = "LIKE";

        public abstract string ToSql();
    }
}
