using BACKEND.DATA.Models;
using Microsoft.EntityFrameworkCore;

namespace BACKEND.DATA;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Transaction> Transactions => Set<Transaction>();
    public DbSet<MasterTransaction> MasterTransactions => Set<MasterTransaction>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _ = modelBuilder.Entity<Transaction>(a =>
        {
            _ = a.HasOne(x => x.Account)
                .WithMany(x => x.Transactions)
                .HasForeignKey(x => x.AccountId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.ClientSetNull);

            _ = a.HasOne(x => x.MasterTransaction)
                .WithMany(x => x.Transactions)
                .HasForeignKey(x => x.MasterTransactionId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.ClientSetNull);

            _ = a.Property(r => r.Point)
                .IsRequired(false)
                .HasComputedColumnSql(
                    @"CASE
                        WHEN MasterTransactionId = 2 AND Amount >= 10001 AND Amount <= 30000 THEN FLOOR(Amount / 1000)
                        WHEN MasterTransactionId = 2 AND Amount > 30000 THEN FLOOR(Amount / 1000) * 2
                        WHEN MasterTransactionId = 3 AND Amount >= 50001 AND Amount <= 100000 THEN FLOOR(Amount / 2000)
                        WHEN MasterTransactionId = 3 AND Amount > 100000 THEN FLOOR(Amount / 2000) * 2
                        ELSE 0
                    END"
                    );
        });

        _ = modelBuilder.Entity<Account>(a =>
        {
            _ = a.HasData(
                new Account { Id = 1, Name = "Customer 1" }
            );
        });

        _ = modelBuilder.Entity<MasterTransaction>(a =>
        {
            _ = a.HasData(
                new MasterTransaction { Id = 1, Name = "Setor Tunai" },
                new MasterTransaction { Id = 2, Name = "Beli Pulsa" },
                new MasterTransaction { Id = 3, Name = "Bayar Listrik" }
            );
        });


        base.OnModelCreating(modelBuilder);
    }
}