using System;
using DeviceManager.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeviceManager.Api.Migrations
{
    [DbContext(typeof(DeviceContext))]
    [Migration("20180121165138_DatabaseInitialise")]
    partial class DatabaseInitialise
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DeviceManagerApi.Data.Model.Device", b =>
                {
                    b.Property<Guid>("DeviceId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DeviceTitle");

                    b.HasKey("DeviceId");

                    b.ToTable("Devices");
                });
        }
    }
}
