using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emotions;


namespace EmotionalBlackboard
{
    public enum Workers
    {
        All,
        MortgageEmployee,
        Student
    }

    class PersonsEnum
    {
        public static Person Reaction(Workers worker)
        {
            switch (worker)
            {
                case Workers.All:
                    return new Person
                    {
                        Id = null,
                        Name = "Все"
                    };
                case Workers.MortgageEmployee:
                    return new Person
                    {
                        Id = 1,
                        Employed = true,
                        Name = "Работник с ипотекой",
                        Behavior = (message, onSelf) =>
                        {
                            if (onSelf)
                            {
                                switch (message)
                                {
                                    case MessageType.Deadline:
                                        return new EmotionsDelta { SorrowDelta = 0.3, AnxietyDelta = 0.3, BlissDelta = -0.1, HappinessDelta = -0.2, FrustrationDelta = 0 };
                                    case MessageType.IncreaseSalary:
                                        return new EmotionsDelta { SorrowDelta = -0.8, AnxietyDelta = -0.5, BlissDelta = 0.5, HappinessDelta = 0.8, FrustrationDelta = -0.1 };
                                    case MessageType.NewMinorTasks:
                                        return new EmotionsDelta { SorrowDelta = -0.1, AnxietyDelta = -0.1, BlissDelta = 0.3, HappinessDelta = 0.1, FrustrationDelta = 0 };
                                    case MessageType.ChangeOffice:
                                        return new EmotionsDelta { SorrowDelta = 0.1, AnxietyDelta = 0.2, BlissDelta = -0.2, HappinessDelta = -0.1, FrustrationDelta = 0.3 };
                                    case MessageType.CorporativeParty:
                                        return new EmotionsDelta { SorrowDelta = -0.1, AnxietyDelta = -0.2, BlissDelta = 0, HappinessDelta = 0.1, FrustrationDelta = 0 };
                                    case MessageType.DecreaseSalary:
                                        return new EmotionsDelta { SorrowDelta = 0.1, AnxietyDelta = 0.1, BlissDelta = -0.1, HappinessDelta = -0.1, FrustrationDelta = 0.1 };
                                    case MessageType.Downgrade:
                                        return new EmotionsDelta { SorrowDelta = 0.2, AnxietyDelta = 0.3, BlissDelta = -0.2, HappinessDelta = -0.1, FrustrationDelta = 0.1 };
                                    case MessageType.IssueDayOff:
                                        return new EmotionsDelta { SorrowDelta = -0.2, AnxietyDelta = -0.2, BlissDelta = 0.2, HappinessDelta = 0.2, FrustrationDelta = -0.1 };
                                    case MessageType.IssueImportantAssignment:
                                        return new EmotionsDelta { SorrowDelta = 0.1, AnxietyDelta = 0, BlissDelta = -0.1, HappinessDelta = 0.2, FrustrationDelta = -0.1 };
                                    case MessageType.NewHardTasks:
                                        return new EmotionsDelta { SorrowDelta = 0.1, AnxietyDelta = 0.1, BlissDelta = -0.1, HappinessDelta = 0.1, FrustrationDelta = 0 };
                                    case MessageType.NewYearPresents:
                                        return new EmotionsDelta { SorrowDelta = -0.1, AnxietyDelta = -0.1, BlissDelta = 0.1, HappinessDelta = 0.2, FrustrationDelta = -0.1 };
                                    case MessageType.Prize:
                                        return new EmotionsDelta { SorrowDelta = -0.5, AnxietyDelta = -0.6, BlissDelta = 0.5, HappinessDelta = 0.4, FrustrationDelta = -0.3 };
                                    case MessageType.Praise:
                                        return new EmotionsDelta { SorrowDelta = -0.1, AnxietyDelta = 0, BlissDelta = 0, HappinessDelta = 0.1, FrustrationDelta = 0 };
                                    case MessageType.Raise:
                                        return new EmotionsDelta { SorrowDelta = -0.4, AnxietyDelta = -0.3, BlissDelta = 0.4, HappinessDelta = 0.4, FrustrationDelta = -0.3 };
                                    case MessageType.SendOnBusinessTrip:
                                        return new EmotionsDelta { SorrowDelta = 0.2, AnxietyDelta = 0.1, BlissDelta = 0.2, HappinessDelta = 0.1, FrustrationDelta = 0 };
                                    case MessageType.HelpFromEmployees:
                                        return new EmotionsDelta { SorrowDelta = -0.1, AnxietyDelta = -0.1, BlissDelta = 0, HappinessDelta = 0.2, FrustrationDelta = 0 };
                                    case MessageType.Inaction:
                                        return new EmotionsDelta { SorrowDelta = 0.1, AnxietyDelta = 0, BlissDelta = -0.1, HappinessDelta = -0.1, FrustrationDelta = 0 };
                                    default:
                                        return new EmotionsDelta { };
                                }
                            }
                            else
                            {
                                switch (message)
                                {
                                    case MessageType.IncreaseSalary:
                                        return new EmotionsDelta { SorrowDelta = 0.3, AnxietyDelta = 0.1, HappinessDelta = -0.2, FrustrationDelta = 0.3 };
                                    case MessageType.Prize:
                                    case MessageType.Raise:
                                    case MessageType.Inaction:
                                        return new EmotionsDelta { SorrowDelta = 0.2, AnxietyDelta = 0, BlissDelta = 0, HappinessDelta = -0.2, FrustrationDelta = 0.1 };
                                    case MessageType.IssueDayOff:
                                        return new EmotionsDelta { SorrowDelta = 0.1, AnxietyDelta = 0, BlissDelta = 0, HappinessDelta = 0, FrustrationDelta = 0 };

                                    default:
                                        return new EmotionsDelta { };
                                }
                            }
                        },
                        CustomReactions = (mood) =>
                        {
                            if (mood.Bliss > 0.8 && mood.Happiness>0.8)
                                return new ActionResult
                                {
                                    Message = "Ура, я не проживаю жизнь в пустую!",
                                    ForBoard = new BBMessage
                                    {
                                        TTL = 5,
                                        Type = MessageType.HelpFromEmployees,
                                        TargetId = null,
                                        Name = "Помочь от счастья"
                                    }
                                };
                            if (mood.Anxiety > 0.9)
                                return new ActionResult
                                {
                                    Message = "Обидно, досадно, но мне ипотеку платить",
                                    ForBoard = new BBMessage
                                    {
                                        TTL = 2,
                                        Type = MessageType.NewMinorTasks,
                                        TargetId = null,
                                        Name = "Сделать баги, которые надо чинить"
                                    }
                                };
                            if (mood.Bliss > 0.8)
                                return new ActionResult
                                {
                                    Message = "Наконец и на моей улице праздник!",
                                    ForBoard = new BBMessage
                                    {
                                        TTL = 3,
                                        Type = MessageType.HelpFromEmployees,
                                        TargetId = null,
                                        Name = "Помочь от счастья"
                                    }
                                };
                            if (mood.Frustration > 0.9)
                                return new ActionResult
                                {
                                    Message = "Всё валиться из рук, и что мне со всем этим делать?!",
                                    ForBoard = new BBMessage
                                    {
                                        TTL = 2,
                                        Type = MessageType.NewHardTasks,
                                        TargetId = null,
                                        Name = "Сломать часть рабочего кода, в котором мало кто разбирается"
                                    }
                                };
                            if (mood.Happiness > 0.7)
                                return new ActionResult
                                {
                                    Message = "Жизнь не так уж и плоха.",
                                    ForBoard = new BBMessage
                                    {
                                        TTL = 1,
                                        Type = MessageType.HelpFromEmployees,
                                        TargetId = null,
                                        Name = "Помощь от счастья"
                                    }
                                };
                            if (mood.Sorrow > 0.8)
                                return new ActionResult
                                {
                                    Message = "Жизнь плоха.",
                                    ForBoard = new BBMessage
                                    {
                                        TTL = 1,
                                        Type = MessageType.NewMinorTasks,
                                        TargetId = null,
                                        Name = "Сделать баги, которые надо чинить"
                                    }
                                };

                            return new ActionResult
                            {
                                Message = "Мне норм"
                            };
                        }
                    };
                case Workers.Student:
                    return new Person
                    {
                        Id = 2,
                        Employed = true,
                        Name = "Студент на подработке",
                        Behavior = (message, onSelf) =>
                        {
                            if (onSelf)
                            {
                                switch (message)
                                {
                                    case MessageType.Deadline:
                                        return new EmotionsDelta { SorrowDelta = 0.1, AnxietyDelta = 0.3, BlissDelta = -0.2, HappinessDelta = 0.1, FrustrationDelta = 0.1 };
                                    case MessageType.IncreaseSalary:
                                        return new EmotionsDelta { SorrowDelta = -0.8, AnxietyDelta = 0, BlissDelta = 0.7, HappinessDelta = 0.8, FrustrationDelta = 0 };
                                    case MessageType.NewMinorTasks:
                                        return new EmotionsDelta { SorrowDelta = 0, AnxietyDelta = 0, BlissDelta = 0, HappinessDelta = 0.1, FrustrationDelta = 0 };
                                    case MessageType.ChangeOffice:
                                        return new EmotionsDelta { SorrowDelta = 0.1, AnxietyDelta = 0, BlissDelta = 0, HappinessDelta = 0, FrustrationDelta = 0 };
                                    case MessageType.CorporativeParty:
                                        return new EmotionsDelta { SorrowDelta = -0.1, AnxietyDelta = -0.2, BlissDelta = 0.1, HappinessDelta = 0.2, FrustrationDelta = -0.1 };
                                    case MessageType.DecreaseSalary:
                                        return new EmotionsDelta { SorrowDelta = 0.2, AnxietyDelta = 0.1, BlissDelta = -0.1, HappinessDelta = -0.1, FrustrationDelta = 0.1 };
                                    case MessageType.Downgrade:
                                        return new EmotionsDelta { SorrowDelta = 0.3, AnxietyDelta = 0.5, BlissDelta = -0.3, HappinessDelta = -0.3, FrustrationDelta = 0.3 };
                                    case MessageType.IssueDayOff:
                                        return new EmotionsDelta { SorrowDelta = -0.3, AnxietyDelta = -0.2, BlissDelta = 0.4, HappinessDelta = 0.3, FrustrationDelta = -0.2 };
                                    case MessageType.IssueImportantAssignment:
                                        return new EmotionsDelta { SorrowDelta = -0.2, AnxietyDelta = -0.1, BlissDelta = -0.1, HappinessDelta = 0.2, FrustrationDelta = -0.1 };
                                    case MessageType.NewHardTasks:
                                        return new EmotionsDelta { SorrowDelta = 0.3, AnxietyDelta = 0, BlissDelta = -0.2, HappinessDelta = 0, FrustrationDelta = 0 };
                                    case MessageType.NewYearPresents:
                                        return new EmotionsDelta { SorrowDelta = -0.1, AnxietyDelta = -0.1, BlissDelta = 0.2, HappinessDelta = 0.1, FrustrationDelta = -0.1 };
                                    case MessageType.Prize:
                                        return new EmotionsDelta { SorrowDelta = -0.3, AnxietyDelta = -0.1, BlissDelta = 0.4, HappinessDelta = 0.6, FrustrationDelta = -0.1 };
                                    case MessageType.Praise:
                                        return new EmotionsDelta { SorrowDelta = -0.3, AnxietyDelta = 0, BlissDelta = 0.3, HappinessDelta = 0.4, FrustrationDelta = -0.2 };
                                    case MessageType.Raise:
                                        return new EmotionsDelta { SorrowDelta = -0.4, AnxietyDelta = -0.3, BlissDelta = 0.5, HappinessDelta = 0.6, FrustrationDelta = -0.3 };
                                    case MessageType.SendOnBusinessTrip:
                                        return new EmotionsDelta { SorrowDelta = -0.2, AnxietyDelta = 0, BlissDelta = 0.3, HappinessDelta = 0.4, FrustrationDelta = 0 };
                                    case MessageType.HelpFromEmployees:
                                        return new EmotionsDelta { SorrowDelta = -0.3, AnxietyDelta = 0, BlissDelta = 0.2, HappinessDelta = 0.2, FrustrationDelta = 0 };

                                    default:
                                        return new EmotionsDelta { };
                                }
                            }
                            else
                            {
                                switch (message)
                                {
                                    case MessageType.IncreaseSalary:
                                        return new EmotionsDelta { SorrowDelta = 0.1, AnxietyDelta = 0, HappinessDelta = -0.1, FrustrationDelta = 0 };
                                    case MessageType.Prize:
                                    case MessageType.Raise:
                                        return new EmotionsDelta { SorrowDelta = 0.1, AnxietyDelta = 0.1, BlissDelta = 0, HappinessDelta = -0.1, FrustrationDelta = 0.1 };
                                    case MessageType.IssueDayOff:
                                        return new EmotionsDelta { SorrowDelta = 0.2, AnxietyDelta = 0.1, BlissDelta = 0, HappinessDelta = 0, FrustrationDelta = 0 };

                                    default:
                                        return new EmotionsDelta { };
                                }
                            }
                        },
                        CustomReactions = (mood) =>
                        {
                            if (mood.Bliss > 0.9 && mood.Happiness > 0.9)
                                return new ActionResult
                                {
                                    Message = "Да я идеальный студент!",
                                    ForBoard = new BBMessage
                                    {
                                        TTL = 3,
                                        Type = MessageType.HelpFromEmployees,
                                        TargetId = null,
                                        Name = "Помочь от счастья"
                                    }
                                };
                            if (mood.Anxiety > 0.9)
                                return new ActionResult
                                {
                                    Message = "Ушёл в депрессию...",
                                    ForBoard = new BBMessage
                                    {
                                        TTL = 2,
                                        Type = MessageType.Inaction,
                                        TargetId = null,
                                        Name = "Убивать время на поднятие настроения"
                                    }
                                };
                            if (mood.Bliss > 0.8 || mood.Happiness > 0.7)
                                return new ActionResult
                                {
                                    Message = "Хорошо, держимся",
                                    ForBoard = new BBMessage
                                    {
                                        TTL = 1,
                                        Type = MessageType.HelpFromEmployees,
                                        TargetId = null,
                                        Name = "Помочь от счастья"
                                    }
                                };
                            if (mood.Frustration > 0.9)
                                return new ActionResult
                                {
                                    Message = "Я ужасный студент.",
                                    ForBoard = new BBMessage
                                    {
                                        TTL = 3,
                                        Type = MessageType.NewMinorTasks,
                                        TargetId = null,
                                        Name = "Мешать остальным в попытках поднять своё эго"
                                    }
                                };
                            if (mood.Sorrow > 0.8)
                                return new ActionResult
                                {
                                    Message = "Эх, всё не заладилось.",
                                    ForBoard = new BBMessage
                                    {
                                        TTL = 1,
                                        Type = MessageType.NewMinorTasks,
                                        TargetId = null,
                                        Name = "Сделать баги, которые надо чинить"
                                    }
                                };

                            return new ActionResult
                            {
                                Message = "Мне норм"
                            };
                        }
                    };

            }
            return new Person();
        }
    }
}
