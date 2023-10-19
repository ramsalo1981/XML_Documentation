using System;

namespace CAXMLDocumentation
{
    internal class Program
    {
        //for documintation; project ---> properties ---> build -----> output ----> xml documintation
        static void Main(string[] args)
        {
            do
            {
                Console.Write("First Name: ");
                var fname = Console.ReadLine();

                Console.Write(" Last Name: ");
                var lname = Console.ReadLine();

                Console.Write(" Hire Date: ");
                DateTime? hireDate = null;
                if (DateTime.TryParse(Console.ReadLine(), out DateTime hDate))
                {
                    hireDate = hDate;
                }
                var empId = Generator.GenerateId(fname, lname, hireDate);
                var randomPassword = Generator.GenerateRandomPassword(8);

                Console.WriteLine($"{{\n Id: {empId},\n FName: {fname},\n LName: {lname},\n hire date: {hireDate.Value.ToShortDateString()}, \n Password: {randomPassword}\n}}");

            } while (1 == 1);
        }
    }
    /// <include file="Generator.xml" path='docs/members[@name="generator"]/Generator/*'/>
    public class Generator
    {
        
        public static int LastIdSequence { get; private set; } = 1;

        


        public static string GenerateId(string fName, string lName, DateTime? hireDate)
        {
            //first capital of last name,first capital of first name, year, month, day , lastsequance 
            if (fName is null)
            {
                throw new InvalidOperationException($"{nameof(fName)} can not be null");
            }
            if (lName is null)
            {
                throw new InvalidOperationException($"{nameof(lName)} can not be null");
            }
            if (hireDate is null)
            {
                hireDate = DateTime.Now;
            }
            else
            {
                if (hireDate.Value.Date < DateTime.Now.Date)
                    throw new InvalidOperationException($"{nameof(hireDate)} can not be in the past");
            }

            var yy = hireDate.Value.ToString("yy");//year Of HireDate
            var MM = hireDate.Value.ToString("MM"); //month Of HireDate
            var dd = hireDate.Value.ToString("dd"); //day Of HireDate

            var code = $"{lName.ToUpper()[0]}{fName.ToUpper()[0]} {yy} {MM} {dd} {(LastIdSequence++).ToString().PadLeft(2, '0')}";

            return code;
        }

        public static string GenerateRandomPassword(int length)
        {
            const string ValidScope = "abcdefghijklmnopqrstuvwxyzABCDEFGHIGKLMNOPQRSTUVWXYZ0123456789";
            var result = "";
            Random rnd = new Random();
            while (0 < length--)
            {
                result += ValidScope[rnd.Next(ValidScope.Length)];
            }

            return result;
        }
    }
}