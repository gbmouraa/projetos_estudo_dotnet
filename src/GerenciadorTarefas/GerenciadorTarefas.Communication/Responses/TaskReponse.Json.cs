using GerenciadorTarefas.Domain.Enums;

namespace GerenciadorTarefas.Communication.Responses
{
    public class TaskResponseJson
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public DateTime DueDate { get; set; }
    }

    public class CreateTaskResponseJson
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class UpdateTaskResponseJson : CreateTaskResponseJson { }
}
