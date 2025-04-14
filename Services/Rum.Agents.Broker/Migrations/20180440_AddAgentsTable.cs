using FluentMigrator;

namespace Rum.Agents.Broker.Migrations;

[Migration(20180430)]
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
            .WithColumn("created_at").AsDateTime().NotNullable().WithDefaultValue(RawSql.Insert("CURRENT_TIMESTAMP"))
            .WithColumn("updated_at").AsDateTime().NotNullable().WithDefaultValue(RawSql.Insert("CURRENT_TIMESTAMP"));
    }

    public override void Down()
    {
        Delete.Table("agents").IfExists();
    }
}