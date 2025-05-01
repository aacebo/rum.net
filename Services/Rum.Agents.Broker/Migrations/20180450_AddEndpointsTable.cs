using FluentMigrator;

namespace Rum.Agents.Broker.Migrations;

[Migration(20180450)]
public class AddEndpointsTable : Migration
{
    public override void Up()
    {
        Create.Table("endpoints")
            .WithColumn("agent_id").AsGuid().ForeignKey("agents", "id").NotNullable()
            .WithColumn("path").AsString().NotNullable()
            .WithColumn("dialect").AsString().NotNullable()
            .WithColumn("created_at").AsDateTime().NotNullable()
            .WithColumn("updated_at").AsDateTime().NotNullable();

        Create.PrimaryKey()
            .OnTable("endpoints")
            .Columns("agent_id", "path");
    }

    public override void Down()
    {
        Delete.Table("endpoints").IfExists();
    }
}