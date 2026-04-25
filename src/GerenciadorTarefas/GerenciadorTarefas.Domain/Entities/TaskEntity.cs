using GerenciadorTarefas.Domain.Enums;

namespace GerenciadorTarefas.Domain.Entities
{
    public class TaskEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public EnumTaskPriority Priority { get; set; }
        public EnumTaskStatus Status { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
