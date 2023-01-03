using Microsoft.VisualStudio.TestTools.UnitTesting;
using XControls.Utilities.Helpers.Extensions;

namespace XControls.UnitTest
{
    [TestClass]
    public class MathOperationsUnitTest1
    {
        [TestMethod]
        public void GetMinAndMaxValueMethod()
        {
            var arr = new double[] { 1, 3, 5, 10 };
            var (min, max) = arr.GetMinAndMaxValue();
            Assert.AreEqual(min, 1);
            Assert.AreEqual(max, 10);
        }
        [TestMethod]
        public void GetScalefactorOffsetMethod_Double()
        {
            var min = 0.0;
            var max = 1000.0;
            var absoluteMaxValue = byte.MaxValue;
            var (sf, offset) = MathHelpers.GetScalefactorOffset(min, max, absoluteMaxValue);

            var (sf_r, offset_r) = ((max - min) / absoluteMaxValue, min);
            Assert.AreEqual(sf, sf_r);
            Assert.AreEqual(offset, offset_r);
        }
        [TestMethod]
        public void GetScalefactorOffsetMethod_Float()
        {
            var min = 0.0f;
            var max = 1000.0f;
            var absoluteMaxValue = byte.MaxValue;
            var (sf, offset) = MathHelpers.GetScalefactorOffset(min, max, absoluteMaxValue);

            var (sf_r, offset_r) = ((max - min) / absoluteMaxValue, min);
            Assert.AreEqual(sf, sf_r);
            Assert.AreEqual(offset, offset_r);
        }
        [TestMethod]
        public void GetScalefactorOffsetMethod_Int()
        {
            var min = 0;
            var max = 1000;
            var absoluteMaxValue = byte.MaxValue;
            var (sf, offset) = MathHelpers.GetScalefactorOffset(min, max, absoluteMaxValue);

            var (sf_r, offset_r) = ((max - min) / absoluteMaxValue, min);
            Assert.AreEqual(sf, sf_r);
            Assert.AreEqual(offset, offset_r);
        }
    }
}