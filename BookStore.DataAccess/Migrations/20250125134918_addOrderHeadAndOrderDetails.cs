using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addOrderHeadAndOrderDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SOrderDetails_OrderHeaders_OrderHeaderId",
                table: "SOrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_SOrderDetails_Products_ProductId",
                table: "SOrderDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SOrderDetails",
                table: "SOrderDetails");

            migrationBuilder.RenameTable(
                name: "SOrderDetails",
                newName: "OrderDetails");

            migrationBuilder.RenameIndex(
                name: "IX_SOrderDetails_ProductId",
                table: "OrderDetails",
                newName: "IX_OrderDetails_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_SOrderDetails_OrderHeaderId",
                table: "OrderDetails",
                newName: "IX_OrderDetails_OrderHeaderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderDetails",
                table: "OrderDetails",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_OrderHeaders_OrderHeaderId",
                table: "OrderDetails",
                column: "OrderHeaderId",
                principalTable: "OrderHeaders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Products_ProductId",
                table: "OrderDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_OrderHeaders_OrderHeaderId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Products_ProductId",
                table: "OrderDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderDetails",
                table: "OrderDetails");

            migrationBuilder.RenameTable(
                name: "OrderDetails",
                newName: "SOrderDetails");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetails_ProductId",
                table: "SOrderDetails",
                newName: "IX_SOrderDetails_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetails_OrderHeaderId",
                table: "SOrderDetails",
                newName: "IX_SOrderDetails_OrderHeaderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SOrderDetails",
                table: "SOrderDetails",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SOrderDetails_OrderHeaders_OrderHeaderId",
                table: "SOrderDetails",
                column: "OrderHeaderId",
                principalTable: "OrderHeaders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SOrderDetails_Products_ProductId",
                table: "SOrderDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
