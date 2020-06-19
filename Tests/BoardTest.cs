using NUnit.Framework;
using IntroSE.Kanban.Backend.BusinessLayer.BoardPackage;
using Moq;
using System;

namespace Tests
{
    public class Tests
    {
        Mock<Column> backlog;
        Mock<Column> in_progress;
        Mock<Column> done;
        Board board;
        [SetUp]
        public void Setup()
        {
            board = new Board();
            backlog = new Mock<Column>("backlog",0);
            in_progress = new Mock<Column>("in_progress",1);
            done = new Mock<Column>("done",2);
            board.addColumnToDict(backlog.Object);
            board.addColumnToDict(in_progress.Object);
            board.addColumnToDict(done.Object);
        }
        [TestCase(-1)]
        [TestCase(3)]
        [Test]
        public void addColumnTest_InvalidColumnId(int columnId)
        {
            //Arrange
            string ValidcolName = "Hila";
            Mock<Column> testColumn = new Mock<Column>();

            //Act
            try
            {
                board.AddColumn(columnId, ValidcolName);
                Assert.Fail("invalid column id exception");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}