using System;
using EventStore.Infrastructure;

namespace EventStore
{
    class Program
    {
        static void Main(string[] args)
        {
            EventBus.Current.Subscribe(Console.WriteLine);

            var store = new Store("store.json");
            
            var tasks = new Repository<Task>(store);
            var lists = new Repository<TaskList>(store);

            CreateInbox(lists);

            var task1 = CreateTask("inbox");
            tasks.Save(task1);

            var task2 = tasks.Get(task1.Id);
            Console.WriteLine(task2.Title);
            task2.ChangeTitle("foo");
            tasks.Save(task2);

            Console.ReadLine();
        }

        private static void CreateInbox(Repository<TaskList> lists)
        {
            if (lists.Find("inbox") == null)
            {
                var inbox = new TaskList("inbox", "Inbox");
                lists.Save(inbox);
            }
        }

        private static Task CreateTask(string listId)
        {
            var id = Guid.NewGuid().ToString();

            return new Task(id, listId, "Hello World")
                .ChangeDueDate(DateTime.Today.AddDays(1))
                .Complete()
                .Incomplete()
                .Star()
                .ChangeTitle("Hello again!");
        }
    }
}
