namespace MySoftware;

internal class Program
{
    public static void Main(string[] args)
    {

        Console.WriteLine("Enter an integer:");
        double integer =  Convert.ToDouble(Console.ReadLine());

        if (IsEven(integer))
        {
            Console.WriteLine($"The number is even.");
        }
        else
        {
            Console.WriteLine($"The number is odd.");
        }

        Console.ReadLine();

    }

    public static bool IsEven(double value)
    {
        return value % 2 == 0;
    }

}
