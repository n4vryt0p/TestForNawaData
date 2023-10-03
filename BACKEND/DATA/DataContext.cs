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