using System;
using System.Linq;
using Console = Colorful.Console;
using System.Drawing;

namespace CashMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            var cashMachine = new CashMachine();
            Console.WriteAscii(" TYME", color: Color.Green);
            Console.WriteAscii(" MACHINE", color: Color.Green);
            string input = string.Empty;
            while (!input.ToUpper().Equals("Q"))
            {
                var alternateColor = true;
                Console.WriteLine("What would you like to do?", Color.Green);
                Console.WriteLine("Available Commands: (W)ithdraw (I)nquiry (R)estock (Q)uit", Color.Green);
                input = Console.ReadLine();
                switch (input.Substring(0, Math.Min(1, input.Length)).ToUpper())
                {
                    case "R":
                        cashMachine.Restock();
                        DisplayBalance(cashMachine);
                        break;
                    case "W":
                        var withdrawals = input.Split('$').Skip(1);
                        foreach (var withdrawal in withdrawals)
                        {
                            int withdrawalAmount = 0;
                            if (Int32.TryParse(withdrawal, out withdrawalAmount))
                            {
                                var success = cashMachine.WithdrawCash(withdrawalAmount);
                                if (success)
                                {
                                    Console.WriteLine(string.Format("Success: Dispensed ${0}", withdrawalAmount), Color.Green);
                                    DisplayBalance(cashMachine);
                                }
                                else
                                {
                                    Console.WriteLine("Failure: insufficient funds", Color.Red);
                                }
                            }
                        }
                        break;
                    case "I":
                        var inquiries = input.Split('$').Skip(1);
                        foreach (var inquiry in inquiries)
                        {
                            int inquiryAmount = 0;
                            if (Int32.TryParse(inquiry, out inquiryAmount))
                            {
                                var billDenomination = cashMachine.Inquiry(inquiryAmount);
                                if (billDenomination != null)
                                {
                                    Console.WriteLine(string.Format("${0} - {1}", billDenomination.Value, billDenomination.Quantity), alternateColor ? Color.Aqua : Color.Gold);
                                }
                                else
                                {
                                    Console.WriteLine(string.Format("No bills available at ${0}", inquiry), Color.Red);
                                }
                                alternateColor = !alternateColor;
                            }
                        }
                        break;
                    case "Q":
                        Console.WriteLine("Thank you for using the", Color.Green);
                        Console.WriteAscii(" TYME", Color.Green);
                        Console.WriteAscii(" MACHINE", color: Color.Green);
                        System.Threading.Thread.Sleep(900);
                        break;
                    default:
                        Console.WriteLine("Failure: Invalid Command", Color.Red);
                        break;
                }
            }
        }

        private static void DisplayBalance(CashMachine cashMachine)
        {
            Console.WriteLine("Machine balance:");
            var balance = cashMachine.GetMachineBalance();
            var alternateColor = true;
            foreach (var billDenomination in balance)
            {
                if (billDenomination.Quantity > 0)
                {
                    Console.WriteLine(string.Format("${0} - {1}", billDenomination.Value, billDenomination.Quantity), alternateColor ? Color.Aqua : Color.Gold);
                    alternateColor = !alternateColor;
                }
                else
                {
                    Console.WriteLine(string.Format("No bills available at ${0}", billDenomination.Value), Color.Red);
                }
            }
        }
    }
}
