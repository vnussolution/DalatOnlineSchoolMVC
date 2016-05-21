//namespace DalatOnlineSchool1.Migrations
//{
//    using System;
//    using System.Data.Entity.Migrations;
    
//    public partial class inheritance : DbMigration
//    {
//        public override void Up()
//        {
//            // Drop foreign keys and indexes that point to tables we're going to drop.
//            DropForeignKey("dbo.Enrollment", "StudentID", "dbo.Student");
//            DropIndex("dbo.Enrollment", new[] { "StudentID" });
//            RenameTable(name: "dbo.Instructor", newName: "Person");
//            AddColumn("dbo.Person", "EnrollmentDate", c => c.DateTime());
//            AddColumn("dbo.Person", "Discriminator", c => c.String(nullable: false, maxLength: 128, defaultValue: "Instructor"));
//            AlterColumn("dbo.Person", "HireDate", c => c.DateTime());
//            AddColumn("dbo.Person", "OldId", c => c.Int(nullable: true));
//            // Copy existing Student data into new Person table.
//            Sql("INSERT INTO dbo.Person (LastName, FirstName, HireDate, EnrollmentDate, Discriminator, OldId) SELECT LastName, FirstName, null AS HireDate, EnrollmentDate, 'Student' AS Discriminator, ID AS OldId FROM dbo.Student");
//            // Fix up existing relationships to match new PK's.
//            Sql("UPDATE dbo.Enrollment SET StudentId = (SELECT ID FROM dbo.Person WHERE OldId = Enrollment.StudentId AND Discriminator = 'Student')");
//            // Remove temporary key
//            DropColumn("dbo.Person", "OldId");
//            DropTable("dbo.Student");
//            // Re-create foreign keys and indexes pointing to new table.
//            AddForeignKey("dbo.Enrollment", "StudentID", "dbo.Person", "ID", cascadeDelete: true);
//            CreateIndex("dbo.Enrollment", "StudentID");
//        }
        
//        public override void Down()
//        {
//            CreateTable(
//                "dbo.Students",
//                c => new
//                    {
//                        ID = c.Int(nullable: false, identity: true),
//                        LastName = c.String(nullable: false, maxLength: 50),
//                        FirstName = c.String(maxLength: 50),
//                        Email = c.String(),
//                        EnrollmentDate = c.DateTime(nullable: false),
//                        Gender = c.Boolean(nullable: false),
//                        Background = c.String(),
//                    })
//                .PrimaryKey(t => t.ID);
            
//            AlterColumn("dbo.People", "HireDate", c => c.DateTime(nullable: false));
//            AlterColumn("dbo.People", "FirstName", c => c.String(maxLength: 50));
//            AlterColumn("dbo.People", "LastName", c => c.String(maxLength: 50));
//            DropColumn("dbo.People", "Discriminator");
//            DropColumn("dbo.People", "EnrollmentDate");
//            DropColumn("dbo.People", "Email");
//            RenameTable(name: "dbo.People", newName: "Instructors");
//        }
//    }
//}
