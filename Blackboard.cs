using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emotions
{
    public enum MessageType { 
        IncreaseSalary, //3
        DecreaseSalary, //4 
        ChangeOffice,//5
        CorporativeParty,//6
        NewYearPresents,//7
        NewHardTasks,//2
        NewMinorTasks,//8
        Deadline, //1
        Prize,//9
        Downgrade,//10
        Dismiss,//11
        Raise,//12
        IssueImportantAssignment,//13
        Praise,//14
        IssueDayOff,//15
        SendOnBusinessTrip//16
    }

    /// <summary>
    /// Сообщение на классной доске
    /// </summary>
    public class BBMessage
    {
        public int TTL { get; set; }

        public MessageType Type { get; set; }

        public long? TargetId { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    public class Blackboard
    {
        public List<BBMessage> messages = new List<BBMessage>();

        public List<Person> staff = new List<Person>();

        public List<string> Act()
        {
            var result = new List<string>();
            for (int i = 0; i < staff.Count; ++i)
            {
                if (staff[i].Employed)
                {
                    ActionResult actionResult = staff[i].Act(ref messages);
                    result.Add(actionResult.Message);
                    if (actionResult.ForBoard != null)
                    {
                        messages.Add(actionResult.ForBoard);
                    }
                }
            }

            messages.ForEach(message => message.TTL -= 1);
            messages = messages.Where(message => message.TTL > 0).ToList();

            return result;
        }
    }
}
