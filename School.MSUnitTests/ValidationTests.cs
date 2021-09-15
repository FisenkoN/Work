using Microsoft.VisualStudio.TestTools.UnitTesting;
using School.UI;

namespace School.MSUnitTests
{
    [TestClass]
    public class ValidationTests
    {
        [DataRow("Nazar")]
        [DataRow("Volodia")]
        [DataRow("Ivan")]
        [DataRow("IO")]
        [TestMethod]
        public void FirstOrLastNameReturnTrue(string name)
        {
            //Arrange

            //Act
            var result = Validation.FirstOrLastName(name);

            //Assert
            Assert.IsNotNull(name);
            Assert.IsTrue(result);
        }
        
        [DataRow(null)]
        [DataRow("l")]
        [DataRow("12")]
        [DataRow("qwdqd112")]
        [DataRow("qwdq d112")]
        [DataRow("qwdq d11qedeqwqqefdcqwefqwefdqefqfe2")]
        [DataRow("Nazarsldhfhtndhvhgydwdadw")]
        [TestMethod]
        public void FirstOrLastNameReturnFalse(string name)
        {
            //Act
            var result = Validation.FirstOrLastName(name);

            //Assert
            Assert.IsFalse(result);
        }

        [DataRow("A")]
        [DataRow("11A")]
        [DataRow("B")]
        [TestMethod]
        public void ClassNameReturnTrue(string name)
        {
            //Act
            var result = Validation.ClassName(name);
            
            //Assert
            Assert.IsTrue(result);
        }
        
        [DataRow("Math")]
        [DataRow("History")]
        [TestMethod]
        public void SubjectNameReturnTrue(string name)
        {
            //Act
            var result = Validation.SubjectName(name);
            
            //Assert
            Assert.IsTrue(result);
        }
        
        [DataRow("")]
        [DataRow("dddd")]
        [DataRow("13122")]
        [DataRow("")]
        [DataRow("dd awddd")]
        [DataRow(null)]
        [TestMethod]
        public void ClassNameReturnFalse(string name)
        {
            //Act
            var result = Validation.ClassName(name);
            
            //Assert
            Assert.IsFalse(result);
        }
        
        [DataRow("")]
        [DataRow("awdadwawdqawdacaedfweffaergerergeargergweggwef")]
        [DataRow("aewew3")]
        [DataRow(null)]
        [DataRow("dd 1awddd")]
        [DataRow("dw")]
        [DataRow("IT")]
        [TestMethod]
        public void SubjectNameReturnFalse(string name)
        {
            //Act
            var result = Validation.SubjectName(name);
            
            //Assert
            Assert.IsFalse(result);
        }
    }
}