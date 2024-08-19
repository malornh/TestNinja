using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class HouseKeeperServiceTests
    {
        private HousekeeperService _service;
        private Mock<IStatementGenerator> _statementGenerator;
        private Mock<IEmailSender> _emailSender;
        private Mock<IXtraMessageBox> _messageBox;
        private DateTime _Statementdate = new DateTime(2017, 1, 1);
        private Housekeeper _houseKeeper;
        private string _statementFileName;

        [SetUp]
        public void SetUp()
        {
            _houseKeeper = new Housekeeper { Email = "a", FullName = "b", Oid = 1, StatementEmailBody = "c" };
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(uow => uow.Query<Housekeeper>()).Returns(new List<Housekeeper>
            {
               _houseKeeper
            }.AsQueryable());

            _statementFileName = "fileName";
            _statementGenerator = new Mock<IStatementGenerator>();
            _statementGenerator
                .Setup(sg => sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _Statementdate)
                ).Returns(() => _statementFileName);

            _emailSender = new Mock<IEmailSender>();
            _messageBox = new Mock<IXtraMessageBox>();

            _service = new HousekeeperService(
                unitOfWork.Object,
                _statementGenerator.Object,
                _emailSender.Object,
                _messageBox.Object);

        }

        [Test]
        public void SendStatementEmails_WhenCalled_GenerateStatements()
        {
            _service.SendStatementEmails(_Statementdate);

            _statementGenerator.Verify(sg => sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _Statementdate));
        }

        [Test]
        [TestCase(null)]
        [TestCase(" ")]
        [TestCase("")]
        public void SendStatementEmails_HousekeepersEmailIsIncorrect_ShouldNotGenerateStatements(string email)
        {
            _houseKeeper.Email = email;

            _service.SendStatementEmails(_Statementdate);

            _statementGenerator.Verify(sg => sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _Statementdate),
                Times.Never);
        }

        private void VerifyMessageBoxDisplayed()
        {
            _messageBox.Verify(b => b.Show(It.IsAny<string>(), It.IsAny<string>(), MessageBoxButtons.OK));
        }

        private void VerifyEmailSent()
        {
            _emailSender.Verify(es => es.EmailFile(
                _houseKeeper.Email,
                _houseKeeper.StatementEmailBody,
                _statementFileName,
                It.IsAny<string>()));
        }

        private void VerifyEmailNotSent()
        {
            _emailSender.Verify(es => es.EmailFile(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()),
                Times.Never);
        }
    }
}
