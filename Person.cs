using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emotions
{
    /// <summary>
    /// Карта эмоций – настроение
    /// </summary>
    public class Mood
    {
        public double Sorrow = 0;
        public double Happiness = 0;
        public double Frustration = 0;
        public double Anxiety = 0;
        public double Bliss = 0;

        public void Apply(EmotionsDelta deltaChanges)
        {
            Sorrow = Clamp(Sorrow + deltaChanges.SorrowDelta, 0, 1);
            Happiness = Clamp(Happiness + deltaChanges.HappinessDelta, 0, 1);
            Frustration = Clamp(Frustration + deltaChanges.FrustrationDelta, 0, 1);
            Anxiety = Clamp(Anxiety + deltaChanges.AnxietyDelta, 0, 1);
            Bliss = Clamp(Bliss + deltaChanges.BlissDelta, 0, 1);
        }

        public static double Clamp(double val, double min, double max)
        {
            if (val < min) return min;
            if (val > max) return max;
            return val;
        }
    }

    public class EmotionsDelta
    {
        public double SorrowDelta { get; set; } = 0;
        public double HappinessDelta { get; set; } = 0;
        public double FrustrationDelta { get; set; } = 0;
        public double AnxietyDelta { get; set; } = 0;
        public double BlissDelta { get; set; } = 0;
    }

    /// <summary>
    /// Сотрудник фирмы
    /// </summary>
    public class Person
    {
        public long? Id { get; set; }
        public bool Employed { get; set; } = true;
        public string Name { get; set; }
        public Mood Condition { get; set; } = new Mood();

        public Func<MessageType, bool, EmotionsDelta> Behavior { get; set; }
        public Func<Mood, ActionResult> CustomReactions { get; set; }

        /// <summary>
        /// Работа с классной доской (списком сообщений)
        /// </summary>
        /// <param name="messages"></param>
        public ActionResult Act(ref List<BBMessage> messages)
        {
            string reactions = $"{Name} говорит: ";
            foreach (var msg in messages)
            {
                if (msg.TargetId == Id || msg.TargetId == null)
                {
                    //  Обрабатываем сообщение, которое адресовано нам
                    Condition.Apply(Behavior(msg.Type, true));
                }
                else if (msg.TargetId != Id && msg.TargetId != null)
                {
                    //  Обрабатываем сообщение, которое адресовано не нам
                    Condition.Apply(Behavior(msg.Type, false));
                }
            }

            //  Генерация сообщений - действия в зависимости от текущего настроения
            ActionResult result = CustomReactions(Condition);
            result.Message = reactions + result.Message;
            return result;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class ActionResult
    {
        public string Message { get; set; }
        public BBMessage ForBoard { get; set; }
    }
}
