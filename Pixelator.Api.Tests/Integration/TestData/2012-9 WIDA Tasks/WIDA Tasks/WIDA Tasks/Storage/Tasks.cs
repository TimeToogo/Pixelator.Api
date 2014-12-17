using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIDA.Tasks;
using System.Xml;

namespace WIDA.Storage
{
    public class Tasks
    {
        public List<Task> TaskList = new List<Task>();

        public int TotalCount()
        {
            return TaskList.Count;
        }

        public void LoadXML(XmlDocument Doc)
        {
            foreach (XmlElement Element in Doc.GetElementsByTagName("Task"))
                TaskList.Add(this.GetTaskNonConflictingName(new Task(Element)));
        }

        public XmlDocument ToXML()
        {
            XmlDocument Doc = new XmlDocument();
            XmlElement Element = Doc.CreateElement("Tasks");
            foreach (Task Task in TaskList)
                Element.AppendChild(Task.ToXML(Doc));
            Doc.AppendChild(Element);

            return Doc;
        }

        public Task GetTask(string GroupName, string Name)
        {
            foreach (Task Task in this.TaskList)
                if (Task.GroupName == GroupName)
                    if (Task.Name == Name)
                        return Task;
            return null;
        }

        //This function just renames duplicates
        public Task GetTaskNonConflictingName(Task Task)
        {
            if (!TaskNameExists(Task.GroupName, Task.Name))
                return Task;
            string GroupName = Task.GroupName;
            string Name = Task.Name;
            int Count = 1;
            while (TaskNameExists(GroupName, Name + "(" + Count.ToString() + ")"))
                Count++;

            return new Task(Name + "(" + Count.ToString() + ")", Task.GroupName, Task.Description, Task.Triggers, Task.Conditions, Task.Actions);
        }

        public bool TaskNameExists(string GroupName, string Name)
        {
            foreach (Task Task in this.TaskList)
                if (Task.GroupName == GroupName)
                    if (Task.Name == Name)
                        return true;
            return false;
        }

        public string[] GetTasksGroupNames()
        {
            List<string> GroupNames = new List<string>();
            foreach (Task Task in this.TaskList)
                if (!GroupNames.Contains(Task.GroupName))
                    GroupNames.Add(Task.GroupName);

            return GroupNames.ToArray();
        }

        public Task[] GetTasksFromGroupName(string GroupName)
        {
            List<Task> Tasks = new List<Task>();
            foreach (Task Task in this.TaskList)
                if (Task.GroupName == GroupName)
                    Tasks.Add(Task);

            return Tasks.ToArray();
        }
    }
}
