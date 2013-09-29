namespace Infrastructure.Data.Migrations
{
    using FluentMigrator;

    [Migration(201330090850)]
    public class CreateShipTable : Migration
    {
        public override void Up()
        {
            Create.Table("Ships")
                .InSchema("dbo")
                .WithColumn("Id")
                .AsGuid()
                .PrimaryKey()
                .WithColumn("CreatedDate")
                .AsCustom("datetime2(7)")
                .NotNullable()
                .WithColumn("ModifiedDate")
                .AsCustom("datetime2(7)")
                .NotNullable()
                .WithColumn("Name")
                .AsString(255)
                .Nullable();
        }

        public override void Down()
        {
            Delete.Table("Ships")
                .InSchema("dbo");
        }
    }
}
