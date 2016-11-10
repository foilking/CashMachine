using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashMachine
{
    public class CashMachine
    {
        public List<BillDenomination> AvailableBills;

        public CashMachine()
        {
            // Default configuration
            AvailableBills = new List<BillDenomination>();
            AvailableBills.Add(new BillDenomination { Value = 100, Quantity = 10 });
            AvailableBills.Add(new BillDenomination { Value = 50, Quantity = 10 });
            AvailableBills.Add(new BillDenomination { Value = 20, Quantity = 10 });
            AvailableBills.Add(new BillDenomination { Value = 10, Quantity = 10 });
            AvailableBills.Add(new BillDenomination { Value = 5, Quantity = 10 });
            AvailableBills.Add(new BillDenomination { Value = 1, Quantity = 10 });
        }

        public void Restock()
        {
            foreach (var billDemonination in AvailableBills)
            {
                billDemonination.Quantity = 10;
            }
        }

        public bool WithdrawCash(int withdrawalAmount)
        {
            var uniqueDenominations = AvailableBills.OrderByDescending(b => b.Value);
            return RecursiveWithdrawal(withdrawalAmount, uniqueDenominations.First(), uniqueDenominations.Skip(1));
        }

        private bool RecursiveWithdrawal(int withdrawalAmount, BillDenomination denomination, IEnumerable<BillDenomination> remainingDenominations)
        {
            var quantityToRemove = withdrawalAmount / denomination.Value;
            if (denomination.Quantity < quantityToRemove)
            {
                return false;
            }
            else
            {
                var remainingWithdrawal = withdrawalAmount - (denomination.Value * quantityToRemove);
                var success = remainingWithdrawal == 0 ? true : RecursiveWithdrawal(remainingWithdrawal, remainingDenominations.First(), remainingDenominations.Skip(1));
                if (success)
                {
                    denomination.Quantity = denomination.Quantity - quantityToRemove;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public List<BillDenomination> GetMachineBalance()
        {
            return AvailableBills.OrderByDescending(b => b.Value).ToList();
        }

        public BillDenomination Inquiry(int inquiryAmount)
        {
            return AvailableBills.FirstOrDefault(b => b.Value == inquiryAmount);
        }
    }
}
