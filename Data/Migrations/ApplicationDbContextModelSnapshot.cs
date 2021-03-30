﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using _2021_dotnet_e_02.Data;

namespace _2021_dotnet_e_02.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("_2021_dotnet_e_02.Models.ActemiumCompany", b =>
                {
                    b.Property<int>("CompanyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("COMPANYID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ADDRESS");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("CITY");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("COUNTRY");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("NAME");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("PHONENUMBER");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("REGISTRATIONDATE");

                    b.HasKey("CompanyId");

                    b.ToTable("ACTEMIUMCOMPANY");
                });

            modelBuilder.Entity("_2021_dotnet_e_02.Models.ActemiumContract", b =>
                {
                    b.Property<int>("ContractId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("ENDDATE");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("STARTDATE");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("STATUS");

                    b.HasKey("ContractId")
                        .HasName("CONTRACTID");

                    b.ToTable("ACTEMIUMCONTRACT");
                });

            modelBuilder.Entity("_2021_dotnet_e_02.Models.ActemiumContractType", b =>
                {
                    b.Property<int>("ContractTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CONTRACTTYPEID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("HasApplication")
                        .HasColumnType("bit")
                        .HasColumnName("HASAPPLICATION");

                    b.Property<bool>("HasEmail")
                        .HasColumnType("bit")
                        .HasColumnName("HASEMAIL");

                    b.Property<bool>("HasPhone")
                        .HasColumnType("bit")
                        .HasColumnName("HASPHONE");

                    b.Property<int>("MaxHandlingTime")
                        .HasColumnType("int")
                        .HasColumnName("MAXHANDLINGTIME");

                    b.Property<int>("MinThroughputTime")
                        .HasColumnType("int")
                        .HasColumnName("MINTHROUGHPUTTIME");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("NAME");

                    b.Property<double>("Price")
                        .HasColumnType("float")
                        .HasColumnName("PRICE");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("STATUS");

                    b.Property<string>("TimeStamp")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("TIMESTAMP");

                    b.HasKey("ContractTypeId");

                    b.ToTable("ACTEMIUMCONTRACTTYPE");
                });

            modelBuilder.Entity("_2021_dotnet_e_02.Models.ActemiumKbItem", b =>
                {
                    b.Property<int>("KbItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Keywords")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("KbItemId");

                    b.ToTable("ACTEMIUMKBITEM");
                });

            modelBuilder.Entity("_2021_dotnet_e_02.Models.ActemiumTicket", b =>
                {
                    b.Property<int>("TicketId")
                        .HasColumnType("int");

                    b.Property<string>("Attachments")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateAndTimeOfCreation")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Priority")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Quality")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Solution")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SupportNeeded")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TicketType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TicketId");

                    b.ToTable("ACTEMIUMTICKET");
                });

            modelBuilder.Entity("_2021_dotnet_e_02.Models.ActemiumTicketChange", b =>
                {
                    b.Property<int>("TicketChangeId")
                        .HasColumnType("int");

                    b.Property<string>("ChangeDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateTimeOfChange")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserRole")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TicketChangeId");

                    b.ToTable("ACTEMIUMTICKETCHANGE");
                });

            modelBuilder.Entity("_2021_dotnet_e_02.Models.ActemiumTicketComment", b =>
                {
                    b.Property<int>("TicketCommentId")
                        .HasColumnType("int");

                    b.Property<string>("CommentText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateTimeOfComment")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserRole")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TicketCommentId");

                    b.ToTable("ACTEMIUMTICKETCOMMENT");
                });

            modelBuilder.Entity("_2021_dotnet_e_02.Models.LoginAttempt", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    b.Property<DateTime>("DateAndTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("DATEANDTIME");

                    b.Property<string>("LoginStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("LOGINSTATUS");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("USERNAME");

                    b.HasKey("Id");

                    b.ToTable("LOGINATTEMPT");
                });

            modelBuilder.Entity("_2021_dotnet_e_02.Models.UserModel", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("FailedLoginAttempts")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("REGISTRATIONDATE");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("USERMODEL");
                });

            modelBuilder.Entity("_2021_dotnet_e_02.Models.ActemiumCustomer", b =>
                {
                    b.HasBaseType("_2021_dotnet_e_02.Models.UserModel");

                    b.ToTable("ACTEMIUMCUSTOMER");
                });

            modelBuilder.Entity("_2021_dotnet_e_02.Models.ActemiumEmployee", b =>
                {
                    b.HasBaseType("_2021_dotnet_e_02.Models.UserModel");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("EMAILADDRESS");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("ACTEMIUMEMPLOYEE");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("_2021_dotnet_e_02.Models.ActemiumContract", b =>
                {
                    b.HasOne("_2021_dotnet_e_02.Models.ActemiumCompany", "Company")
                        .WithMany("Contracts")
                        .HasForeignKey("ContractId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("_2021_dotnet_e_02.Models.ActemiumContractType", "ContractType")
                        .WithMany()
                        .HasForeignKey("ContractId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("ContractType");
                });

            modelBuilder.Entity("_2021_dotnet_e_02.Models.ActemiumTicket", b =>
                {
                    b.HasOne("_2021_dotnet_e_02.Models.ActemiumCompany", "Company")
                        .WithMany("Tickets")
                        .HasForeignKey("TicketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("_2021_dotnet_e_02.Models.ActemiumTicketChange", b =>
                {
                    b.HasOne("_2021_dotnet_e_02.Models.ActemiumTicket", "Ticket")
                        .WithMany("TicketChanges")
                        .HasForeignKey("TicketChangeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Ticket");
                });

            modelBuilder.Entity("_2021_dotnet_e_02.Models.ActemiumTicketComment", b =>
                {
                    b.HasOne("_2021_dotnet_e_02.Models.ActemiumTicket", "Ticket")
                        .WithMany("Comments")
                        .HasForeignKey("TicketCommentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Ticket");
                });

            modelBuilder.Entity("_2021_dotnet_e_02.Models.LoginAttempt", b =>
                {
                    b.HasOne("_2021_dotnet_e_02.Models.UserModel", "UserModel")
                        .WithMany("LoginAttempts")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserModel");
                });

            modelBuilder.Entity("_2021_dotnet_e_02.Models.ActemiumCustomer", b =>
                {
                    b.HasOne("_2021_dotnet_e_02.Models.ActemiumCompany", "Company")
                        .WithMany("ContactPersons")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("_2021_dotnet_e_02.Models.UserModel", null)
                        .WithOne()
                        .HasForeignKey("_2021_dotnet_e_02.Models.ActemiumCustomer", "UserId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("_2021_dotnet_e_02.Models.ActemiumEmployee", b =>
                {
                    b.HasOne("_2021_dotnet_e_02.Models.UserModel", null)
                        .WithOne()
                        .HasForeignKey("_2021_dotnet_e_02.Models.ActemiumEmployee", "UserId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("_2021_dotnet_e_02.Models.ActemiumCompany", b =>
                {
                    b.Navigation("ContactPersons");

                    b.Navigation("Contracts");

                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("_2021_dotnet_e_02.Models.ActemiumTicket", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("TicketChanges");
                });

            modelBuilder.Entity("_2021_dotnet_e_02.Models.UserModel", b =>
                {
                    b.Navigation("LoginAttempts");
                });
#pragma warning restore 612, 618
        }
    }
}
