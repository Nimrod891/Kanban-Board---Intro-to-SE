using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer.BoardPackage
{
    class Column
    {
        private string name;
        private int columnId;
        private int limitNum;
        private int numOfTasks;
        private Dictionary<int, Task> tasks;

        public Column(string name, int columnId)
        {
            this.name = name;
            this.columnId = columnId;
            limitNum = -1;
            numOfTasks = 0;
            tasks = new Dictionary<int, Task>();
        }
        public string GetName()
        {
            return name;
        }

        public int GetColumnId()
        {
            return columnId;
        }

        public int GetLimitNum()
        {
            return limitNum;
        }

        public int GetNumOfTasks()
        {
            return numOfTasks;
        }

        public void SetLimitNum(int limitNum)
        {
            if(limitNum < 0 || limitNum < numOfTasks) // limit num smaller then 
            {
                throw new Exception("Invalid number of limited tasks");
            }
            this.limitNum = limitNum;
        }

        public void AddTask(int taskId, string title, string description, DateTime dueDate)
        {
            if (limitNum == -1 || numOfTasks < limitNum) /// if there's no limit or we didnt over limit task number
            {
                Task t = new Task(taskId, title, description, dueDate);
                this.AddTasksToDict(taskId, t);
            }
            else
            {
                throw new Exception("there are already " + limitNum + " tasks in " + name + "column"); /// "there are already 6 tasks in backlog column" 
            }
        }

        public void DeleteTask(int taskId)
        {
            if (!tasks.Remove(taskId))  /// if removal failed.
            {
                throw new Exception("Removal failed, task Id could not be found");
            }
            numOfTasks = tasks.Count;
        }
        public void AddTasksToDict(int taskId, Task t)
        {
            if(limitNum == -1 || numOfTasks < limitNum)
                {
                tasks.Add(taskId, t);
                numOfTasks = tasks.Count;
            }
            else
            {
                throw new Exception("there are already " + limitNum + " tasks in " + name + "column"); /// example:"there are already 6 tasks in backlog column" 
            }

        }

        public Task GetTaskById(int taskId)
        {
            return tasks[taskId];
        }

        public int GetColumnIdByName(string colName)
        {

            if (this.name.Equals(colName))
            {
                return this.columnId;
            }
            return -1;
        }
    }
}
