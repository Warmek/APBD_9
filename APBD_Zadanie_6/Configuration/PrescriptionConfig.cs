﻿using APBD_Zadanie_6.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APBD_Zadanie_6.Configuration
{
    internal class PrescriptionConfig : IEntityTypeConfiguration<Prescription>
    {
        public void Configure(EntityTypeBuilder<Prescription> builder)
        {
            builder.HasKey(e => e.IdPrescription).HasName("IdPrescription_PK");
            builder.Property(e => e.IdPrescription).UseIdentityColumn();

            builder.Property(e => e.Date).IsRequired();
            builder.Property(e => e.DueDate).IsRequired();

            builder.HasOne(e => e.IdPatientNav)
                .WithMany(e => e.Prescriptions)
                .HasForeignKey(e => e.IdPatient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Patient_Prescription_FK");


            builder.HasOne(e => e.IdDoctorNav)
                .WithMany(e => e.Prescriptions)
                .HasForeignKey(e => e.IdDoctor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Doctor_Prescription_FK");

            var prescriptions = new List<Prescription>();

            prescriptions.Add(new Prescription
            {
                IdPrescription = 1,
                Date = DateTime.Now,
                DueDate = DateTime.Now.AddDays(5),
                IdPatient = 3,
                IdDoctor = 4
            });
            prescriptions.Add(new Prescription
            {
                IdPrescription = 2,
                Date = DateTime.Now,
                DueDate = DateTime.Now.AddDays(3),
                IdPatient = 2,
                IdDoctor = 3
            });
            prescriptions.Add(new Prescription
            {
                IdPrescription = 3,
                Date = DateTime.Now,
                DueDate = DateTime.Now.AddDays(7),
                IdPatient = 1,
                IdDoctor = 6
            });

            builder.HasData(prescriptions);
        }
    }
}