using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ResourceProvisioning.Broker.Infrastructure.Migrations
{
    public partial class baseline : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "DomainContext");

            migrationBuilder.CreateTable(
                name: "Request",
                schema: "DomainContext",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Request", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                schema: "DomainContext",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValue: 1),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Discriminator = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Environment",
                schema: "DomainContext",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DesiredState_Name = table.Column<string>(nullable: true),
                    DesiredState_ApiVersion = table.Column<string>(nullable: true),
                    StatusId = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Environment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Environment_Status_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "DomainContext",
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Resource",
                schema: "DomainContext",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    StatusId = table.Column<int>(nullable: false),
                    DesiredState_Name = table.Column<string>(nullable: true),
                    DesiredState_ApiVersion = table.Column<string>(nullable: true),
                    RegisteredDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resource", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resource_Status_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "DomainContext",
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnvironmentResourceReference",
                schema: "DomainContext",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ResourceId = table.Column<Guid>(nullable: false),
                    Provisioned = table.Column<DateTime>(nullable: false),
                    Comment = table.Column<string>(nullable: false),
                    EnvironmentId = table.Column<Guid>(nullable: false),
                    EnvironmentRootId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnvironmentResourceReference", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnvironmentResourceReference_Environment_EnvironmentRootId",
                        column: x => x.EnvironmentRootId,
                        principalSchema: "DomainContext",
                        principalTable: "Environment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Environment_StatusId",
                schema: "DomainContext",
                table: "Environment",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_EnvironmentResourceReference_EnvironmentRootId",
                schema: "DomainContext",
                table: "EnvironmentResourceReference",
                column: "EnvironmentRootId");

            migrationBuilder.CreateIndex(
                name: "IX_Resource_StatusId",
                schema: "DomainContext",
                table: "Resource",
                column: "StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnvironmentResourceReference",
                schema: "DomainContext");

            migrationBuilder.DropTable(
                name: "Request",
                schema: "DomainContext");

            migrationBuilder.DropTable(
                name: "Resource",
                schema: "DomainContext");

            migrationBuilder.DropTable(
                name: "Environment",
                schema: "DomainContext");

            migrationBuilder.DropTable(
                name: "Status",
                schema: "DomainContext");
        }
    }
}
