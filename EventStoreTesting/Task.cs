using System;
using System.Collections.Generic;
using EventStore.Events;
using EventStore.Infrastructure;

namespace EventStore
{
    public class Task : EventSourced
    {
        protected Task(string id)
            : base(id)
        {
            Handles<TaskCreated>(e => ListId = e.ListId);
            Handles<TaskMoved>(e => ListId = e.ToListId);
            Handles<TaskCompleted>(e => CompletedAt = e.CompletedAt);
            Handles<TaskIncompleted>(e => CompletedAt = null);
            Handles<TaskDeleted>(e => DeletedAt = e.DeletedAt);
            Handles<TaskStarred>(e => IsStarred = true);
            Handles<TaskUnstarred>(e => IsStarred = false);
            Handles<TaskTitleChanged>(e => Title = e.Title);
            Handles<TaskNoteChanged>(e => Note = e.Note);
            Handles<TaskDueDateChanged>(e => DueDate = e.DueDate);
        }

        /// <summary>
        /// Loads an existing task, specifying the past events.
        /// </summary>
        /// <param name="id">The local ID.</param>
        /// <param name="history">The event stream of this event sourced object.</param>
        public Task(string id, IEnumerable<IVersionedEvent> history)
            : this(id)
        {
            LoadFrom(history);
        }

        /// <summary>
        /// Creates a new task with the given title.
        /// </summary>
        /// <param name="id">The local ID.</param>
        /// <param name="listId">The list in which the task is created.</param>
        /// <param name="title">The task title.</param>
        public Task(string id, string listId, string title)
            : this(id)
        {
            Update(new TaskCreated(listId));
            Update(new TaskTitleChanged(title));
        }

        public DateTime? CompletedAt { get; private set; }

        public bool IsCompleted
        {
            get { return CompletedAt.HasValue; }
        }

        public DateTime? DeletedAt { get; private set; }

        public bool IsDeleted
        {
            get { return DeletedAt.HasValue; }
        }

        public bool IsStarred { get; private set; }

        public string Title { get; private set; }

        public string Note { get; private set; }

        public DateTime? DueDate { get; private set; }

        public string ListId { get; private set; }

        public Task Complete()
        {
            return Complete(DateTime.Now);
        }

        public Task Complete(DateTime completedAt)
        {
            Update(new TaskCompleted(completedAt));
            return this;
        }

        public Task Incomplete()
        {
            Update(new TaskIncompleted());
            return this;
        }

        public Task Star()
        {
            Update(new TaskStarred());
            return this;
        }

        public Task Unstar()
        {
            Update(new TaskUnstarred());
            return this;
        }

        public Task ChangeTitle(string value)
        {
            Update(new TaskTitleChanged(value));
            return this;
        }

        public Task ChangeNote(string value)
        {
            Update(new TaskNoteChanged(value));
            return this;
        }

        public Task ChangeDueDate(DateTime? value)
        {
            Update(new TaskDueDateChanged(value));
            return this;
        }

        public Task Move(string listId)
        {
            Update(new TaskMoved(ListId, listId));
            return this;
        }
    }
}
