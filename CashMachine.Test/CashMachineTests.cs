using System;
using CashMachine;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CashMachine.Test
{
    [TestClass]
    public class CashMachineTests
    {
        [TestMethod]
        public void CashMachine_WithdrawalOneOfEachBill()
        {
            // arrange
            var cashMachine = new CashMachine();
            var success = false;

            // act
            success = cashMachine.WithdrawCash(186);

            // assert 
            Assert.IsTrue(success);
            Assert.IsTrue(cashMachine.AvailableBills.All(b => b.Quantity == 9));
        }

        [TestMethod]
        public void CashMachine_RemoveGreaterAmountThanAvailable()
        {
            // arrange 
            var cashMachine = new CashMachine();
            var success = true;

            // act
            success = cashMachine.WithdrawCash(10000);

            // assert
            Assert.IsFalse(success);
            Assert.IsTrue(cashMachine.AvailableBills.All(b => b.Quantity == 10));
        }

        [TestMethod]
        public void CashMachine_UnableToWithdrawLastWithdrawal()
        {
            // arrange 
            var cashMachine = new CashMachine();
            var firstWithdrawalSuccess = false;
            var secondWithdrawalSuccess = false;
            var thirdWithdrawalSuccess = true;

            // act
            firstWithdrawalSuccess = cashMachine.WithdrawCash(208);
            secondWithdrawalSuccess = cashMachine.WithdrawCash(9);
            thirdWithdrawalSuccess = cashMachine.WithdrawCash(9);

            // assert
            Assert.IsTrue(firstWithdrawalSuccess);
            Assert.IsTrue(secondWithdrawalSuccess);
            Assert.IsFalse(thirdWithdrawalSuccess);
        }

        [TestMethod]
        public void CashMachine_GetInquiry()
        {
            // arrange 
            var cashMachine = new CashMachine();
            var firstWithdrawalSuccess = false;

            // act
            firstWithdrawalSuccess = cashMachine.WithdrawCash(208);
            var hundredsInquiry = cashMachine.Inquiry(100);

            // assert
            Assert.IsTrue(firstWithdrawalSuccess);
            Assert.IsTrue(hundredsInquiry.Quantity == 8);
        }

        [TestMethod]
        public void CashMachine_ConfirmRestock()
        {
            // arrange 
            var cashMachine = new CashMachine();

            // act
            cashMachine.WithdrawCash(186);
            cashMachine.Restock();

            // assert
            Assert.IsTrue(cashMachine.AvailableBills.All(b => b.Quantity == 10));
        }

        [TestMethod]
        public void CashMachine_InvalidInquiry()
        {
            // arrange 
            var cashMachine = new CashMachine();
            var twoDollarInquiry = cashMachine.Inquiry(2);

            // assert
            Assert.IsNull(twoDollarInquiry);
        }
        
    }
}
