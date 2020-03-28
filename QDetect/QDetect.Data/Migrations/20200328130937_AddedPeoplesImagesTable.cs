using Microsoft.EntityFrameworkCore.Migrations;

namespace QDetect.Data.Migrations
{
    public partial class AddedPeoplesImagesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonImage_Images_ImageId",
                table: "PersonImage");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonImage_Persons_PersonId",
                table: "PersonImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonImage",
                table: "PersonImage");

            migrationBuilder.RenameTable(
                name: "PersonImage",
                newName: "PeopleImages");

            migrationBuilder.RenameIndex(
                name: "IX_PersonImage_PersonId",
                table: "PeopleImages",
                newName: "IX_PeopleImages_PersonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PeopleImages",
                table: "PeopleImages",
                columns: new[] { "ImageId", "PersonId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PeopleImages_Images_ImageId",
                table: "PeopleImages",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PeopleImages_Persons_PersonId",
                table: "PeopleImages",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PeopleImages_Images_ImageId",
                table: "PeopleImages");

            migrationBuilder.DropForeignKey(
                name: "FK_PeopleImages_Persons_PersonId",
                table: "PeopleImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PeopleImages",
                table: "PeopleImages");

            migrationBuilder.RenameTable(
                name: "PeopleImages",
                newName: "PersonImage");

            migrationBuilder.RenameIndex(
                name: "IX_PeopleImages_PersonId",
                table: "PersonImage",
                newName: "IX_PersonImage_PersonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonImage",
                table: "PersonImage",
                columns: new[] { "ImageId", "PersonId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PersonImage_Images_ImageId",
                table: "PersonImage",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonImage_Persons_PersonId",
                table: "PersonImage",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
