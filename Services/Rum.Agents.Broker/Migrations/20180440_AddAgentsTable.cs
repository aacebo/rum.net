using FluentMigrator;

namespace Rum.Agents.Broker.Migrations;

[Migration(20180440)]
public class AddAgentsTable : Migration
{
    public override void Up()
    {
        Create.Table("agents")
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("version").AsString().NotNullable()
            .WithColumn("name").AsString().Unique().NotNullable()
            .WithColumn("description").AsString().Nullable()
            .WithColumn("url").AsString().NotNullable()
            .WithColumn("documentation_url").AsString().Nullable()
            .WithColumn("created_at").AsDateTime().NotNullable()
            .WithColumn("updated_at").AsDateTime().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("agents").IfExists();
    }
}