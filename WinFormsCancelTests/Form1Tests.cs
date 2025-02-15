using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WinFormsCancel;

namespace WinFormsCancelTests
{
    [TestClass]
    public class Form1Tests
    {
        [TestMethod]
        public void CalculateRandomWinner_ReturnsValueWithinRange()
        {
            // Arrange
            var form = new Form1();
            form.NombreAlumnes = 10;

            // Act
            var result = form.CalculateRandomWinner();

            // Assert
            Assert.IsTrue(result >= 1 && result <= 10);
        }
    }
}
