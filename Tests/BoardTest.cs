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
            board.setCreator("hila@gmail.com");
        }

        [TestCase(-1)]
        [TestCase(3)]
        [Test]
        public void addColumnTest_InvalidColumnId_ExceptingException(int columnId)
        {
            //Arrange
            string ValidcolName = "Hila";
            board.SetIsULoggedIn(true);
            Mock<Column> testColumn = new Mock<Column>();

            //Act
            Assert.Catch<Exception>(delegate { board.AddColumn(columnId, ValidcolName); }, "invalid column id exception");
        }

        [TestCase("backlog")]
        [TestCase("in_progress")]
        [TestCase("done")]
        [TestCase("")]
        [TestCase(null)]
        [Test]
        public void addColumnTest_invalidExistingName_ExceptingException(string inValidName)
        {
            //Arrange
            int validColId = 1;
            board.SetIsULoggedIn(true);
            Mock<Column> testColumn = new Mock<Column>();

            //Act
            Assert.Catch<Exception>(delegate { board.AddColumn(validColId, inValidName); }, "inValid name exception");
        }
        [Test]
        public void addColumnTest_notCreatorUser_ExceptingException()
        {
            //Arrange
            int validColId = 1;
            string ValidColName = "Hila";
            board.SetIsULoggedIn(true);
            board.setLoggedInUser("nim@gmail.com");
            Mock<Column> testColumn = new Mock<Column>();

            //Act
            Assert.Catch<Exception>(delegate { board.AddColumn(validColId, ValidColName); }, "inValid name exception");
        }
        [Test]
        public void addColumnTest_validArguments_ExceptingColAddInRightPlace()
        {
            //Arrange
            
            string ValidcolName = "Hila";
            int validColId = 1;
            board.SetIsULoggedIn(true);
            board.setLoggedInUser("hila@gmail.com");
            //Act
            Column c = board.AddColumn(validColId, ValidcolName);
            //Assert
            Assert.AreSame(c, board.getMyColumns()[validColId]);
            Assert.AreSame(backlog.Object, board.getMyColumns()[0]);
            Assert.AreSame(in_progress.Object, board.getMyColumns()[2]);
            Assert.AreSame(done.Object, board.getMyColumns()[3]);
        }

        [TestCase(-1)]
        [TestCase(3)]
        [Test]
        public void MoveColumnRight_InvalidColumnId_ExceptingException(int columnId)
        {
            //Arrange
            board.SetIsULoggedIn(true);
            board.setLoggedInUser("hila@gmail.com");

            //Act
            Assert.Catch<Exception>(delegate { board.MoveColumnRight(columnId); }, "inValid ColID exception");
        }
        [Test]
        public void MoveColumnRight_lastColumnCheck_ExceptingException()
        {
            //Arrange
            int columnOrdinal = 2;
            board.SetIsULoggedIn(true);
            board.setLoggedInUser("hila@gmail.com");
            //Act
            Assert.Catch<Exception>(delegate { board.MoveColumnRight(columnOrdinal); }, "cant move right the last column");
        }

        [Test]
        public void MoveColumnRightTest_validArguments_ExceptingColumnsChangesPlace()
        {
            //Arrange
            int validColId = 1;
            Column c2 = board.getMyColumns()[validColId + 1];
            board.SetIsULoggedIn(true);
            board.setLoggedInUser("hila@gmail.com");
            //Act
            Column c1 = board.MoveColumnRight(validColId);
            //Assert
            Assert.AreSame(c1, board.getMyColumns()[validColId+1]);
            Assert.AreSame(c2, board.getMyColumns()[validColId]);
        }
        [Test]
        public void MoveColumnRightTest_notCreatorUser_ExceptingException()
        {
            //Arrange
            int validColId = 1;
            board.SetIsULoggedIn(true);
            board.setLoggedInUser("nim@gmail.com");
            Mock<Column> testColumn = new Mock<Column>();

            //Act
            Assert.Catch<Exception>(delegate { board.MoveColumnRight(validColId); }, "inValid name exception");
        }

        [TestCase(-1)]
        [TestCase(3)]
        [Test]
        public void LimitTaskTest_InvalidColumnId_ExceptingException(int columnId)
        {
            //Arrange
            int ValidLimitNum = 10;
            board.SetIsULoggedIn(true);
            board.setLoggedInUser("hila@gmail.com");
            //backlog.Setup(m => m.SetLimitNum(10));

            //Act
            Assert.Catch<Exception>(delegate { board.LimitTasks(columnId,ValidLimitNum); }, "invalid column id exception");
        }

        [Test]
        public void LimitTaskTest_notCreatorUser_ExceptingException()
        {
            //Arrange
            int validColId = 1;
            int validLimitNum = 10;
            board.SetIsULoggedIn(true);
            board.setLoggedInUser("nim@gmail.com");
            Mock<Column> testColumn = new Mock<Column>();

            //Act
            Assert.Catch<Exception>(delegate { board.LimitTasks(validColId, validLimitNum); }, "cant add if loggedin user is not creator");
        }
        [Test]
        public void LimitTaskTest_notLoggedInUser_ExceptingException()
        {
            int validColId = 1;
            int ValidLimitNum = 10;
            board.SetIsULoggedIn(false);
            board.setLoggedInUser("hila@gmail.com");
            Assert.Catch<Exception>(delegate { board.LimitTasks(validColId, ValidLimitNum); }, "cant add if loggedin user is not creator");
        }
        [Test]
        public void LimitTaskTest_ValidArguments_notThrowingExceptions()
        {
            int validColId = 1;
            int ValidLimitNum = 10;
            board.SetIsULoggedIn(true);
            board.setLoggedInUser("hila@gmail.com");
            Assert.DoesNotThrow(delegate { board.LimitTasks(validColId, ValidLimitNum); }, "succed limitTask");
        }
    }
}