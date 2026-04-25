namespace GerenciadorTarefas.Communication.Requests
{
    public class CreateTaskRequestJson
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Priority { get; set; }
        public DateTime DueDate { get; set; }
        public int Status { get; set; }
    }
}
