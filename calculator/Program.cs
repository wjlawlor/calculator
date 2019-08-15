using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculator
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Welcome to the Calculator app!");
            Console.WriteLine();
            Console.WriteLine("Press [CTRL]+[C] to Exit.");
            Console.WriteLine();

            while (true)
            {
                string arguMessageOne = "Enter the first argument: ";
                string argumentOne = GetArgument(arguMessageOne);
                ArgumentType argumentOneType = GetArgumentType(argumentOne);

                OperatorType operatorType = GetOperatorType();
                Console.WriteLine();

                string arguMessageTwo = "Enter the second argument: ";
                string argumentTwo = GetArgument(arguMessageTwo);
                ArgumentType argumentTwoType = GetArgumentType(argumentTwo);

                if (argumentOneType == ArgumentType.Number && argumentTwoType == ArgumentType.Number)
                {
                    decimal? result = null;
                    decimal argumentOneNum = decimal.Parse(argumentOne);
                    decimal argumentTwoNum = decimal.Parse(argumentTwo);

                    try
                    {
                        if (operatorType == OperatorType.Addition)
                        {
                            result = argumentOneNum + argumentTwoNum;
                        }
                        else if (operatorType == OperatorType.Subtraction)
                        {
                            result = argumentOneNum - argumentTwoNum;
                        }
                        else if (operatorType == OperatorType.Multiplication)
                        {
                            result = argumentOneNum * argumentTwoNum;
                        }
                        else if (operatorType == OperatorType.Division)
                        {
                            result = argumentOneNum / argumentTwoNum;
                        }
                    }
                    catch (OverflowException)
                    {
                        Console.WriteLine("Overflow Error! The numbers you entered were too big!");
                    }

                    Console.WriteLine("Result = {0}", result);

                }
                else if ((argumentOneType == ArgumentType.String && argumentTwoType == ArgumentType.String) && (operatorType == OperatorType.Addition || operatorType == OperatorType.Subtraction))
                {
                    string result = "";

                    if (operatorType == OperatorType.Addition)
                    {
                        result = argumentOne + argumentTwo;
                    }
                    else if (operatorType == OperatorType.Subtraction)
                    {
                        // Taking characters from the second string from the first.
                        string filteredString = argumentOne;
                        char[] characters = argumentTwo.ToCharArray();

                        for (int index = 0; index < characters.Length; index++)
                        {
                            char character = characters[index];
                            int characterindex = filteredString.IndexOf(character);
                            while (characterindex > -1)
                            {
                                filteredString = filteredString.Remove(characterindex, 1);
                                characterindex = filteredString.IndexOf(character);
                            }
                        }
                        result = filteredString;
                    }
                    Console.WriteLine("Result = {0}", result);
                }
                else if ((argumentOneType == ArgumentType.DateTime && argumentTwoType == ArgumentType.DateTime) && operatorType == OperatorType.Subtraction)
                {
                    DateTimeOffset argumentOneDateTime = DateTimeOffset.Parse(argumentOne);
                    DateTimeOffset argumentTwoDateTime = DateTimeOffset.Parse(argumentTwo);

                    // Gives you the timespan between.
                    TimeSpan result = argumentOneDateTime - argumentTwoDateTime;

                    Console.WriteLine("Result = {0}", result);
                }
                else if ((argumentOneType == ArgumentType.DateTime && argumentTwoType == ArgumentType.TimeSpan) && (operatorType == OperatorType.Addition || operatorType == OperatorType.Subtraction))
                {

                    DateTimeOffset argumentOneDateTime = DateTimeOffset.Parse(argumentOne);
                    TimeSpan argumentTwoTimeSpan = TimeSpan.Parse(argumentTwo);

                    DateTimeOffset result = new DateTimeOffset();

                    if (operatorType == OperatorType.Addition)
                    {
                        result = argumentOneDateTime + argumentTwoTimeSpan;
                    }
                    else if (operatorType == OperatorType.Subtraction)
                    {
                        result = argumentOneDateTime - argumentTwoTimeSpan;
                    }

                    Console.WriteLine("Result = {0}", result);
                }
                else if ((argumentOneType == ArgumentType.String && argumentTwoType == ArgumentType.Number) && operatorType == OperatorType.Multiplication)
                {
                    int argumentTwoNumber = int.Parse(argumentTwo);
                    string result = string.Empty;

                    // Adds the string to itself for as many times as the count of the number.
                    for (int stringInstance = 0; stringInstance < argumentTwoNumber; stringInstance++)
                    {
                        result = result + argumentOne;
                    }
                    Console.WriteLine("Result = {0}", result);
                }
                else
                {
                    Console.WriteLine("Error. Requested operation does not exist.");
                    Console.WriteLine();
                    Console.WriteLine("The following operations are supported:");
                    Console.WriteLine();
                    Console.WriteLine(" * + , - , * , / for Numerical Values");
                    Console.WriteLine(" * String + String for String Concatantation");
                    Console.WriteLine(" * String - String for removing elements from a String");
                    Console.WriteLine(" * Date/Time - Date/Time for Time Span");
                    Console.WriteLine(" * Date/Time +/- Time Span");
                    Console.WriteLine(" * String * A Number for String Repetition");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Takes in an argument number and prompts the user to input a valid argument for calculations.
        /// </summary>
        /// <param name="message">Is either going to prompt the user for a first or second argument, depending on this.</param>
        /// <returns>Returns the user input as a string.</returns>
        private static string GetArgument(string message)
        {
            Console.Write(message);
            string argument = Console.ReadLine();
            while (String.IsNullOrEmpty(argument))
            {
                Console.Write("Please enter a valid arguement: ");
                argument = Console.ReadLine();
            }
            Console.WriteLine();
            return argument;
        }

        /// <summary>
        /// Takes in the operator and assigns it a type, from the list provided by Enum OperatorType.
        /// </summary>
        /// <returns>The operator type.</returns>
        private static OperatorType GetOperatorType()
        {
            OperatorType operatorType = OperatorType.Unknown;

            Console.Write("Enter an operator ('+', '-', '*', or '/'): ");
            string input = Console.ReadLine();

            // Prompts user for a valid operator. 
            while (string.IsNullOrEmpty(input) || (input != "+" && input != "-" && input != "*" && input != "/"))
            {
                Console.Write("Error. Please enter a valid operator. (+ , - , * , /): ");
                input = Console.ReadLine();
            }

            if(input == "+")
            {
                operatorType = OperatorType.Addition;
            }
            else if(input == "-")
            {
                operatorType = OperatorType.Subtraction;
            }
            else if (input == "*")
            {
                operatorType = OperatorType.Multiplication;
            }
            else if (input == "/")
            {
                operatorType = OperatorType.Division;
            }

            return operatorType;
        }
        
        /// <summary>
        /// Takes in an argument and assigns it a type, from the list provided by Enum ArgumentType.
        /// </summary>
        /// <param name="argument">User entered value.</param>
        /// <returns>Assigns a type of number, datetime, or timespan; if none of the above, stays a "string".</returns>
        private static ArgumentType GetArgumentType(string argument)
        {
            // Defaults to string.
            ArgumentType typeOfArgument = ArgumentType.String;

            decimal argumentNumber;
            TimeSpan argumentTimeSpan;
            DateTimeOffset argumentDateTime;

            // Testing the input argument for a type. Ask about TimeSpan before DateTimeOffset.
            if(decimal.TryParse(argument, out argumentNumber))
            {
                typeOfArgument = ArgumentType.Number;
            }
            else if(TimeSpan.TryParse(argument, out argumentTimeSpan))
            {
                typeOfArgument = ArgumentType.TimeSpan;
            }
            else if (DateTimeOffset.TryParse(argument, out argumentDateTime))
            {
                typeOfArgument = ArgumentType.DateTime;
            }
            return typeOfArgument; 
        }       
    }

    enum ArgumentType
    {
        String,
        Number,
        DateTime,
        TimeSpan,
    }

    enum OperatorType
    {
        Unknown,
        Addition,
        Subtraction,
        Multiplication,
        Division,
    }
}
