using homework8.Controllers;

namespace homework8.Models
{
    public interface ICalculator
    {
        decimal Calculate(CalcArguments args);       
    }
}