using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cuni.Arithmetics.FixedPoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuni.Arithmetics.FixedPoint.Tests
{
    [TestClass()]
    public class FixedToStringTestClass
    {
        [TestMethod()]
        public void ToStringTest()
        {
            int a = 5;
            var aFixed = new Fixed<Q24_8>(a);
            Assert.AreEqual(aFixed.ToString(), a.ToString());
            int b = -45;
            var bFixed = new Fixed<Q24_8>(b);

            var resultFixed = aFixed.Divide(bFixed);
            Assert.AreEqual(Math.Round(resultFixed, FixedOperationsTestClass.roundDeximalCount).ToString(),
                Math.Round(a / (double)b, FixedOperationsTestClass.roundDeximalCount).ToString());

        }
    }
    [TestClass()]
    public class FixedOperationsTestClass
    {
        public static int roundDeximalCount = 2;
        static List<(int, int)> Test_Data = new List<(int, int)>() {
            (1,1), (5,0), (0,0), (0,5),
            (5,-7), (-7,5), (-5,-7), (5,7)};

        [TestMethod]
        public void Test_All_Q32_Types()
        {
            Test_All_Operations<Q8_24>(Test_Data);
            Test_All_Operations<Q16_16>(Test_Data);
            Test_All_Operations<Q24_8>(Test_Data);
        }

        public void Test_All_Operations<T>(IReadOnlyCollection<(int,int)> TestData) where T : IArithmetics, new()
        {
            foreach (var data in TestData)
            {
                Test_Add<T>(data.Item1, data.Item2);
                Test_Divide<T>(data.Item1, data.Item2);
                Test_Subtract<T>(data.Item1, data.Item2);
                Test_Multiply<T>(data.Item1, data.Item2);
            }
        }        

        public Fixed<T> Test_Add<T>(int a, int b) where T : IArithmetics, new()
        {
            var aFixed = new Fixed<T>(a);
            var bFixed = new Fixed<T>(b);
            var resultFixed = aFixed.Add(bFixed);
            Assert.AreEqual(Math.Round(b + (double)a, roundDeximalCount),
                Math.Round(resultFixed, roundDeximalCount));
            return resultFixed;
        }

        public Fixed<T> Test_Subtract<T>(int a, int b) where T : IArithmetics, new()
        {
            var aFixed = new Fixed<T>(a);
            var bFixed = new Fixed<T>(b);
            var resultFixed = aFixed.Subtract(bFixed);
            Assert.AreEqual(Math.Round((double)(a - b), roundDeximalCount),
                Math.Round(resultFixed, roundDeximalCount));
            return resultFixed;
        }
        public Fixed<T> Test_Multiply<T>(int a, int b) where T : IArithmetics, new()
        {
            var aFixed = new Fixed<T>(a);
            var bFixed = new Fixed<T>(b);
            var resultFixed = aFixed.Multiply(bFixed);
            Assert.AreEqual(Math.Round((double)(a * b), roundDeximalCount),
                Math.Round(resultFixed, roundDeximalCount));
            return resultFixed;
        }
        public Fixed<T> Test_Divide<T>(int a, int b) where T : IArithmetics, new()
        {            
            try
            {
                var aFixed = new Fixed<T>(a);
                var bFixed = new Fixed<T>(b);
                var resultFixed = aFixed.Divide(bFixed);
                Assert.AreEqual(Math.Round(a / (double)b, roundDeximalCount),
                    Math.Round(resultFixed, roundDeximalCount));
                return resultFixed;
            }
            catch(DivideByZeroException)
               when (b == 0){}
            catch (Exception)
            {
                Assert.Fail();
            }
            return new Fixed<T>(default(int));
        }
        
        [TestMethod()]
        public void SubtractingQ24_08()
        {
            int a = 5;
            int b = 7;
            var aFixed = new Fixed<Q24_8>(a);
            var bFixed = new Fixed<Q24_8>(b);
            var resultFixed = aFixed.Subtract(bFixed);
            Assert.AreEqual(a - (double)b, resultFixed);
        }

        [TestMethod()]
        public void DivisionMinusTests()
        {
            Test_Divide<Q24_8>(5, -7);
            Test_Divide<Q16_16>(5, -7);
            Test_Divide<Q8_24>(5, -7);

            Test_Divide<Q24_8>(-5, -7);
            Test_Divide<Q16_16>(-5, -7);
            Test_Divide<Q8_24>(-5, -7);
        }

        [TestMethod()]
        public void DivideByLowerMinusFixedQ24_08()
        {
            int a = 5;
            int b = 7;
            var aFixed = new Fixed<Q16_16>(a);
            var bFixed = new Fixed<Q16_16>(b);
            var subtractResult = aFixed.Subtract(bFixed);
            // 5 - 7 
            Assert.AreEqual(a - (double)b,
                Math.Round((double)subtractResult, roundDeximalCount));


            // 5/-2
            var divideResult = aFixed.Divide(subtractResult);
            Assert.AreEqual(Math.Round((double)divideResult, roundDeximalCount),
                Math.Round((double)a / -2, roundDeximalCount));
        }

        [TestMethod()]
        public void DivideByLowerPlusFixedQ16_16()
        {
            int a = 5;
            int b = 7;
            var aFixed = new Fixed<Q16_16>(a);
            var bFixed = new Fixed<Q16_16>(b);

            // 5/7
            var divideResult = aFixed.Divide(bFixed);
            Assert.AreEqual(Math.Round((double)divideResult, roundDeximalCount),
                Math.Round(a / (double)b, roundDeximalCount));
        }

        [TestMethod()]
        public void DivideByLowerMinusFixedQ8_24()
        {
            int a = 5;
            int b = 7;
            var aFixed = new Fixed<Q8_24>(a);
            var bFixed = new Fixed<Q8_24>(b);
            // 5 - 7 = -2 
            var subtractResult = aFixed.Subtract(bFixed);
            // 5/ -2 = -2.5
            var divideResult = aFixed.Divide(subtractResult);
            Assert.AreEqual(Math.Round(a / subtractResult, roundDeximalCount),
                Math.Round(divideResult, roundDeximalCount));
        }

        [TestMethod()]
        public void DivideSameNumbersQ8_24()
        {
            int a = 7;
            int b = 7;
            var aFixed = new Fixed<Q8_24>(a);
            var bFixed = new Fixed<Q8_24>(b);

            // 5/ -2 = -2.5
            var divideResult = aFixed.Divide(bFixed);
            Assert.AreEqual(1, Math.Round(divideResult, roundDeximalCount));
        }

        [TestMethod()]
        public void DivideSameNumbersMinusQ8_24()
        {
            int a = 7;

            var aFixed = new Fixed<Q8_24>(a);
            var zeroFixed = new Fixed<Q8_24>(0);

            var negAFixed = zeroFixed.Subtract(aFixed);
            // 0 - 7= -7
            var divideResult = aFixed.Divide(negAFixed);
            Assert.AreEqual(-1, Math.Round(divideResult, roundDeximalCount));
        }

        [TestMethod()]
        public void DivideSameNumberQ8_24()
        {
            Test_Divide<Q8_24>(7, -7);
            Test_Divide<Q8_24>(-7, -7);
            Test_Divide<Q8_24>(7, 7);
            Test_Divide<Q8_24>(-7, 7);
        }

        [TestMethod()]
        public void DivideSameNumbersMinusMinusQ8_24()
        {
            Test_Divide<Q8_24>(-7, 7);
        }

        [TestMethod()]
        public void MultByZeroQ8_24()
        {
            int a = 120;
            var zero = Test_Add<Q24_8>(-5, 5);
            var aFixed = new Fixed<Q24_8>(a);
            var multResult = zero.Multiply(aFixed);
            Assert.AreEqual(0, Math.Round(multResult, roundDeximalCount));
        }

        [TestMethod()]
        public void MultTwoMinusesQ8_24()
        {
            var a = Test_Multiply<Q24_8>(-12, -5);
            var b = Test_Multiply<Q24_8>(-12, 5);
            var c = Test_Multiply<Q24_8>(12, -5);
        }

        [TestMethod()]
        public void MultBigQ8_24()
        {
            // Too big numbers
            int a = 7;
            int b = 50000000;
            var aFixed = new Fixed<Q8_24>(a);
            var bFixed = new Fixed<Q8_24>(b);

            var negAFixed = bFixed.Multiply(aFixed);
            //Assert.AreEqual(a * b, Math.Round(negAFixed, roundDeximalCount));
            Assert.Inconclusive();
        }

        [TestMethod()]
        public void MultSmallNumbersQ8_24()
        {
            var xFixed = Test_Divide<Q24_8>(15, 10);
            var aFixed = Test_Divide<Q24_8>(1, 15);
            var bFixed = Test_Divide<Q24_8>(-2, 5);
            var resultFixed = aFixed.Multiply(bFixed);
            Assert.AreEqual(Math.Round(aFixed * bFixed, roundDeximalCount),
                Math.Round(resultFixed, roundDeximalCount));
        }
    }
}