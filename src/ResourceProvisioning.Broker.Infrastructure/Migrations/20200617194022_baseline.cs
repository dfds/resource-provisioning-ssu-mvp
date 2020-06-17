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

            migrationBuilder.CreateTable(
                name: "State",
                schema: "DomainContext",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    ApiVersion = table.Column<string>(nullable: true),
                    EnvironmentRootId = table.Column<Guid>(nullable: true),
                    ResourceRootId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_State", x => x.Name);
                    table.ForeignKey(
                        name: "FK_State_Environment_EnvironmentRootId",
                        column: x => x.EnvironmentRootId,
                        principalSchema: "DomainContext",
                        principalTable: "Environment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_State_Resource_ResourceRootId",
                        column: x => x.ResourceRootId,
                        principalSchema: "DomainContext",
                        principalTable: "Resource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Label",
                columns: table => new
                {
                    DesiredStateName = table.Column<string>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Label", x => new { x.DesiredStateName, x.Id });
                    table.ForeignKey(
                        name: "FK_Label_State_DesiredStateName",
                        column: x => x.DesiredStateName,
                        principalSchema: "DomainContext",
                        principalTable: "State",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Property",
                columns: table => new
                {
                    DesiredStateName = table.Column<string>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Key = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Property", x => new { x.DesiredStateName, x.Id });
                    table.ForeignKey(
                        name: "FK_Property_State_DesiredStateName",
                        column: x => x.DesiredStateName,
                        principalSchema: "DomainContext",
                        principalTable: "State",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateIndex(
                name: "IX_State_EnvironmentRootId",
                schema: "DomainContext",
                table: "State",
                column: "EnvironmentRootId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_State_ResourceRootId",
                schema: "DomainContext",
                table: "State",
                column: "ResourceRootId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Label");

            migrationBuilder.DropTable(
                name: "Property");

            migrationBuilder.DropTable(
                name: "EnvironmentResourceReference",
                schema: "DomainContext");

            migrationBuilder.DropTable(
                name: "State",
                schema: "DomainContext");

            migrationBuilder.DropTable(
                name: "Environment",
                schema: "DomainContext");

            migrationBuilder.DropTable(
                name: "Resource",
                schema: "DomainContext");

            migrationBuilder.DropTable(
                name: "Status",
                schema: "DomainContext");
        }
    }
}
