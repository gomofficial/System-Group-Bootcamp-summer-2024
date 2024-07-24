using Task_1.Interface;

namespace Test.TestDataTypes;

public class Temperature:IPrintable,IComparable<Temperature>
{
    public double Fahrenheit;

    public Temperature(double fahrenheit)
    {
        this.Fahrenheit = fahrenheit;
    }
    public string Print()
    {
        return Fahrenheit.ToString();
    }

    public int CompareTo(Temperature? tmp) {
        if (tmp != null)
            return this.Fahrenheit.CompareTo(tmp.Fahrenheit);
        else
            throw new NullReferenceException("Null");
    }
}