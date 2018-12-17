using System;
using System.Collections.Generic;
using System.Reflection;

using Dapper;

using SqlKata;
using SqlKata.Compilers;

namespace EZChat.Master.Database
{
    public static class SqlExtensions
    {
        private static readonly Compiler Compiler = new PostgresCompiler();

        public static string CompileQuery(this Query query)
        {
            return Compiler.Compile(query).ToString();
        }

        public static void MapDapperColumns<T>(Dictionary<string, string> map)
        {
            PropertyInfo Prop(Type type, string col)
            {
                return type.GetProperty(map.ContainsKey(col) ? map[col] : col);
            }

            SqlMapper.SetTypeMap(typeof(T), new CustomPropertyTypeMap(typeof(T), Prop));
        }
    }
}
