using ATM_DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ATM_DAL.Data
{
    public class AtmDbContext : DbContext
    {

        public AtmDbContext(DbContextOptions<AtmDbContext> options) : base(options)
        {


        }


        public DbSet<BankAccounts> BankAccounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }



       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BankAccounts>()
                .Property(b => b.Balance)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Transaction>()
                .Property(t => t.TransactionAmount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<BankAccounts>()
                .HasKey(b => b.CardNumber)
                .HasName("PK_BankAccounts_CardNumber");

            modelBuilder.Entity<BankAccounts>()
                .Property(b => b.CardNumber)
                .ValueGeneratedNever();

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.FromAccount)
                .WithMany(a => a.FromTransactions)
                .HasForeignKey(t => t.BankAccountNoFrom)
                .HasPrincipalKey(b => b.CardNumber)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.ToAccount)
                .WithMany(a => a.ToTransactions)
                .HasForeignKey(t => t.BankAccountNoTo)
                .HasPrincipalKey(b => b.CardNumber)
                .OnDelete(DeleteBehavior.NoAction);

           
        }





    }
}
