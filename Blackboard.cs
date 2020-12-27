using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emotions
{
    public enum MessageType
    {
        /// <summary>
        /// Повышение зп
        /// </summary>
        IncreaseSalary,
        /// <summary>
        /// Понижение зп
        /// </summary>
        DecreaseSalary,
        /// <summary>
        /// Смена офиса
        /// </summary>
        ChangeOffice,
        /// <summary>
        /// Корпоратив
        /// </summary>
        CorporativeParty,
        /// <summary>
        /// Новогодний подарок
        /// </summary>
        NewYearPresents,
        /// <summary>
        /// Новая тяжёлая задача
        /// </summary>
        NewHardTasks,
        /// <summary>
        /// Новая лёгкая задача
        /// </summary>
        NewMinorTasks,
        /// <summary>
        /// Дедлайн
        /// </summary>
        Deadline,
        /// <summary>
        /// Премия
        /// </summary>
        Prize,
        /// <summary>
        /// Понижение
        /// </summary>
        Downgrade,
        /// <summary>
        /// Повышение
        /// </summary>
        Raise,
        /// <summary>
        /// Выдача важного задания
        /// </summary>
        IssueImportantAssignment,
        /// <summary>
        /// Похвала
        /// </summary>
        Praise,
        /// <summary>
        /// Выдача выходного
        /// </summary>
        IssueDayOff,
        /// <summary>
        /// Отправление в командировку
        /// </summary>
        SendOnBusinessTrip,
        /// <summary>
        /// Помощь сотрудников
        /// </summary>
        HelpFromEmployees,
        /// <summary>
        /// Бездействие
        /// </summary>
        Inaction
    }

    /// <summary>
    /// Сообщение на классной доске
    /// </summary>
    public class BBMessage
    {
        /// <summary>
        /// Кол-во повторений
        /// </summary>
        public int TTL { get; set; }

        /// <summary>
        /// Тип события
        /// </summary>
        public MessageType Type { get; set; }

        /// <summary>
        /// Кто будет получать "радость"
        /// </summary>
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
