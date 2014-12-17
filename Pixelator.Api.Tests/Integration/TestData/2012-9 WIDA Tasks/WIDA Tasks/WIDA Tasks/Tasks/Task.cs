using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIDA.Tasks.Triggers;
using WIDA.Tasks.Conditions;
using WIDA.Tasks.Actions;
using System.Xml;

namespace WIDA.Tasks
{
    public class Task
    {
        public readonly string Name = String.Empty;
        public readonly string GroupName = String.Empty;
        public readonly string Description = String.Empty;
        public bool Active = true;
        public readonly List<Trigger> Triggers = new List<Trigger>();
        public readonly List<Condition> Conditions = new List<Condition>();
        public readonly List<Actions.Action> Actions = new List<Actions.Action>();

        //Initialize task from Strong variables
        public Task(string Name, string GroupName, string Description, List<Trigger> Triggers, List<Condition> Conditions, List<Actions.Action> Actions)
        {
            this.Name = Name;
            this.GroupName = GroupName;
            this.Description = Description;
            foreach (Actions.Action Action in Actions)
            {
                this.Actions.Add(Action);
                Action.AssignTask(this);
            }
            foreach (Condition Condition in Conditions)
            {
                this.Conditions.Add(Condition);
                Condition.AssignTask(this);
            }
            foreach (Trigger Trigger in Triggers)
            {
                this.Triggers.Add(Trigger);
                Trigger.AssignTask(this);
            }
        }

        //Initialize task from Weak variables
        public Task(XmlElement Element)
        {
            if (Element.Name != "Task")
                throw new Exception("Incorrect XML markup");
            this.Name = Element.GetElementsByTagName("Name")[0].InnerText;
            this.GroupName = Element.GetElementsByTagName("GroupName")[0].InnerText;
            this.Description = Element.GetElementsByTagName("Description")[0].InnerText;
            this.Active = Element.GetElementsByTagName("Active")[0].InnerText == "1";
            XmlElement TriggersElement = (XmlElement)Element.GetElementsByTagName("Triggers")[0];
            foreach (XmlElement TriggerElement in TriggersElement.ChildNodes)
            {
                Trigger Trigger = new Trigger(TriggerElement);
                Triggers.Add(Trigger);
                Trigger.AssignTask(this);
            }
            XmlElement ConditionsElement = (XmlElement)Element.GetElementsByTagName("Conditions")[0];
            foreach (XmlElement ConditionElement in ConditionsElement.ChildNodes)
            {
                Condition Condition = new Condition(ConditionElement);
                Conditions.Add(Condition);
                Condition.AssignTask(this);
            }
            XmlElement ActionsElement = (XmlElement)Element.GetElementsByTagName("Actions")[0];
            foreach (XmlElement ActionElement in ActionsElement.ChildNodes)
            {
                Actions.Action Action = new Actions.Action(ActionElement);
                Actions.Add(Action);
                Action.AssignTask(this);
            }
        }

        public XmlElement ToXML(XmlDocument DocArg = null)
        {
            //Create XML representing the data in the task
            XmlDocument Doc = DocArg;
            if (Doc == null)
                Doc = new XmlDocument();
            XmlElement Element = Doc.CreateElement("Task");

            XmlElement NameElement = Doc.CreateElement("Name");
            NameElement.InnerText = this.Name;
            Element.AppendChild(NameElement);

            XmlElement GroupNameElement = Doc.CreateElement("GroupName");
            GroupNameElement.InnerText = this.GroupName;
            Element.AppendChild(GroupNameElement);

            XmlElement DescriptionElement = Doc.CreateElement("Description");
            DescriptionElement.InnerText = this.Description;
            Element.AppendChild(DescriptionElement);

            XmlElement ActiveElement = Doc.CreateElement("Active");
            ActiveElement.InnerText = this.Active ? "1" : "0";
            Element.AppendChild(ActiveElement);

            XmlElement TriggersElement = Doc.CreateElement("Triggers");
            foreach (Trigger Trigger in this.Triggers)
                TriggersElement.AppendChild(Trigger.ToXML(Doc));
            Element.AppendChild(TriggersElement);

            XmlElement ConditionsElement = Doc.CreateElement("Conditions");
            foreach (Condition Condition in this.Conditions)
                ConditionsElement.AppendChild(Condition.ToXML(Doc));
            Element.AppendChild(ConditionsElement);

            XmlElement ActionsElement = Doc.CreateElement("Actions");
            foreach (Actions.Action Action in this.Actions)
                ActionsElement.AppendChild(Action.ToXML(Doc));
            Element.AppendChild(ActionsElement);

            return Element;
        }

        //Beautifly simple code ;)
        public void Trigger()
        {
            //Only proceed if the task is active
            if (Active)
            {
                //Check all conditions are met
                if (this.Conditions.All(Condition => Condition.IsMet()))
                {
                    //If so execute all actions
                    foreach (Actions.Action Action in this.Actions)
                        Action.DoAction();
                }
            }
        }

        public void Dispose()
        {
            this.Active = false;
            foreach (Trigger Trigger in this.Triggers)
            {
                Trigger.Dispose();
            }
            foreach (Condition Condition in this.Conditions)
            {
                Condition.Dispose();
            }
            foreach (Actions.Action Action in this.Actions)
            {
                Action.Dispose();
            }
            this.Triggers.Clear();
            this.Conditions.Clear();
            this.Actions.Clear();
        }
    }
}
