using System.ComponentModel.DataAnnotations.Schema;
using CalculatorF;

namespace WebCalculatorWithDI.DataBase
{
    public class ExpressionEntity
    {
        public string? Expression { get; init; }
        public decimal? Res { get; set; }
    }
}