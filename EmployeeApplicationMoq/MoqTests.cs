using EmployeeApplication.Model;
using NUnit.Framework;
using Moq;
//balaji
//aug6
namespace EmployeeApplicationMoq
{
    public class MoqTests
    {
//bala
        //[Test]
        //public void ActualMethodCallTest()
        //{
        //    //arrange - create
        //    var pf = new EmployeePFDetails(new EmployeeDetails());

        //    //act(test)
        //    double contribution = pf.GetEmployerContribution(2);

        //    //assert(verify)
        //    Assert.AreEqual(5000 * .12, contribution);
        //}

        [Test]
        public void MockAndverifyMethodCallTest()
        {
            //will mock GetEmployeeSalary(empId)

            //arrange - create
            var mock = new Mock<IEmployeeDetails>(); //mock object for EmployeeDetails
            var pf = new EmployeePFDetails(mock.Object);

            //mock the method
            mock.Setup(x => x.GetEmployeeSalary(It.IsAny<int>())).Returns(10000);
            // mock.Setup(x => x.GetEmployeeRole(5)).Returns("Manager");

            //act(test)
            double contribution = pf.GetEmployerContribution(2);

            //assert(verify)
            Assert.AreEqual(10000 * .12, contribution);

            //act
            bool value = pf.IsPFEligible(650);

            //assert
            Assert.True(value);

            //check method call based on number of times
            mock.Verify(x => x.GetEmployeeSalary(It.IsAny<int>()), Times.Exactly(3));
        }

        [Test]
        public void MockAndverifyPropertyCallTest()
        {
            //arrange
            var mock = new Mock<IEmployeeDetails>();
            var pf = new EmployeePFDetails(mock.Object);

            //mock the property
            mock.Setup(x => x.CompanyName).Returns("Sollers");

            //act
            string compName = pf.GetCompanyName();

            pf.PrintCompanyDetail();
            //assert
            Assert.AreEqual("Sollers", compName);
        }

        //stubbing 
        [Test]
        public void MockAndverifyProperty2CallTest()
        {
            //arrange
            var mock = new Mock<IEmployeeDetails>();
            var pf = new EmployeePFDetails(mock.Object);

            mock.SetupAllProperties();
            mock.Object.UniqueCode = 10004;
            mock.Object.CompanyName = "google";

            //mock the property //stubbing
            mock.SetupProperty(x => x.CompanyName, "Google");
            //mock.SetupProperty(x => x.UniqueCode, It.IsAny<int>());
            mock.SetupProperty(x => x.UniqueCode, 90001);

            //act
            string compName = pf.GetCompanyName();

            pf.PrintCompanyDetail();
            //assert
            Assert.AreEqual("Google", compName);
        }


        //loose  moq --> not check whether method or properties mocked explicitly 
        [Test]
        public void MockMethodCallTest()
        {
            //will mock GetEmployeeSalary(empId)

            //arrange - create
            var mock = new Mock<IEmployeeDetails>(); //mock object for EmployeeDetails
            var pf = new EmployeePFDetails(mock.Object);

            //act(test)
            double contribution = pf.GetEmployerContribution(2);

            //assert(verify)
            //Assert.AreEqual(10000 * .12, contribution);

        }

        //and strict moq --> check whether method or properties mocked explicitly otherwise throw error
        [Test]
        [TestCase("peter",5000)]
        [TestCase("john", 5000)]
        public void MockMethodStrictCallTest(string name,double salary)
        {
            //will mock GetEmployeeSalary(empId)

            //arrange - create
            var mock = new Mock<IEmployeeDetails>(MockBehavior.Strict); //mock object for EmployeeDetails
            var pf = new EmployeePFDetails(mock.Object);

            //method and properties should be strictly mocked
            mock.Setup(x => x.GetEmployeeSalary(It.IsAny<int>())).Returns(salary);
            mock.Setup(x => x.GetEmployeeName(It.IsAny<int>())).Returns(name);
            mock.SetupProperty(x => x.CompanyName, "Google");
            mock.SetupProperty(x => x.UniqueCode, 90001);


            //act(test)
            double contribution = pf.GetEmployerContribution(2);

            pf.PrintCompanyDetail();
            //assert(verify)
            //Assert.AreEqual(10000 * .12, contribution);
            System.Console.WriteLine(contribution);
        }
    }
}