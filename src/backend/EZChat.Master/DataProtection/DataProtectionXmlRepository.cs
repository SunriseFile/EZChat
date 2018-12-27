using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

using Dapper;

using EZChat.Master.Database;

using Microsoft.AspNetCore.DataProtection.Repositories;

using SqlKata;

namespace EZChat.Master.DataProtection
{
    public class DataProtectionXmlRepository : IXmlRepository
    {
        private const string TableName = "data_protection_keys";

        private readonly IDbConnectionFactory _factory;

        public DataProtectionXmlRepository(IDbConnectionFactory factory)
        {
            _factory = factory;
        }

        public IReadOnlyCollection<XElement> GetAllElements()
        {
            var sql = new Query(TableName).CompileQuery();

            using (var connection = _factory.Open())
            {
                return connection.Query(sql)
                                 .Select(x => XElement.Parse((string) x.xml))
                                 .ToArray();
            }
        }

        public void StoreElement(XElement element, string friendlyName)
        {
            var data = new Dictionary<string, object>
            {
                ["name"] = friendlyName,
                ["xml"] = element.ToString(SaveOptions.DisableFormatting)
            };

            var sql = new Query(TableName)
                      .AsInsert(data)
                      .CompileQuery();

            using (var connection = _factory.Open())
            {
                connection.Execute(sql);
            }
        }
    }
}
