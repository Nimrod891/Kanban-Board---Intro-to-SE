using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace IntroSE.Kanban.Backend.BusinessLayer.BoardPackage
{
    class Column
    {
        private string name;
        private int columnId;
        private int limitNum;
        private int numOfTasks;
        private Dictionary<int, Task> tasks;
        private ReadOnlyDictionary<int, Task> readOnlyDict;

        public Column(string name, int columnId)
        {
            this.name = name;
            this.columnId = columnId;
            limitNum = 100;
            numOfTasks = 0;
            tasks = new Dictionary<int, Task>();
        }
        public  Column(string email, string name, int columnId, int limitNum, int numOfTasks)
        {
            this.name = name;
            this.columnId = columnId;
            this.limitNum = limitNum;
            this.numOfTasks = numOfTasks;
            tasks = new Dictionary<int, Task>();
            
        }
        public Column()
        {

        }
        public string GetName()
        {
            return name;
        }
        public void setName(string name)
        {
            this.name = name;
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
        public void setColumnId(int columnId)
        {
            this.columnId = columnId;
        }
       public Dictionary<int,Task> getMyTasks()
        {
            return this.tasks;
        }
        public void removeMyTasks()
        {
            tasks.Clear();
        }
        public void SetLimitNum(int limitNum)
        {
            if (limitNum == -1)
            {
                this.limitNum = limitNum;
            }
            if(limitNum < 0 || limitNum <= numOfTasks) // limit num smaller then 
            {
                throw new Exception("Invalid number of limited tasks");
            }
            this.limitNum = limitNum;
        }

        public Task AddTask(int taskId, string title, string description, DateTime dueDate)
        {
            if (!(this.columnId == 0))
            {
                throw new Exception("You can only add new task to backlog column");
            }
            if (limitNum != -1 && numOfTasks >= limitNum) /// if there's no limit or we didnt over limit task number
            {
                throw new Exception("there are already " + limitNum + " tasks in " + name + "column"); /// "there are already 6 tasks in backlog column"
                
            }
            Task t = new Task(taskId, title, description, dueDate);
            this.AddTasksToDict(taskId, t);
            return t;
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
            if (limitNum != -1 && numOfTasks >= limitNum)
            {
                throw new Exception("there are already " + limitNum + " tasks in " + name + "column"); /// example:"there are already 6 tasks in backlog column"  
            }
            tasks.Add(taskId, t);
            numOfTasks = tasks.Count;

        }

        public Task GetTaskById(int taskId)
        {
            if (!tasks.ContainsKey(taskId))
            {
                throw new Exception("Task does not exisit");
            }
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
        public Dictionary<int,Task> getTasksDict()
        {
            return this.tasks;
        }
        public ReadOnlyCollection<ServiceLayer.Task> GetMyTasks()
        {
            List<ServiceLayer.Task> listTasks = new List<ServiceLayer.Task>();
            foreach (KeyValuePair<int,Task> t in tasks)
            {
                ServiceLayer.Task task = new ServiceLayer.Task(t.Value.GetTaskId(), t.Value.GetCreationDate(), t.Value.GetDueDate(), t.Value.GetTitle(), t.Value.GetDescription());
                listTasks.Add(task);
            }
            return listTasks.AsReadOnly();
        }

        public void AssignTask(int taskId, string emailAssignee)
        {
            tasks[taskId].setEmailAssignee(emailAssignee);
        }
    }
}
