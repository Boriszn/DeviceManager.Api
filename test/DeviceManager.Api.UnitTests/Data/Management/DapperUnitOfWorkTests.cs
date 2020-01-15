using DeviceManager.Api.Data.Management.Dapper;
using Moq;
using System.Data;
using Xunit;

namespace DeviceManager.Api.UnitTests.Data.Management
{
    public class DapperUnitOfWorkTests
    {
        [Fact]
        public void Commit_WillCallSaveChangesOnce()
        {
            // Arrange
            var mockConnection = new Mock<IDbConnection>();
            var mockTransaction = new Mock<IDbTransaction>();
            var mockConnectionFactory = new Mock<IConnectionFactory>();
            
            mockConnection.Setup(mc => mc.BeginTransaction()).Returns(mockTransaction.Object);

            mockConnectionFactory.Setup(cf => cf.Connection).Returns(mockConnection.Object);

            var dapperUnitOfWork = new DapperUnitOfWork(mockConnectionFactory.Object);

            // Act
            dapperUnitOfWork.BeginTransaction();

            dapperUnitOfWork.Commit();

            //// Assert
            mockTransaction.Verify(x => x.Commit(), Times.Once);
        }
    }
}
