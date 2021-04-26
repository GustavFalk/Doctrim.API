using Doctrim.EF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctrim.EF.Data
{
    public class DoctrimContext : DbContext
    {

        public DoctrimContext(DbContextOptions options) : base(options) { }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<DocumentFile>  Documents { get; set; }
        
        public DbSet<MetadataTag> MetadataTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DocumentFile>()
                .HasOne(p => p.Type)
                .WithMany(b => b.Documents)
                .HasForeignKey(p => p.TypeGuid)
                .HasPrincipalKey(b => b.UniqueId);

            modelBuilder.Entity<MetadataTag>()
               .HasOne(p => p.Document)
               .WithMany(b => b.Tags)
               .HasForeignKey(p => p.DocumentGuid)
               .HasPrincipalKey(b => b.UniqueId);

            modelBuilder.Entity<DocumentType>()
                .Property(x => x.UniqueId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<DocumentFile>()
               .Property(x => x.UniqueId)
               .ValueGeneratedOnAdd();
        }


    }
    
}
