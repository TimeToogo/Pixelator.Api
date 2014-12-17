using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIDA.Tasks.Triggers;
using WIDA.Tasks.Conditions;
using System.Xml;

namespace WIDA.Storage
{
    public class Definitions
    {
        public List<Trigger> Triggers = new List<Trigger>();
        public List<Condition> Conditions = new List<Condition>();
        public List<WIDA.Tasks.Actions.Action> Actions = new List<WIDA.Tasks.Actions.Action>();

        //Load all triggers, conditions and actions from the supplied XML doc
        public void LoadXML(XmlDocument Doc)
        {
            XmlElement TriggersElement = (XmlElement)Doc.GetElementsByTagName("Triggers")[0];
            foreach (XmlElement Element in TriggersElement.ChildNodes)
            {
                Trigger Trigger = this.GetTriggerNonConflictingName(new Trigger(Element));
                Triggers.Add(Trigger);
            }

            XmlElement ConditionsElement = (XmlElement)Doc.GetElementsByTagName("Conditions")[0];
            foreach (XmlElement Element in ConditionsElement.ChildNodes)
            {
                Condition Condition = this.GetConditionNonConflictingName(new Condition(Element));
                Conditions.Add(Condition);
            }

            XmlElement ActionsElement = (XmlElement)Doc.GetElementsByTagName("Actions")[0];
            foreach (XmlElement Element in ActionsElement.ChildNodes)
            {
                WIDA.Tasks.Actions.Action Action = this.GetActionNonConflictingName(new WIDA.Tasks.Actions.Action(Element));
                Actions.Add(Action);
            }
        }

        //Returns a XML doc from the triggers, conditions and actions
        public XmlDocument ToXML()
        {
            XmlDocument Doc = new XmlDocument();

            XmlElement ParentElement = Doc.CreateElement("Definitions");

            XmlElement TriggersElement = Doc.CreateElement("Triggers");
            foreach (Trigger Trigger in Triggers)
                TriggersElement.AppendChild(Trigger.ToXML(Doc));

            XmlElement ConditionsElement = Doc.CreateElement("Conditions");
            foreach (Condition Condition in Conditions)
                ConditionsElement.AppendChild(Condition.ToXML(Doc));

            XmlElement ActionsElement = Doc.CreateElement("Actions");
            foreach (WIDA.Tasks.Actions.Action Action in Actions)
                ActionsElement.AppendChild(Action.ToXML(Doc));

            ParentElement.AppendChild(TriggersElement);
            ParentElement.AppendChild(ConditionsElement);
            ParentElement.AppendChild(ActionsElement);
            Doc.AppendChild(ParentElement);

            return Doc;
        }

        public int TotalCount()
        {
            return this.Triggers.Count + this.Conditions.Count + this.Actions.Count;
        }

        public Trigger GetTrigger(string GroupName, string Name)
        {
            foreach (Trigger Trigger in this.Triggers)
                if (Trigger.GroupName == GroupName)
                    if (Trigger.Name == Name)
                        return Trigger;
            return null;
        }

        public Condition GetCondition(string GroupName, string Name)
        {
            foreach (Condition Condition in this.Conditions)
                if (Condition.GroupName == GroupName)
                    if (Condition.Name == Name)
                        return Condition;
            return null;
        }

        public WIDA.Tasks.Actions.Action GetAction(string GroupName, string Name)
        {
            foreach (WIDA.Tasks.Actions.Action Action in this.Actions)
                if (Action.GroupName == GroupName)
                    if (Action.Name == Name)
                        return Action;
            return null;
        }

        public Trigger GetTriggerNonConflictingName(Trigger Trigger)
        {
            if (!TriggerNameExists(Trigger.GroupName, Trigger.Name))
                return Trigger;
            string GroupName = Trigger.GroupName;
            string Name = Trigger.Name;
            int Count = 1;
            while (TriggerNameExists(GroupName, Name + "(" + Count.ToString() + ")"))
                Count++;

            return new Trigger(Name + "(" + Count.ToString() + ")", Trigger.GroupName, Trigger.Description, Trigger.Source, Trigger.NeedsParams, Trigger.FormSource, Trigger.Args);
        }

