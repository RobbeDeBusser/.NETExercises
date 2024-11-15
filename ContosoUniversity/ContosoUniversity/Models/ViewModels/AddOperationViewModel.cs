namespace ContosoUniversity.Models.ViewModels
{
    public enum Operator
    {
        Add,
        Subtract
    }
    public class AddOperationViewModel
    {
        public int Number1 { get; set; }
        public int Number2 { get; set; }
        public Operator Operator { get; set; }
        public int Result { get; set; }
    }
}
