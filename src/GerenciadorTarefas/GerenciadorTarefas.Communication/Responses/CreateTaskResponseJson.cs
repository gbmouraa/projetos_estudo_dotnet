namespace GerenciadorTarefas.Communication.Responses
{
    public class CreateTaskResponseJson
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
