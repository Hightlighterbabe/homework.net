using hw9.Models;

namespace hw9.DataBases
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