using DeviceManager.Api.Data;
using DeviceManager.Api.Data.Management;
using Moq;
using System;
using Xunit;

namespace DeviceManager.Api.UnitTests.Data.Management
{
    public class UnitOfWorkTests
    {
        [Fact]
        public void Commit_WillCallSaveChangesOnce()
        {
            // Arrange
            var mockContext = new Mock<IDbContext>();
            var contextFactory = new Mock<IContextFactory>();
            contextFactory.Setup(cf => cf.DbContext).Returns(mockContext.Object);

            var unitOfWork = new UnitOfWork(contextFactory.Object);

            // Act
            unitOfWork.Commit();

            //// Assert
            mockContext.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}
