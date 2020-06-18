using IntroSE.Kanban.Backend.BusinessLayer.BoardPackage;
using Moq;
using NUnit.Framework;
using System;

namespace Tests
{
    
    public class TestBoard
    {
        Board board;
        [SetUp]
        public void Setup()
        {
            Mock<IUserController>
            board = new Board(email);
        }
        [TestCase(-1)]
        [TestCase(3)]
        [Test]
        public void TestLimitTasks_invalidColumnid(int invalidColumnId)
        {
            Exception exc = null;
            //Act
            try
            {
                board.GetColumnById(invalidColumnId);
            }
            catch(Exception e)
            {
                exc = e;
            }
            Assert.IsNull(exc);
        }
    }
}