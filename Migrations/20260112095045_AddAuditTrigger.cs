using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HCAMiniEHR.Migrations
{
    /// <inheritdoc />
    public partial class AddAuditTrigger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE TRIGGER [Healthcare].[trg_AppointmentAudit]
                ON [Healthcare].[Appointment]
                AFTER INSERT, UPDATE, DELETE
                AS
                BEGIN
                    SET NOCOUNT ON;
                    DECLARE @Action VARCHAR(10);
                    IF EXISTS(SELECT * FROM inserted)
                    BEGIN
                        IF EXISTS(SELECT * FROM deleted)
                            SET @Action = 'UPDATE';
                        ELSE
                            SET @Action = 'INSERT';
                    END
                    ELSE
                        SET @Action = 'DELETE';

                    INSERT INTO [Healthcare].[AuditLog] (TableName, Action, Timestamp, Details)
                    VALUES ('Appointment', @Action, GETDATE(), 'Appointment changed');
                END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP TRIGGER [Healthcare].[trg_AppointmentAudit]");
        }
    }
}
