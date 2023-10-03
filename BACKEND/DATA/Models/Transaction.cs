using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BACKEND.DATA.Models;

public class Transaction
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int? AccountId { get; set; }
    public Account? Account { get; set; }
    public DateTime? TransactionDate { get; set; }
    public int? MasterTransactionId { get; set; }
    public MasterTransaction? MasterTransaction { get; set; }
    public string? DebitCreditStatus { get; set; }
    [Column(TypeName = "money")]
    public decimal? Amount { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int? Point { get; set; }
}