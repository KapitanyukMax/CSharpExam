using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddingNewEntitiesAndFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatUser_Users_UsersCredentialsId",
                table: "ChatUser");

            migrationBuilder.RenameColumn(
                name: "UsersCredentialsId",
                table: "ChatUser",
                newName: "MembersCredentialsId");

            migrationBuilder.RenameIndex(
                name: "IX_ChatUser_UsersCredentialsId",
                table: "ChatUser",
                newName: "IX_ChatUser_MembersCredentialsId");

            migrationBuilder.AddColumn<string>(
                name: "IPAddress",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "SendingTime",
                table: "Messages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Login",
                table: "Credentials",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MailAddress",
                table: "Credentials",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Credentials",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Credentials",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Credentials",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatUser_Users_MembersCredentialsId",
                table: "ChatUser",
                column: "MembersCredentialsId",
                principalTable: "Users",
                principalColumn: "CredentialsId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatUser_Users_MembersCredentialsId",
                table: "ChatUser");

            migrationBuilder.DropColumn(
                name: "IPAddress",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SendingTime",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Login",
                table: "Credentials");

            migrationBuilder.DropColumn(
                name: "MailAddress",
                table: "Credentials");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Credentials");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Credentials");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Credentials");

            migrationBuilder.RenameColumn(
                name: "MembersCredentialsId",
                table: "ChatUser",
                newName: "UsersCredentialsId");

            migrationBuilder.RenameIndex(
                name: "IX_ChatUser_MembersCredentialsId",
                table: "ChatUser",
                newName: "IX_ChatUser_UsersCredentialsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatUser_Users_UsersCredentialsId",
                table: "ChatUser",
                column: "UsersCredentialsId",
                principalTable: "Users",
                principalColumn: "CredentialsId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