        public bool TriggerNameExists(string GroupName, string Name)
        {
            foreach (Trigger Trigger in this.Triggers)
                if (Trigger.GroupName == GroupName)
                    if (Trigger.Name == Name)
                        return true;
            return false;
        }

        public Condition GetConditionNonConflictingName(Condition Condition)
        {
            if (!ConditionNameExists(Condition.GroupName, Condition.Name))
                return Condition;
            string GroupName = Condition.GroupName;
            string Name = Condition.Name;
            int Count = 1;
            while (ConditionNameExists(GroupName, Name + "(" + Count.ToString() + ")"))
                Count++;

            return new Condition(Name + "(" + Count.ToString() + ")", Condition.GroupName, Condition.Description, Condition.Source, Condition.NeedsParams, Condition.FormSource, Condition.Args);
        }

        public bool ConditionNameExists(string GroupName, string Name)
        {
            foreach (Condition Condition in this.Conditions)
                if (Condition.GroupName == GroupName)
                    if (Condition.Name == Name)
                        return true;
            return false;
        }

        public WIDA.Tasks.Actions.Action GetActionNonConflictingName(WIDA.Tasks.Actions.Action Action)
        {
            if (!ActionNameExists(Action.GroupName, Action.Name))
                return Action;
            string GroupName = Action.GroupName;
            string Name = Action.Name;
            int Count = 1;
            while (ActionNameExists(GroupName, Name + "(" + Count.ToString() + ")"))
                Count++;

            return new WIDA.Tasks.Actions.Action(Name + "(" + Count.ToString() + ")", Action.GroupName, Action.Description, Action.Source, Action.NeedsParams, Action.FormSource, Action.Args);
        }

        public bool ActionNameExists(string GroupName, string Name)
        {
            foreach (WIDA.Tasks.Actions.Action Action in this.Actions)
                if (Action.GroupName == GroupName)
                    if (Action.Name == Name)
                        return true;
            return false;
        }

        public string[] GetTriggersGroupNames()
        {
            List<string> GroupNames = new List<string>();
            foreach (Trigger Trigger in this.Triggers)
                if(!GroupNames.Contains(Trigger.GroupName))
                GroupNames.Add(Trigger.GroupName);

            return GroupNames.ToArray();
        }

        public string[] GetConditionsGroupNames()
        {
            List<string> GroupNames = new List<string>();
            foreach (Condition Condition in this.Conditions)
                if (!GroupNames.Contains(Condition.GroupName))
                    GroupNames.Add(Condition.GroupName);

            return GroupNames.ToArray();
        }

        public string[] GetActionsGroupNames()
        {
            List<string> GroupNames = new List<string>();
            foreach (WIDA.Tasks.Actions.Action Action in this.Actions)
                if (!GroupNames.Contains(Action.GroupName))
                    GroupNames.Add(Action.GroupName);

            return GroupNames.ToArray();
        }

        public Trigger[] GetTriggersFromGroupName(string GroupName)
        {
            List<Trigger> Triggers = new List<Trigger>();
            foreach (Trigger Trigger in this.Triggers)
                if (Trigger.GroupName == GroupName)
                    Triggers.Add(Trigger);

            return Triggers.ToArray();
        }

        public Condition[] GetConditionsFromGroupName(string GroupName)
        {
            List<Condition> Conditions = new List<Condition>();
            foreach (Condition Condition in this.Conditions)
                if (Condition.GroupName == GroupName)
                    Conditions.Add(Condition);

            return Conditions.ToArray();
        }

        public WIDA.Tasks.Actions.Action[] GetActionsFromGroupName(string GroupName)
        {
            List<WIDA.Tasks.Actions.Action> Actions = new List<WIDA.Tasks.Actions.Action>();
            foreach (WIDA.Tasks.Actions.Action Action in this.Actions)
                if (Action.GroupName == GroupName)
                    Actions.Add(Action);

            return Actions.ToArray();
        }
    }
}
