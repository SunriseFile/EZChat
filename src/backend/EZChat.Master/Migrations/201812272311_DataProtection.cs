using FluentMigrator;

namespace EZChat.Master.Migrations
{
    [Migration(2018_12_27__23_11)]
    public class DataProtectionMigration : Migration
    {
        public override void Up()
        {
            Create.Table("data_protection_keys")
                  .WithColumn("id").AsInt32().PrimaryKey().Identity()
                  .WithColumn("name").AsString()
                  .WithColumn("xml").AsString();
        }

        public override void Down()
        {
            Delete.Table("data_protection_keys");
        }
    }
}
