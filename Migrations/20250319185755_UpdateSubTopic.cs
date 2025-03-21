using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SubjectApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSubTopic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    SubjectID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Subjects__AC1BA388115FC1C8", x => x.SubjectID);
                });

            migrationBuilder.CreateTable(
                name: "SubTopics",
                columns: table => new
                {
                    SubTopicID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectID = table.Column<int>(type: "int", nullable: true),
                    SubTopicName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SubTopic__3EFE32F02059155E", x => x.SubTopicID);
                    table.ForeignKey(
                        name: "FK__SubTopics__Subje__7F2BE32F",
                        column: x => x.SubjectID,
                        principalTable: "Subjects",
                        principalColumn: "SubjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "UQ__Subjects__4C5A7D55ADE81CE5",
                table: "Subjects",
                column: "SubjectName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubTopics_SubjectID",
                table: "SubTopics",
                column: "SubjectID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubTopics");

            migrationBuilder.DropTable(
                name: "Subjects");
        }
    }
}
