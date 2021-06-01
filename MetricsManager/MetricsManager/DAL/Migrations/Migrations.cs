using FluentMigrator;

namespace MetricsManager.DAL.Migrations
{
    [Migration(1, "First Migration")]
    public class FirstMigration : Migration
    {
        public override void Up()
        {
            Create.Table("agents")
                .WithColumn("agentId").AsInt32().PrimaryKey()
                .WithColumn("agentUrl").AsString()
                .WithColumn("enabled").AsBoolean();

            Create.Table("cpumetrics")
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("value").AsInt32()
                .WithColumn("time").AsInt64()
                .WithColumn("agentId").AsInt32().ForeignKey("agents", "agentId");

            Create.Table("dotnetmetrics")
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("value").AsInt32()
                .WithColumn("time").AsInt64()
                .WithColumn("agentId").AsInt32().ForeignKey("agents", "agentId");

            Create.Table("hddmetrics")
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("value").AsInt32()
                .WithColumn("time").AsInt64()
                .WithColumn("agentId").AsInt32().ForeignKey("agents", "agentId");

            Create.Table("networkmetrics")
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("value").AsInt32()
                .WithColumn("time").AsInt64()
                .WithColumn("agentId").AsInt32().ForeignKey("agents", "agentId");

            Create.Table("rammetrics")
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("value").AsInt32()
                .WithColumn("time").AsInt64()
                .WithColumn("agentId").AsInt32().ForeignKey("agents", "agentId");
        }

        public override void Down()
        {
            Delete.Table("cpumetrics");
            Delete.Table("dotnetmetrics");
            Delete.Table("hddmetrics");
            Delete.Table("networkmetrics");
            Delete.Table("rammetrics");
            Delete.Table("agents");
        }
    }
}
