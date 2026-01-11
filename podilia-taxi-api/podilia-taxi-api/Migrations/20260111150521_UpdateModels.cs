using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace podilia_taxi_api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EstimatedPrice",
                table: "RideOrders",
                newName: "PickupLongitude");

            migrationBuilder.AddColumn<DateTime>(
                name: "AcceptedAt",
                table: "Rides",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CancellationReason",
                table: "Rides",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CancelledAt",
                table: "Rides",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CancelledByUserId",
                table: "Rides",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Currency",
                table: "Rides",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DriverArrivedAt",
                table: "Rides",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DriverComment",
                table: "Rides",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DriverRating",
                table: "Rides",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "DropoffLatitude",
                table: "Rides",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "DropoffLongitude",
                table: "Rides",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "PassengerComment",
                table: "Rides",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PassengerRating",
                table: "Rides",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "PickupLatitude",
                table: "Rides",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PickupLongitude",
                table: "Rides",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "RideOrderId",
                table: "Rides",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RoutePolyline",
                table: "Rides",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "BaseFare",
                table: "RideOrders",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DriverEarnings",
                table: "RideOrders",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<double>(
                name: "DropoffLatitude",
                table: "RideOrders",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "DropoffLongitude",
                table: "RideOrders",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PickupLatitude",
                table: "RideOrders",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<decimal>(
                name: "PlatformFee",
                table: "RideOrders",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "RideOrders",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PricePerKm",
                table: "RideOrders",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PricePerMinute",
                table: "RideOrders",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SurgeMultiplier",
                table: "RideOrders",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Rides_RideOrderId",
                table: "Rides",
                column: "RideOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rides_RideOrders_RideOrderId",
                table: "Rides",
                column: "RideOrderId",
                principalTable: "RideOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rides_RideOrders_RideOrderId",
                table: "Rides");

            migrationBuilder.DropIndex(
                name: "IX_Rides_RideOrderId",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "AcceptedAt",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "CancellationReason",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "CancelledAt",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "CancelledByUserId",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "DriverArrivedAt",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "DriverComment",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "DriverRating",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "DropoffLatitude",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "DropoffLongitude",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "PassengerComment",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "PassengerRating",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "PickupLatitude",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "PickupLongitude",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "RideOrderId",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "RoutePolyline",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "BaseFare",
                table: "RideOrders");

            migrationBuilder.DropColumn(
                name: "DriverEarnings",
                table: "RideOrders");

            migrationBuilder.DropColumn(
                name: "DropoffLatitude",
                table: "RideOrders");

            migrationBuilder.DropColumn(
                name: "DropoffLongitude",
                table: "RideOrders");

            migrationBuilder.DropColumn(
                name: "PickupLatitude",
                table: "RideOrders");

            migrationBuilder.DropColumn(
                name: "PlatformFee",
                table: "RideOrders");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "RideOrders");

            migrationBuilder.DropColumn(
                name: "PricePerKm",
                table: "RideOrders");

            migrationBuilder.DropColumn(
                name: "PricePerMinute",
                table: "RideOrders");

            migrationBuilder.DropColumn(
                name: "SurgeMultiplier",
                table: "RideOrders");

            migrationBuilder.RenameColumn(
                name: "PickupLongitude",
                table: "RideOrders",
                newName: "EstimatedPrice");
        }
    }
}
