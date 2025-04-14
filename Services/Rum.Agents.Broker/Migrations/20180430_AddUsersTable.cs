using FluentMigrator;

namespace Rum.Agents.Broker.Migrations;

[Migration(20180430)]
public class AddUsersTable : Migration
{
    public override void Up()
    {
        Create.Table("users")
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("name").AsString().NotNullable()
            .WithColumn("created_at").AsDateTime().NotNullable().WithDefaultValue(RawSql.Insert("CURRENT_TIMESTAMP"));
    }

    public override void Down()
    {
        Delete.Table("users").IfExists();
    }
}