using ComputeBill;

namespace ComputeBillUnitTest
{
    [TestClass]
    public sealed class Bill
    {
        [TestMethod]// Just for testing the framework
        public void TestMethod1()
        {
            int expectedOutput = 10;
            int actualOutput = 10;

            Assert.AreEqual( expectedOutput, actualOutput);

        }

        [TestMethod]// unit functional test for IndustrialBill
        public void TestIndustrialBill()
        {
            double expectedOutput = 100;
            ElectricityBill e = new IndustialBill(100);
            double actualOutput = e.CalculateBill();
            Assert.AreEqual(expectedOutput, actualOutput);

        }

        //Activity - test for commercialbill and domestic bill
        [TestMethod]// unit functional test for CommercialBill
        public void TestCommercialBill()
        {
            double expectedOutput = 75;
            ElectricityBill e = new CommercialBill(100);
            double actualOutput = e.CalculateBill();
            Assert.AreEqual(expectedOutput, actualOutput);

        }

        [TestMethod]// unit functional test for DomesticBill
        public void TestDomesticBill()
        {
            double expectedOutput = 50;
            ElectricityBill e = new DomesticBill(100);
            double actualOutput = e.CalculateBill();
            Assert.AreEqual(expectedOutput, actualOutput);

        }



        [TestMethod]// Equivalance partition test  for IndustrialBill
        public void TestIndustrialBillEqPartition()
        {
            double expectedOutput = -1;
            ElectricityBill e = new IndustialBill(0);
            double actualOutput = e.CalculateBill();
            Assert.AreEqual(expectedOutput, actualOutput);

        }

        [TestMethod]// Equivalance partition test  for CommercialBill
        public void TestCommercialBillEqPartition()
        {
            double expectedOutput = -1;
            ElectricityBill e = new CommercialBill(0);
            double actualOutput = e.CalculateBill();
            Assert.AreEqual(expectedOutput, actualOutput);

        }

        [TestMethod]// Equivalance partition test  for DomesticBill
        public void TestDomesticBillEqPartition()
        {
            double expectedOutput = -1;
            ElectricityBill e = new DomesticBill(0);
            double actualOutput = e.CalculateBill();
            Assert.AreEqual(expectedOutput, actualOutput);

        }



        [TestMethod]// Stress test  for DomesticBill
        public void TestDomesticBillStressTest()
        {
            double expectedOutput = 50000;
            ElectricityBill e = new DomesticBill(100000);
            double actualOutput = e.CalculateBill();
            Assert.AreEqual(expectedOutput, actualOutput);

        }

        [TestMethod]// Stress test  for DomesticBill
        public void TestIndustryBillStressTest()
        {
            double expectedOutput = 100000;
            ElectricityBill e = new IndustialBill(100000);
            double actualOutput = e.CalculateBill();
            Assert.AreEqual(expectedOutput, actualOutput);

        }


        [TestMethod]// Stress test  for Commercial
        public void TestCommercialBillStressTest()
        {
            double expectedOutput = 75000;
            ElectricityBill e = new CommercialBill(100000);
            double actualOutput = e.CalculateBill();
            Assert.AreEqual(expectedOutput, actualOutput);

        }

    }
}
