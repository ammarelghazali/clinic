using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace clinic.Models
{
    public partial class clinicContext : DbContext
    {
        public clinicContext()
        {
        }

        public clinicContext(DbContextOptions<clinicContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<View1> View1s { get; set; }
        public virtual DbSet<View2> View2s { get; set; }
        public virtual DbSet<Visit> Visits { get; set; }
        public virtual DbSet<VisitType> VisitTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=clinic;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.ToTable("Doctor");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(10)
                    .HasColumnName("name")
                    .IsFixedLength(true);

                entity.Property(e => e.Number)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("number");
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.ToTable("Patient");

                entity.HasIndex(e => e.Mobile, "UQ__Patient__A32E2E1CAF39C767")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("address");

                entity.Property(e => e.Age)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("age");

                entity.Property(e => e.Mobile)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("mobile");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Ssn)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ssn");
            });

            modelBuilder.Entity<View1>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_1");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("address");

                entity.Property(e => e.Age)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("age");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<View2>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_2");

                entity.Property(e => e.Age)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("age");

                entity.Property(e => e.BloodPressure)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("blood_pressure");

                entity.Property(e => e.BloodSugarLevel)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("blood_sugar_level");

                entity.Property(e => e.DoctorNotices)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("doctor_notices");

                entity.Property(e => e.DoctorTime)
                    .HasColumnType("datetime")
                    .HasColumnName("doctor_time");

                entity.Property(e => e.Medicine)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("medicine");

                entity.Property(e => e.Mobile)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("mobile");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.NurseNotices)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nurse_notices");

                entity.Property(e => e.PatientIdFk).HasColumnName("Patient_id_fk");

                entity.Property(e => e.VisitTime)
                    .HasColumnType("datetime")
                    .HasColumnName("visit_time");

                entity.Property(e => e.VisitTypeIdFk).HasColumnName("visit_type_id_fk");
            });

            modelBuilder.Entity<Visit>(entity =>
            {
                entity.ToTable("Visit");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BloodPressure)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("blood_pressure");

                entity.Property(e => e.BloodSugarLevel)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("blood_sugar_level");

                entity.Property(e => e.DoctorIdFk).HasColumnName("doctor_id_fk");

                entity.Property(e => e.DoctorNotices)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("doctor_notices");

                entity.Property(e => e.DoctorTime)
                    .HasColumnType("datetime")
                    .HasColumnName("doctor_time");

                entity.Property(e => e.IsChecked)
                    .HasColumnName("Is_Checked")
                    .HasDefaultValueSql("('False')");

                entity.Property(e => e.Medicine)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("medicine");

                entity.Property(e => e.NurseNotices)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nurse_notices");

                entity.Property(e => e.PatientIdFk).HasColumnName("Patient_id_fk");

                entity.Property(e => e.VisitTime)
                    .HasColumnType("datetime")
                    .HasColumnName("visit_time")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.VisitTypeIdFk).HasColumnName("visit_type_id_fk");

                entity.HasOne(d => d.DoctorIdFkNavigation)
                    .WithMany(p => p.Visits)
                    .HasForeignKey(d => d.DoctorIdFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Visit_Doctor");

                entity.HasOne(d => d.PatientIdFkNavigation)
                    .WithMany(p => p.Visits)
                    .HasForeignKey(d => d.PatientIdFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Visit_Patient");

                entity.HasOne(d => d.VisitTypeIdFkNavigation)
                    .WithMany(p => p.Visits)
                    .HasForeignKey(d => d.VisitTypeIdFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Visit_visit_type");
            });

            modelBuilder.Entity<VisitType>(entity =>
            {
                entity.ToTable("visit_type");

                entity.Property(e => e.VisitTypeId).HasColumnName("visit_type_id");

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("type");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
