using Microsoft.VisualStudio.TestTools.UnitTesting;
using XControls.Helpers.Extensions;

namespace XControls.Helpers.UnitTest
{
    [TestClass]
    public class MathOperationsUnitTest1
    {
        [TestMethod]
        public void GetMinAndMaxValueMethod()
        {
            var arr = new double[] {1,3,5,10};
            var result = arr.GetMinAndMaxValue();
            Assert.AreEqual(result.min, 1);
            Assert.AreEqual(result.max, 10);
        }      
        [TestMethod]
        public void GetScalefactorOffsetMethod_Double()
        {
            var min = 0.0;
            var max = 1000.0;
            var absoluteMaxValue = byte.MaxValue;
            var result = MathHelpers.GetScalefactorOffset(min, max, absoluteMaxValue);

            var right =  ((max - min) / absoluteMaxValue, min);
            Assert.AreEqual(result.sf, right.Item1);
            Assert.AreEqual(result.offset, right.Item2);
        }
        [TestMethod]
        public void GetScalefactorOffsetMethod_Float()
        {
            var min = 0.0f;
            var max = 1000.0f;
            var absoluteMaxValue = byte.MaxValue;
            var result = MathHelpers.GetScalefactorOffset(min, max, absoluteMaxValue);

            var right = ((max - min) / absoluteMaxValue, min);
            Assert.AreEqual(result.sf, right.Item1);
            Assert.AreEqual(result.offset, right.Item2);
        }
        [TestMethod]
        public void GetScalefactorOffsetMethod_Int()
        {
            var min = 0;
            var max = 1000;
            var absoluteMaxValue = byte.MaxValue;
            var result = MathHelpers.GetScalefactorOffset(min, max, absoluteMaxValue);

            var right = ((max - min) / absoluteMaxValue, min);
            Assert.AreEqual(result.sf, right.Item1);
            Assert.AreEqual(result.offset, right.Item2);
        }
    }
}