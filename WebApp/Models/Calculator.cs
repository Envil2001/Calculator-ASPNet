
public enum Operators
{
    Add, Sub, Mul, Div
}

public class Calculator
{
    public Operators? Operator { get; set; }
    public double? X { get; set; }
    public double? Y { get; set; }

    public string Op
    {
        get
        {
            return Operator switch
            {
                Operators.Add => "+",
                Operators.Sub => "−",
                Operators.Mul => "×",
                Operators.Div => "÷",
                _ => ""
            };
        }
    }

    public bool IsValid()
    {
        return Operator != null && X != null && Y != null;
    }

    public double Calculate()
    {
        return Operator switch
        {
            Operators.Add => X.Value + Y.Value,
            Operators.Sub => X.Value - Y.Value,
            Operators.Mul => X.Value * Y.Value,
            Operators.Div => Y.Value == 0 ? double.NaN : X.Value / Y.Value,
            _ => double.NaN
        };
    }
}