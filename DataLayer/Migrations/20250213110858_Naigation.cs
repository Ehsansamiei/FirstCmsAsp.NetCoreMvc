using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class Naigation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Page_PageGroup_pageGroupGroupId",
                table: "Page");

            migrationBuilder.DropIndex(
                name: "IX_Page_pageGroupGroupId",
                table: "Page");

            migrationBuilder.DropColumn(
                name: "pageGroupGroupId",
                table: "Page");

            migrationBuilder.CreateIndex(
                name: "IX_Page_GroupId",
                table: "Page",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Page_PageGroup_GroupId",
                table: "Page",
                column: "GroupId",
                principalTable: "PageGroup",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Page_PageGroup_GroupId",
                table: "Page");

            migrationBuilder.DropIndex(
                name: "IX_Page_GroupId",
                table: "Page");

            migrationBuilder.AddColumn<int>(
                name: "pageGroupGroupId",
                table: "Page",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Page_pageGroupGroupId",
                table: "Page",
                column: "pageGroupGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Page_PageGroup_pageGroupGroupId",
                table: "Page",
                column: "pageGroupGroupId",
                principalTable: "PageGroup",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
