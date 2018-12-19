using FluentMigrator;

namespace EZChat.Master.Migrations
{
    [Migration(2018_12_19__16_10)]
    public class InitialMigration : Migration
    {
        public override void Up()
        {
            Create.Table("users")
                  .WithColumn("id").AsInt32().PrimaryKey().Identity()
                  .WithColumn("username").AsString(64).NotNullable().Unique()
                  .WithColumn("normalized_username").AsString(64).NotNullable().Unique()
                  .WithColumn("display_name").AsString(64).NotNullable()
                  .WithColumn("password_hash").AsString().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("users");
        }
    }
}
