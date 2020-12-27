using Emotions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EmotionalBlackboard
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<string> BBResults = new ObservableCollection<string>();

        public Blackboard Blackboard;

        public MainWindow()
        {
            InitializeComponent();

            var possibleMessages = new List<BBMessage>
            {
                new BBMessage
                {
                    TTL = -1,
                    Name = "Нет сообщения"
                },
                new BBMessage
                {
                    TTL = 2,
                    Type = MessageType.Deadline,
                    Name = "Сказать, что скоро дедлайн"
                },
                new BBMessage
                {
                    TTL = 5,
                    Type = MessageType.NewHardTasks,
                    Name = "Добавить сложных заданий"
                },
                new BBMessage
                {
                    TTL = 4,
                    Type = MessageType.IncreaseSalary,
                    Name = "Повышение зарплаты"
                },
                new BBMessage
                {
                    TTL = 4,
                    Type = MessageType.DecreaseSalary,
                    Name = "Понижение зарплаты"
                },
                new BBMessage
                {
                    TTL = 6,
                    Type = MessageType.ChangeOffice,
                    Name = "Переезд в новый офис"
                },
                new BBMessage
                {
                    TTL = 2,
                    Type = MessageType.CorporativeParty,
                    Name = "Корпоратив"
                },
                new BBMessage
                {
                    TTL = 3,
                    Type = MessageType.NewYearPresents,
                    Name = "Новогодние подарки"
                },
                new BBMessage
                {
                    TTL = 3,
                    Type = MessageType.NewMinorTasks,
                    Name = "Добавить лёгких задач"
                },
                new BBMessage
                {
                    TTL = 3,
                    Type = MessageType.Prize,
                    Name = "Выдать премиальные"
                },
                new BBMessage
                {
                    TTL = 3,
                    Type = MessageType.Downgrade,
                    Name = "Понизить в должности"
                },
                new BBMessage
                {
                    TTL = 1,
                    Type = MessageType.Dismiss,
                    Name = "Уволить"
                },
                new BBMessage
                {
                    TTL = 3,
                    Type = MessageType.Raise,
                    Name = "Повысить в должности"
                },
                new BBMessage
                {
                    TTL = 1,
                    Type = MessageType.IssueImportantAssignment,
                    Name = "Выдать важное задание"
                },
                new BBMessage
                {
                    TTL = 2,
                    Type = MessageType.Praise,
                    Name = "Похвалить"
                },
                new BBMessage
                {
                    TTL = 4,
                    Type = MessageType.IssueDayOff,
                    Name = "Выдать выходной"
                },
                new BBMessage
                {
                    TTL = 3,
                    Type = MessageType.SendOnBusinessTrip,
                    Name = "Отправить в командировку"
                }
            };

            var employee = new List<Person>
            {
                new Person
                {
                    Id = null,
                    Name = "Все"
                },
                new Person
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
                                    return new EmotionsDelta {SorrowDelta = 0.3, AnxietyDelta = 0.3, BlissDelta = -0.1, HappinessDelta = -0.2};
                                case MessageType.IncreaseSalary:
                                    return new EmotionsDelta {SorrowDelta = -0.8, AnxietyDelta = -0.5, BlissDelta = 0.5, HappinessDelta = 0.8};
                                case MessageType.NewMinorTasks:
                                    return new EmotionsDelta {SorrowDelta = -0.1, AnxietyDelta = -0.1, BlissDelta = 0.3, HappinessDelta = 0.1};
                                default:
                                    return new EmotionsDelta { };
                            }
                        }
                        else
                        {
                            switch (message)
                            {
                                case MessageType.IncreaseSalary:
                                    return new EmotionsDelta {SorrowDelta = 0.3, AnxietyDelta = 0.1, HappinessDelta = -0.2, FrustrationDelta = 0.3};
                                default:
                                    return new EmotionsDelta { };
                            }
                        }
                    },
                    CustomReactions = (mood) =>
                    {
                        if (mood.Anxiety > 0.9)
                            return new ActionResult {
                                Message = "Обидно, досадно, но мне ипотеку платить",
                                ForBoard = new BBMessage
                                {
                                    TTL = 2, 
                                    Type = MessageType.NewMinorTasks,
                                    TargetId = null,
                                    Name = "Сделать баги, которые надо чинить"
                                }
                            };
                        return new ActionResult {
                                Message = "Мне норм"
                            };
                    }
                }
            };

            Blackboard = new Blackboard
            {
                messages = new List<BBMessage>(),
                staff = employee.Skip(1).ToList()
            };

            messagesList.ItemsSource = possibleMessages;
            employeeList.ItemsSource = employee;
            iterationResult.ItemsSource = BBResults;
        }

        private void act_Click(object sender, RoutedEventArgs e)
        {
            if (messagesList.SelectedItem != null)
            {
                BBMessage selected = (BBMessage)messagesList.SelectedItem;

                if (selected.TTL >= 0)
                {
                    Blackboard.messages.Add(new BBMessage
                    {
                        TTL = selected.TTL,
                        Name = selected.Name,
                        Type = selected.Type,
                        TargetId = employeeList.SelectedItem != null ? ((Person)employeeList.SelectedItem).Id : null
                    });
                }
            }

            var results = Blackboard.Act();
            foreach (var res in results)
            {
                BBResults.Add(res);
            }
        }
    }
}
