using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HCAMiniEHR.Migrations
{
    /// <inheritdoc />
    public partial class AddCreateAppointmentSP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE PROCEDURE [Healthcare].[sp_CreateAppointment]
                    @PatientId int,
                    @AppointmentDate datetime2,
                    @Reason nvarchar(max),
                    @Status nvarchar(50),
                    @DoctorId int = NULL
                AS
                BEGIN
                    INSERT INTO [Healthcare].[Appointment] (PatientId, AppointmentDate, Reason, Status, DoctorId)
                    VALUES (@PatientId, @AppointmentDate, @Reason, @Status, @DoctorId);
                    
                    SELECT CAST(SCOPE_IDENTITY() as int);
                END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE [Healthcare].[sp_CreateAppointment]");
        }
    }
}
