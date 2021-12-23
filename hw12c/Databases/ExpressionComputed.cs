using hw12с.Models;

namespace hw12с.DataBases
{
    public class ExpressionsComputed
    {
        public int Id { get; set; }
        public decimal Val1 { get; init; } 
        public decimal Val2 { get; init; }
        public Operation Op { get; init; } 
        public decimal? Res { get; set; }
    }
}