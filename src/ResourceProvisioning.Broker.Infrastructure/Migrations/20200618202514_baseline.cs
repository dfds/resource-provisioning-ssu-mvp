using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ResourceProvisioning.Broker.Infrastructure.Migrations
{
    public partial class baseline : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Discriminator = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Environment",
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
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Resource",
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
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnvironmentResourceReference",
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
                        principalTable: "Environment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "State",
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
                        principalTable: "Environment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_State_Resource_ResourceRootId",
                        column: x => x.ResourceRootId,
                        principalTable: "Resource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Label",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true),
                    DesiredStateName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Label", x => x.Name);
                    table.ForeignKey(
                        name: "FK_Label_State_DesiredStateName",
                        column: x => x.DesiredStateName,
                        principalTable: "State",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Property",
                columns: table => new
                {
                    Key = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true),
                    DesiredStateName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Property", x => x.Key);
                    table.ForeignKey(
                        name: "FK_Property_State_DesiredStateName",
                        column: x => x.DesiredStateName,
                        principalTable: "State",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Environment_StatusId",
                table: "Environment",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_EnvironmentResourceReference_EnvironmentRootId",
                table: "EnvironmentResourceReference",
                column: "EnvironmentRootId");

            migrationBuilder.CreateIndex(
                name: "IX_Label_DesiredStateName",
                table: "Label",
                column: "DesiredStateName");

            migrationBuilder.CreateIndex(
                name: "IX_Property_DesiredStateName",
                table: "Property",
                column: "DesiredStateName");

            migrationBuilder.CreateIndex(
                name: "IX_Resource_StatusId",
                table: "Resource",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_State_EnvironmentRootId",
                table: "State",
                column: "EnvironmentRootId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_State_ResourceRootId",
                table: "State",
                column: "ResourceRootId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnvironmentResourceReference");

            migrationBuilder.DropTable(
                name: "Label");

            migrationBuilder.DropTable(
                name: "Property");

            migrationBuilder.DropTable(
                name: "State");

            migrationBuilder.DropTable(
                name: "Environment");

            migrationBuilder.DropTable(
                name: "Resource");

            migrationBuilder.DropTable(
                name: "Status");
        }
    }
}
