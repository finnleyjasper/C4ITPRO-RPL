using ComputeBill;

char c = 'n';
do
{
    Console.WriteLine("Choose an option");
    Console.WriteLine(Utilities.CreateBillInterface());
    char option = Convert.ToChar(Console.ReadLine());

    if (option != '4')
    {
        Console.WriteLine("How many units spent");
        int numU = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("The computed bill amount = " + Utilities.ComputeBill(option, numU));

        Console.WriteLine("Do you want to continue? Press y to continue");
        c = Convert.ToChar(Console.ReadLine().ToLower());
    }
    else
    {
        break;
    }

}while(c=='y');

