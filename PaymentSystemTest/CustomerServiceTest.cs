using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using PaymentSystem.Core.DTOs;
using PaymentSystem.Core.Interfaces;
using PaymentSystem.Core.Services;
using PaymentSystem.Core.Utility;
using PaymentSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PaymentSystemTest
{

    [TestFixture]
    public class CustomerServiceTest
    {
        // Mock dependencies (you need to replace these with your actual mocks)
        private Mock<ILogger<CustomerService>> _loggerMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private IMapper _mapper; // You may use a mock for IMapper as well

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<CustomerService>>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<PaymentSystemProfile>()));
        }

        [Test]
        public async Task GetCustomerAsync_ValidNationalId_ReturnsSuccess()
        {
            // Arrange
            var service = new CustomerService(_mapper, _unitOfWorkMock.Object, _loggerMock.Object);
            var nationalId = "22222222223";

            _unitOfWorkMock.Setup(uow => uow.Customer.GetByNationalId(nationalId))
                .ReturnsAsync(new Customer {NationalId = nationalId, Surname = "okpalaugo", Name = "chuka", CustomerNumber = "08037869935", TransactionHistory = "string" });

            // Act
            var result = await service.GetCustomerAsync(nationalId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo((int)HttpStatusCode.OK));
            Assert.That(result.Data, Is.Not.Null);
            Assert.That(result.Data.NationalId, Is.EqualTo(nationalId));

            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Once);
        }

        [Test]
        public async Task GetCustomerAsync_InvalidNationalId_ReturnsBadRequest()
        {
            // Arrange
            var service = new CustomerService(_mapper, _unitOfWorkMock.Object, _loggerMock.Object);
            var nationalId = string.Empty; // Invalid NationalId

            // Act
            var result = await service.GetCustomerAsync(nationalId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo((int)HttpStatusCode.BadRequest));
            Assert.That(result.Data, Is.Null);

            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [Test]
        public async Task GetCustomerAsync_CustomerNotFound_ReturnsNotFound()
        {
            // Arrange
            var service = new CustomerService(_mapper, _unitOfWorkMock.Object, _loggerMock.Object);
            var nationalId = "NonExistentNationalId";

            _unitOfWorkMock.Setup(uow => uow.Customer.GetByNationalId(nationalId))
                .ReturnsAsync((Customer)null); // Simulate customer not found

            // Act
            var result = await service.GetCustomerAsync(nationalId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo((int)HttpStatusCode.NotFound));
            Assert.That(result.Data, Is.Null);

            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [Test]
        public async Task InsertCustomerAsync_UniqueNationalId_Success()
        {
            // Arrange
            var service = new CustomerService(_mapper, _unitOfWorkMock.Object, _loggerMock.Object);
            var customerDetails = new CustomerRequestDto { NationalId = "1234567890", /* other properties */ };
            _unitOfWorkMock.Setup(uow => uow.Customer.CountAsync(It.IsAny<Expression<Func<Customer, bool>>>())).Returns(0);

            // Act
            var result = await service.InsertCustomerAsync(customerDetails);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo((int)HttpStatusCode.Created));
            Assert.That(result.Data, Is.True);
            _unitOfWorkMock.Verify(uow => uow.Customer.InsertAsync(It.IsAny<Customer>()), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Save(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Rollback(), Times.Never);
        }

        [Test]
        public async Task InsertCustomerAsync_DuplicateNationalId_Failure()
        {
            // Arrange
            var service = new CustomerService(_mapper, _unitOfWorkMock.Object, _loggerMock.Object);
            var customerDetails = new CustomerRequestDto { NationalId = "1234567890", /* other properties */ };
            _unitOfWorkMock.Setup(uow => uow.Customer.CountAsync(It.IsAny<Expression<Func<Customer, bool>>>())).Returns(1);

            // Act
            var result = await service.InsertCustomerAsync(customerDetails);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo((int)HttpStatusCode.NotFound));
            Assert.That(result.Data, Is.False);
            _unitOfWorkMock.Verify(uow => uow.Customer.InsertAsync(It.IsAny<Customer>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Save(), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Rollback(), Times.Never);
        }

        [Test]
        public async Task DeleteCustomerAsync_CustomerNotFound_ReturnsNotFoundResponse()
        {
            // Arrange
            var service = new CustomerService(_mapper, _unitOfWorkMock.Object, _loggerMock.Object);
            var nationalId = "123456789";

            _unitOfWorkMock.Setup(uow => uow.Customer.CountAsync(It.IsAny<Expression<Func<Customer, bool>>>()))
                .Returns(0);

            // Act
            var result = await service.DeleteCustomerAsync(nationalId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo((int)HttpStatusCode.NotFound));
            Assert.That(result.Data, Is.False);
            _unitOfWorkMock.Verify(uow => uow.Customer.DeleteCustomerByNationalId(It.IsAny<string>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Save(), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Rollback(), Times.Never);
        }

        [Test]
        public async Task DeleteCustomerAsync_CustomerFound_DeletesCustomerAndReturnsOkResponse()
        {
            // Arrange
            var service = new CustomerService(_mapper, _unitOfWorkMock.Object, _loggerMock.Object);
            var nationalId = "123456789";

            _unitOfWorkMock.Setup(uow => uow.Customer.CountAsync(It.IsAny<Expression<Func<Customer, bool>>>()))
                .Returns(1);

            // Act
            var result = await service.DeleteCustomerAsync(nationalId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo((int)HttpStatusCode.OK));
            Assert.That(result.Data, Is.True);
            _unitOfWorkMock.Verify(uow => uow.Customer.DeleteCustomerByNationalId(nationalId), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Save(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Rollback(), Times.Never);
        }
    }
}