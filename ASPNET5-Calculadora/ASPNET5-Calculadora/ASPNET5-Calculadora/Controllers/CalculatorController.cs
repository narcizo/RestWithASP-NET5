using Microsoft.AspNetCore.Mvc;

namespace ASPNET5_Calculadora.Controllers;

[ApiController]
[Route("[controller]")]
public class CalculatorController : ControllerBase
{
    private readonly ILogger<CalculatorController> _logger;

    public CalculatorController(ILogger<CalculatorController> logger)
    {
        _logger = logger;
    }

    [HttpGet("sum/{firstNumber}/{secondNumber}")]
    public IActionResult GetSum(string firstNumber, string secondNumber)
    {
        if(isNumeric(firstNumber) && isNumeric(firstNumber))
        {
            var sum = ConvertToDecimal(firstNumber) + ConvertToDecimal(secondNumber);
            return Ok(sum.ToString());
        }
        return BadRequest("Invalid Input");
    }

    [HttpGet("sub/{firstNumber}/{secondNumber}")]
    public IActionResult GetSub(string firstNumber, string secondNumber)
    {
        return BadRequest("Invalid Input");
    }

    [HttpGet("mult/{firstNumber}/{secondNumber}")]
    public IActionResult GetMult(string firstNumber, string secondNumber)
    {
        if (isNumeric(firstNumber) && isNumeric(secondNumber))
        {
            var sum = ConvertToDecimal(firstNumber) * ConvertToDecimal(secondNumber);
            return Ok(sum.ToString());
        }
        return BadRequest("Invalid Input");
    }

    [HttpGet("div/{firstNumber}/{secondNumber}")]
    public IActionResult GetDiv(string firstNumber, string secondNumber)
    {
        if (isNumeric(firstNumber) && isNumeric(secondNumber))
        {
            var sum = ConvertToDecimal(firstNumber) / ConvertToDecimal(secondNumber);
            return Ok(sum.ToString());
        }
        return BadRequest("Invalid Input");
    }

    [HttpGet("avg")]
    public IActionResult GetAvg([FromQuery]string numbers)
    {
        decimal sum = 0;

        foreach (var number in numbers.Split(','))
        {
            if (isNumeric(number)) sum += ConvertToDecimal(number);
        }

        sum /= numbers.Split(','). Length;

        return Ok(sum.ToString());
    }

    [HttpGet("squareRoot/{number}")]
    public IActionResult GetSquareRoot(string number)
    {
        if (isNumeric(number))
        {
            decimal square = ConvertToDecimal(number);
            decimal root;

            if (square < 0) return Ok("0");

            root = square / 3;

            for (int i = 0; i < 32; i++)
                root = (root + square / root) / 2;

            return Ok(root.ToString());
        }
        return BadRequest("Invalid Input");
    }

    private bool isNumeric(string strNumber)
    {
        double number;

        return double.TryParse(
            strNumber, 
            System.Globalization.NumberStyles.Any, 
            System.Globalization.NumberFormatInfo.InvariantInfo, 
            out number);
    }

    private decimal ConvertToDecimal(string strNumber)
    {
        decimal decimalValue;

        if (decimal.TryParse(strNumber, out decimalValue))
            return decimalValue;
        return 0;

    }
}

