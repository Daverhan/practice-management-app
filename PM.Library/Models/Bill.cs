namespace PM.Library.Models
{
    public class Bill
    {
        public decimal TotalAmount { get; set; }
        public DateTime DueDate { get; set; }

        public override string ToString()
        {
            return "Balance of $" + String.Format("{0:0.00}", TotalAmount) + " due on " + DueDate.ToShortDateString();
        }
    }
}
