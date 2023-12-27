namespace ToDo.Application.Models
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot("project")]
    public class XmlProjectDataModel
    {
        [XmlArray("developers")]
        [XmlArrayItem("developer")]
        public List<XmlDeveloper> Developers { get; set; }

        [XmlElement("tasks")]
        public XmlTasks Tasks { get; set; }
    }

    public class XmlDeveloper
    {
        [XmlElement("dev_name")]
        public string Name { get; set; }

        [XmlElement("capacity_per_hour")]
        public int HourlyCapacity { get; set; }
    }

    public class XmlTasks
    {
        [XmlElement("task")]
        public List<XmlTask> TaskList { get; set; }
    }

    public class XmlTask
    {
        [XmlElement("task_id")]
        public int TaskId { get; set; }

        [XmlElement("task_name")]
        public string TaskName { get; set; }

        [XmlElement("task_duration")]
        public int TaskDuration { get; set; }

        [XmlElement("task_difficulty")]
        public int TaskDifficulty { get; set; }

        [XmlElement("task_assigned_to")]
        public string TaskAssignedTo { get; set; }
    }

}
