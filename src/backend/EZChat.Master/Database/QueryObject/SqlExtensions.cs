using SqlKata;
using SqlKata.Compilers;

namespace EZChat.Master.Database.QueryObject
{
    public static class SqlExtensions
    {
        private static readonly Compiler Compiler = new PostgresCompiler();

        public static string CompileQuery(this Query query)
        {
            return Compiler.Compile(query).ToString();
        }
    }
}
