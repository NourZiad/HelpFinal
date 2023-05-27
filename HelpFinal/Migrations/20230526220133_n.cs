using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpFinal.Migrations
{
    /// <inheritdoc />
    public partial class n : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RegisterViewModels",
                columns: table => new
                {
                    RegisterViewModelId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StudentId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConfirmPassword = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisterViewModels", x => x.RegisterViewModelId);
                });

            migrationBuilder.CreateTable(
                name: "DisabledViewModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisbilityType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegisterViewModelId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisabledViewModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DisabledViewModels_RegisterViewModels_RegisterViewModelId",
                        column: x => x.RegisterViewModelId,
                        principalTable: "RegisterViewModels",
                        principalColumn: "RegisterViewModelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VolunteerViewModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Skills = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegisterViewModelId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VolunteerViewModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VolunteerViewModels_RegisterViewModels_RegisterViewModelId",
                        column: x => x.RegisterViewModelId,
                        principalTable: "RegisterViewModels",
                        principalColumn: "RegisterViewModelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DisabledViewModels_RegisterViewModelId",
                table: "DisabledViewModels",
                column: "RegisterViewModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerViewModels_RegisterViewModelId",
                table: "VolunteerViewModels",
                column: "RegisterViewModelId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DisabledViewModels");

            migrationBuilder.DropTable(
                name: "VolunteerViewModels");

            migrationBuilder.DropTable(
                name: "RegisterViewModels");
        }
    }
}
