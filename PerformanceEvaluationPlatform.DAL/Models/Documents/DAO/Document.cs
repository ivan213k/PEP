using Microsoft.EntityFrameworkCore;
using PerformanceEvaluationPlatform.DAL.Models.Shared;
using PerformanceEvaluationPlatform.DAL.Models.Users.Dao;
using System;
namespace PerformanceEvaluationPlatform.DAL.Models.Documents.Dao
{
    public class Document: IUpdatebleCreateable
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TypeId { get; set; }
        public DateTime ValidToDate { get; set; }
        public string FileName { get; set; }
        public int CreatedById { get; set; }
        public DateTime CreatedAt { get; }
        public int? LastUpdatesById { get; set; }
        public DateTime? LastUpdatesAt { get; }
        public string MetaData { get; set; }

        //navigation prop
        public DocumentType DocumentType { get; set; }
        public User User { get; set; }
        public User CreatedBy { get; set; }
        public User UpdatedBy { get; set; }

       

        public static void Configure(ModelBuilder modelBuilder)
        {
            var documentBuilder = modelBuilder.Entity<Document>();
            documentBuilder.ToTable("Document");
            documentBuilder.HasKey(t => t.Id);
            documentBuilder.Property(t => t.UserId)
                .IsRequired();
            documentBuilder.Property(t => t.TypeId)
                .IsRequired();
            documentBuilder.Property(t => t.ValidToDate)
                .IsRequired();
            documentBuilder.Property(t => t.FileName)
                .IsRequired()
                .HasMaxLength(512);
            documentBuilder.Property(t => t.CreatedById)
                .IsRequired();
            documentBuilder.Property(t => t.MetaData)
                .IsRequired();


            documentBuilder.HasOne(t => t.DocumentType)
                .WithMany()
                .HasForeignKey(t => t.TypeId)
                .IsRequired();
            documentBuilder.HasOne(t => t.User)
                .WithMany()
                .HasForeignKey(t => t.UserId)
                .IsRequired();
            documentBuilder.HasOne(t => t.CreatedBy)
                .WithMany()
                .HasForeignKey(t => t.CreatedById)
                .IsRequired();
            documentBuilder
                .HasOne(t => t.UpdatedBy)
                .WithMany()
            .HasForeignKey(t => t.LastUpdatesById);


        }
    }
}
