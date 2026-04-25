using GerenciadorTarefas.Domain.Enums;

namespace GerenciadorTarefas.Communication.Requests
{
    public class CreateTaskRequestJson
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public EnumTaskPriority Priority { get; set; }
        public DateTime DueDate { get; set; }
        public EnumTaskStatus Status { get; set; }
    }
}
