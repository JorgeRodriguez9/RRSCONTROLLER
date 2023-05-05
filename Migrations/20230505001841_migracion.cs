using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RRSCONTROLLER.Migrations
{
    /// <inheritdoc />
    public partial class migracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CATEGORYS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CATEGORYS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ROLES",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_Role = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROLES", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SUPPLIERS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SUPPLIERS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UNITS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_Unit = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UNITS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "USERS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_User = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Id_Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_USERS_ROLES_Id_Role",
                        column: x => x.Id_Role,
                        principalTable: "ROLES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ADMIN_PAEs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Document = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Last_Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Adress = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Id_User = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADMIN_PAEs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ADMIN_PAEs_USERS_Id_User",
                        column: x => x.Id_User,
                        principalTable: "USERS",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "MANAGER_PAEs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Document = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Last_Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Adress = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Id_User = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MANAGER_PAEs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MANAGER_PAEs_USERS_Id_User",
                        column: x => x.Id_User,
                        principalTable: "USERS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NUTRITIONITS_PAEs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Document = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Last_Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Adress = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Id_User = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NUTRITIONITS_PAEs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_NUTRITIONITS_PAEs_USERS_Id_User",
                        column: x => x.Id_User,
                        principalTable: "USERS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PRODUCTS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Id_Supplier = table.Column<int>(type: "int", nullable: false),
                    Id_Unit = table.Column<int>(type: "int", nullable: false),
                    Id_Admin_Pae = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUCTS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PRODUCTS_ADMIN_PAEs_Id_Admin_Pae",
                        column: x => x.Id_Admin_Pae,
                        principalTable: "ADMIN_PAEs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PRODUCTS_SUPPLIERS_Id_Supplier",
                        column: x => x.Id_Supplier,
                        principalTable: "SUPPLIERS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PRODUCTS_UNITS_Id_Unit",
                        column: x => x.Id_Unit,
                        principalTable: "UNITS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "INSTITUTIONS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Adress = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Phone_Number = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Id_Manager = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INSTITUTIONS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_INSTITUTIONS_MANAGER_PAEs_Id_Manager",
                        column: x => x.Id_Manager,
                        principalTable: "MANAGER_PAEs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FOODS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Id_Nutritionits_Pae = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FOODS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FOODS_NUTRITIONITS_PAEs_Id_Nutritionits_Pae",
                        column: x => x.Id_Nutritionits_Pae,
                        principalTable: "NUTRITIONITS_PAEs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MENUS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Id_Category = table.Column<int>(type: "int", nullable: false),
                    Id_Nutritionits_Pae = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MENUS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MENUS_CATEGORYS_Id_Category",
                        column: x => x.Id_Category,
                        principalTable: "CATEGORYS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MENUS_NUTRITIONITS_PAEs_Id_Nutritionits_Pae",
                        column: x => x.Id_Nutritionits_Pae,
                        principalTable: "NUTRITIONITS_PAEs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NUTRITIONITS_INTSs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Document = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Last_Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Adress = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Id_User = table.Column<int>(type: "int", nullable: false),
                    Id_Institution = table.Column<int>(type: "int", nullable: false),
                    Id_Admin_Pae = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NUTRITIONITS_INTSs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_NUTRITIONITS_INTSs_ADMIN_PAEs_Id_Admin_Pae",
                        column: x => x.Id_Admin_Pae,
                        principalTable: "ADMIN_PAEs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NUTRITIONITS_INTSs_INSTITUTIONS_Id_Institution",
                        column: x => x.Id_Institution,
                        principalTable: "INSTITUTIONS",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_NUTRITIONITS_INTSs_USERS_Id_User",
                        column: x => x.Id_User,
                        principalTable: "USERS",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "SECRETARY_INTSs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Document = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Last_Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Adress = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Id_User = table.Column<int>(type: "int", nullable: false),
                    Id_Institution = table.Column<int>(type: "int", nullable: false),
                    Id_Admin_Pae = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SECRETARY_INTSs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SECRETARY_INTSs_ADMIN_PAEs_Id_Admin_Pae",
                        column: x => x.Id_Admin_Pae,
                        principalTable: "ADMIN_PAEs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SECRETARY_INTSs_INSTITUTIONS_Id_Institution",
                        column: x => x.Id_Institution,
                        principalTable: "INSTITUTIONS",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_SECRETARY_INTSs_USERS_Id_User",
                        column: x => x.Id_User,
                        principalTable: "USERS",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "FOOD_PRODUCTS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Id_Food = table.Column<int>(type: "int", nullable: false),
                    Id_Product = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FOOD_PRODUCTS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FOOD_PRODUCTS_FOODS_Id_Food",
                        column: x => x.Id_Food,
                        principalTable: "FOODS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FOOD_PRODUCTS_PRODUCTS_Id_Product",
                        column: x => x.Id_Product,
                        principalTable: "PRODUCTS",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "MENU_FOODS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Menu = table.Column<int>(type: "int", nullable: false),
                    Id_Food = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MENU_FOODS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MENU_FOODS_FOODS_Id_Food",
                        column: x => x.Id_Food,
                        principalTable: "FOODS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MENU_FOODS_MENUS_Id_Menu",
                        column: x => x.Id_Menu,
                        principalTable: "MENUS",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "REQUESTS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "Date", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Desired_Delivery_Date = table.Column<DateTime>(type: "Date", nullable: false),
                    Id_Nutritionits_Ints = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REQUESTS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_REQUESTS_NUTRITIONITS_INTSs_Id_Nutritionits_Ints",
                        column: x => x.Id_Nutritionits_Ints,
                        principalTable: "NUTRITIONITS_INTSs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "REQUEST_MENUS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Id_Request = table.Column<int>(type: "int", nullable: false),
                    Id_Menu = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REQUEST_MENUS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_REQUEST_MENUS_MENUS_Id_Menu",
                        column: x => x.Id_Menu,
                        principalTable: "MENUS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_REQUEST_MENUS_REQUESTS_Id_Request",
                        column: x => x.Id_Request,
                        principalTable: "REQUESTS",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "SHIPMENTS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "Date", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Transport = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id_Request = table.Column<int>(type: "int", nullable: false),
                    Id_Admin_Pae = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SHIPMENTS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SHIPMENTS_ADMIN_PAEs_Id_Admin_Pae",
                        column: x => x.Id_Admin_Pae,
                        principalTable: "ADMIN_PAEs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SHIPMENTS_REQUESTS_Id_Request",
                        column: x => x.Id_Request,
                        principalTable: "REQUESTS",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "EVALUATIONS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Received = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Correct_Quantity = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Date_Received = table.Column<DateTime>(type: "Date", nullable: false),
                    Id_Shipment = table.Column<int>(type: "int", nullable: false),
                    Id_Secretary_Inst = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EVALUATIONS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EVALUATIONS_SECRETARY_INTSs_Id_Secretary_Inst",
                        column: x => x.Id_Secretary_Inst,
                        principalTable: "SECRETARY_INTSs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EVALUATIONS_SHIPMENTS_Id_Shipment",
                        column: x => x.Id_Shipment,
                        principalTable: "SHIPMENTS",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ADMIN_PAEs_Id_User",
                table: "ADMIN_PAEs",
                column: "Id_User",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EVALUATIONS_Id_Secretary_Inst",
                table: "EVALUATIONS",
                column: "Id_Secretary_Inst");

            migrationBuilder.CreateIndex(
                name: "IX_EVALUATIONS_Id_Shipment",
                table: "EVALUATIONS",
                column: "Id_Shipment",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FOOD_PRODUCTS_Id_Food",
                table: "FOOD_PRODUCTS",
                column: "Id_Food");

            migrationBuilder.CreateIndex(
                name: "IX_FOOD_PRODUCTS_Id_Product",
                table: "FOOD_PRODUCTS",
                column: "Id_Product");

            migrationBuilder.CreateIndex(
                name: "IX_FOODS_Id_Nutritionits_Pae",
                table: "FOODS",
                column: "Id_Nutritionits_Pae");

            migrationBuilder.CreateIndex(
                name: "IX_INSTITUTIONS_Id_Manager",
                table: "INSTITUTIONS",
                column: "Id_Manager");

            migrationBuilder.CreateIndex(
                name: "IX_MANAGER_PAEs_Id_User",
                table: "MANAGER_PAEs",
                column: "Id_User",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MENU_FOODS_Id_Food",
                table: "MENU_FOODS",
                column: "Id_Food");

            migrationBuilder.CreateIndex(
                name: "IX_MENU_FOODS_Id_Menu",
                table: "MENU_FOODS",
                column: "Id_Menu");

            migrationBuilder.CreateIndex(
                name: "IX_MENUS_Id_Category",
                table: "MENUS",
                column: "Id_Category");

            migrationBuilder.CreateIndex(
                name: "IX_MENUS_Id_Nutritionits_Pae",
                table: "MENUS",
                column: "Id_Nutritionits_Pae");

            migrationBuilder.CreateIndex(
                name: "IX_NUTRITIONITS_INTSs_Id_Admin_Pae",
                table: "NUTRITIONITS_INTSs",
                column: "Id_Admin_Pae");

            migrationBuilder.CreateIndex(
                name: "IX_NUTRITIONITS_INTSs_Id_Institution",
                table: "NUTRITIONITS_INTSs",
                column: "Id_Institution");

            migrationBuilder.CreateIndex(
                name: "IX_NUTRITIONITS_INTSs_Id_User",
                table: "NUTRITIONITS_INTSs",
                column: "Id_User",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NUTRITIONITS_PAEs_Id_User",
                table: "NUTRITIONITS_PAEs",
                column: "Id_User",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCTS_Id_Admin_Pae",
                table: "PRODUCTS",
                column: "Id_Admin_Pae");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCTS_Id_Supplier",
                table: "PRODUCTS",
                column: "Id_Supplier");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCTS_Id_Unit",
                table: "PRODUCTS",
                column: "Id_Unit");

            migrationBuilder.CreateIndex(
                name: "IX_REQUEST_MENUS_Id_Menu",
                table: "REQUEST_MENUS",
                column: "Id_Menu");

            migrationBuilder.CreateIndex(
                name: "IX_REQUEST_MENUS_Id_Request",
                table: "REQUEST_MENUS",
                column: "Id_Request");

            migrationBuilder.CreateIndex(
                name: "IX_REQUESTS_Id_Nutritionits_Ints",
                table: "REQUESTS",
                column: "Id_Nutritionits_Ints");

            migrationBuilder.CreateIndex(
                name: "IX_SECRETARY_INTSs_Id_Admin_Pae",
                table: "SECRETARY_INTSs",
                column: "Id_Admin_Pae");

            migrationBuilder.CreateIndex(
                name: "IX_SECRETARY_INTSs_Id_Institution",
                table: "SECRETARY_INTSs",
                column: "Id_Institution");

            migrationBuilder.CreateIndex(
                name: "IX_SECRETARY_INTSs_Id_User",
                table: "SECRETARY_INTSs",
                column: "Id_User",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SHIPMENTS_Id_Admin_Pae",
                table: "SHIPMENTS",
                column: "Id_Admin_Pae");

            migrationBuilder.CreateIndex(
                name: "IX_SHIPMENTS_Id_Request",
                table: "SHIPMENTS",
                column: "Id_Request",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_USERS_Id_Role",
                table: "USERS",
                column: "Id_Role");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EVALUATIONS");

            migrationBuilder.DropTable(
                name: "FOOD_PRODUCTS");

            migrationBuilder.DropTable(
                name: "MENU_FOODS");

            migrationBuilder.DropTable(
                name: "REQUEST_MENUS");

            migrationBuilder.DropTable(
                name: "SECRETARY_INTSs");

            migrationBuilder.DropTable(
                name: "SHIPMENTS");

            migrationBuilder.DropTable(
                name: "PRODUCTS");

            migrationBuilder.DropTable(
                name: "FOODS");

            migrationBuilder.DropTable(
                name: "MENUS");

            migrationBuilder.DropTable(
                name: "REQUESTS");

            migrationBuilder.DropTable(
                name: "SUPPLIERS");

            migrationBuilder.DropTable(
                name: "UNITS");

            migrationBuilder.DropTable(
                name: "CATEGORYS");

            migrationBuilder.DropTable(
                name: "NUTRITIONITS_PAEs");

            migrationBuilder.DropTable(
                name: "NUTRITIONITS_INTSs");

            migrationBuilder.DropTable(
                name: "ADMIN_PAEs");

            migrationBuilder.DropTable(
                name: "INSTITUTIONS");

            migrationBuilder.DropTable(
                name: "MANAGER_PAEs");

            migrationBuilder.DropTable(
                name: "USERS");

            migrationBuilder.DropTable(
                name: "ROLES");
        }
    }
}
