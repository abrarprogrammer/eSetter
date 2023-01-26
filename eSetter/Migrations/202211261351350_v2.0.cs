using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace eSetter.Migrations
{
    public partial class v20 : DbMigration
    {
        public override void Down()
        {
			Sql("DROP TRIGGGER trg_ins_into_subscriberhist");
		}

        public override void Up()
		{
			String sql = @"CREATE TRIGGER trg_ins_into_subscriberhist ON Subscribers
							AFTER INSERT
								,DELETE
								,UPDATE
							AS
							BEGIN
								SET NOCOUNT ON;

								DECLARE @Action AS CHAR(1);
								DECLARE @Prefix AS NVARCHAR(10);
								DECLARE @Username AS NVARCHAR(10);

								IF NOT EXISTS (
										SELECT *
										FROM INSERTED
										)
								BEGIN
									-- DELETE
									SET @Action = 'DELETE';

									SELECT @Prefix = prefix
										,@Username = username
									FROM DELETED;
								END
								ELSE
								BEGIN
									IF NOT EXISTS (
											SELECT *
											FROM DELETED
											)
									BEGIN
										-- INSERT
										SET @Action = 'INSERT';

										SELECT @Prefix = prefix
											,@Username = username
										FROM INSERTED;
									END
									ELSE
									BEGIN
										-- UPDATE
										SET @Action = 'UPDATE';

										SELECT @Prefix = prefix
											,@Username = username
										FROM INSERTED;
									END
								END

								INSERT INTO SubscriberHists (
									prefix
									,username
									,createdDate
									,STATUS
									)
								VALUES (
									@Prefix
									,@Username
									,GETDATE()
									,@Action
									)
							END";
			Sql(@sql);
		}
    }
}