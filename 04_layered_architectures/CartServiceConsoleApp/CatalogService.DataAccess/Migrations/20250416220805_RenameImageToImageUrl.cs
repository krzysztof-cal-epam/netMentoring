﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatalogService.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RenameImageToImageUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Categories",
                newName: "ImageUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Categories",
                newName: "Image");
        }
    }
}
