namespace SurveyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Candidates",
                c => new
                    {
                        CandidateId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.CandidateId);
            
            CreateTable(
                "dbo.SurveyAnswers",
                c => new
                    {
                        SurveyId = c.Int(nullable: false),
                        QuestionId = c.Int(nullable: false),
                        CandidateId = c.Int(nullable: false),
                        Answer = c.String(),
                    })
                .PrimaryKey(t => new { t.SurveyId, t.QuestionId, t.CandidateId })
                .ForeignKey("dbo.Candidates", t => t.CandidateId, cascadeDelete: true)
                .ForeignKey("dbo.Surveys", t => t.SurveyId, cascadeDelete: true)
                .Index(t => t.SurveyId)
                .Index(t => t.CandidateId);
            
            CreateTable(
                "dbo.Surveys",
                c => new
                    {
                        SurveyId = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        SurveyDesc = c.String(),
                        OrgId = c.Int(),
                    })
                .PrimaryKey(t => t.SurveyId)
                .ForeignKey("dbo.Organizations", t => t.OrgId)
                .Index(t => t.OrgId);
            
            CreateTable(
                "dbo.Organizations",
                c => new
                    {
                        OrgId = c.Int(nullable: false, identity: true),
                        OrgName = c.String(),
                        AdminId = c.Int(),
                    })
                .PrimaryKey(t => t.OrgId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.QuestionBanks",
                c => new
                    {
                        QuestionId = c.Int(nullable: false, identity: true),
                        Question = c.String(),
                        Type = c.String(),
                        CategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.QuestionId)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.SurveyCandidates",
                c => new
                    {
                        SurveyId = c.Int(nullable: false),
                        CandidateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SurveyId, t.CandidateId })
                .ForeignKey("dbo.Candidates", t => t.CandidateId, cascadeDelete: true)
                .ForeignKey("dbo.Surveys", t => t.SurveyId, cascadeDelete: true)
                .Index(t => t.SurveyId)
                .Index(t => t.CandidateId);
            
            CreateTable(
                "dbo.SurveyQuestions",
                c => new
                    {
                        SurveyId = c.Int(nullable: false),
                        QuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SurveyId, t.QuestionId });
            
            CreateTable(
                "dbo.UserQuestions",
                c => new
                    {
                        UQuestionId = c.Int(nullable: false, identity: true),
                        Question = c.String(),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.UQuestionId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.SurveyCandidate1",
                c => new
                    {
                        Survey_SurveyId = c.Int(nullable: false),
                        Candidate_CandidateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Survey_SurveyId, t.Candidate_CandidateId })
                .ForeignKey("dbo.Surveys", t => t.Survey_SurveyId, cascadeDelete: true)
                .ForeignKey("dbo.Candidates", t => t.Candidate_CandidateId, cascadeDelete: true)
                .Index(t => t.Survey_SurveyId)
                .Index(t => t.Candidate_CandidateId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.SurveyCandidates", "SurveyId", "dbo.Surveys");
            DropForeignKey("dbo.SurveyCandidates", "CandidateId", "dbo.Candidates");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.QuestionBanks", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.SurveyAnswers", "SurveyId", "dbo.Surveys");
            DropForeignKey("dbo.Surveys", "OrgId", "dbo.Organizations");
            DropForeignKey("dbo.SurveyCandidate1", "Candidate_CandidateId", "dbo.Candidates");
            DropForeignKey("dbo.SurveyCandidate1", "Survey_SurveyId", "dbo.Surveys");
            DropForeignKey("dbo.SurveyAnswers", "CandidateId", "dbo.Candidates");
            DropIndex("dbo.SurveyCandidate1", new[] { "Candidate_CandidateId" });
            DropIndex("dbo.SurveyCandidate1", new[] { "Survey_SurveyId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.SurveyCandidates", new[] { "CandidateId" });
            DropIndex("dbo.SurveyCandidates", new[] { "SurveyId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.QuestionBanks", new[] { "CategoryId" });
            DropIndex("dbo.Surveys", new[] { "OrgId" });
            DropIndex("dbo.SurveyAnswers", new[] { "CandidateId" });
            DropIndex("dbo.SurveyAnswers", new[] { "SurveyId" });
            DropTable("dbo.SurveyCandidate1");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.UserQuestions");
            DropTable("dbo.SurveyQuestions");
            DropTable("dbo.SurveyCandidates");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.QuestionBanks");
            DropTable("dbo.Categories");
            DropTable("dbo.Organizations");
            DropTable("dbo.Surveys");
            DropTable("dbo.SurveyAnswers");
            DropTable("dbo.Candidates");
        }
    }
}
