using System.Data.Entity.Migrations;

namespace Time_Clock_Web.Migrations
{
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                {
                    EmployeeId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    FirstName = c.String(unicode: false),
                    LastName = c.String(unicode: false),
                    CardNumber = c.String(unicode: false),
                    Wage = c.Decimal(nullable: false, precision: 18, scale: 2),
                    EmailAddress = c.String(unicode: false),
                    PhoneNumber = c.String(unicode: false),
                    AspIdentity = c.String(unicode: false),
                    Disabled = c.Boolean(nullable: false),
                    Discriminator = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                })
                .PrimaryKey(t => t.EmployeeId);

            CreateTable(
                "dbo.Shifts",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    TimeStampIn = c.DateTime(nullable: false, precision: 0),
                    TimeStampOut = c.DateTime(nullable: false, precision: 0),
                    IsHolidayPay = c.Boolean(nullable: false),
                    EmployeeId = c.String(maxLength: 128, storeType: "nvarchar"),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .Index(t => t.EmployeeId);

            CreateTable(
                "dbo.IncompleteShifts",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    TimeStampIn = c.DateTime(nullable: false, precision: 0),
                    IsHolidayPay = c.Boolean(nullable: false),
                    EmployeeId = c.String(maxLength: 128, storeType: "nvarchar"),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .Index(t => t.EmployeeId);

            CreateTable(
                "dbo.AspNetRoles",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    Name = c.String(nullable: false, maxLength: 196, storeType: "nvarchar"),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");

            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                {
                    UserId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    RoleId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);

            CreateTable(
                "dbo.AspNetUsers",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    Email = c.String(maxLength: 196, storeType: "nvarchar"),
                    EmailConfirmed = c.Boolean(nullable: false),
                    PasswordHash = c.String(unicode: false),
                    SecurityStamp = c.String(unicode: false),
                    PhoneNumber = c.String(unicode: false),
                    PhoneNumberConfirmed = c.Boolean(nullable: false),
                    TwoFactorEnabled = c.Boolean(nullable: false),
                    LockoutEndDateUtc = c.DateTime(precision: 0),
                    LockoutEnabled = c.Boolean(nullable: false),
                    AccessFailedCount = c.Int(nullable: false),
                    UserName = c.String(nullable: false, maxLength: 196, storeType: "nvarchar"),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");

            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    ClaimType = c.String(unicode: false),
                    ClaimValue = c.String(unicode: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                {
                    LoginProvider = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    ProviderKey = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    UserId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

        }

        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.IncompleteShifts", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Shifts", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.IncompleteShifts", new[] { "EmployeeId" });
            DropIndex("dbo.Shifts", new[] { "EmployeeId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.IncompleteShifts");
            DropTable("dbo.Shifts");
            DropTable("dbo.Employees");
        }
    }
}
