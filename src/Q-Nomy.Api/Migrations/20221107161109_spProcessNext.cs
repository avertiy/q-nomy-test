using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QNomy.Api.Migrations
{
    public partial class spProcessNext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
	        var sp = @"CREATE PROCEDURE [dbo].[spProcessNext]
AS
BEGIN
			-- prevent extra result sets from interfering with SELECT statements.
            SET NOCOUNT ON;
			
			-- increment status for client(s) in process
  update [dbo].[Clients]
  set [Status] = 2 
  where [Status] = 1
  
           -- pick client with lowest NumberInLine (under normal flow = 1)   
  update [dbo].[Clients]
  set [Status] = 1,
  [NumberInLine] = 0
  where NumberInLine = (select min(NumberInLine) from [dbo].[Clients] where [Status] = 0)
		   -- decrement number in linet for waiting clients
  update [dbo].[Clients]
  set NumberInLine = NumberInLine-1 
  where [Status] = 0 
END";

	        migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
	        var sql = "DROP PROCEDURE [dbo].[spProcessNext]";
	        migrationBuilder.Sql(sql);
        }
    }
}
