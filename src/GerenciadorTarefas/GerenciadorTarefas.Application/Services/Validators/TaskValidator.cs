using FluentValidation;
using GerenciadorTarefas.Communication.Requests;

namespace GerenciadorTarefas.Application.Services.Validators
{
    public class TaskValidator : AbstractValidator<CreateTaskRequestJson>
    {
        public TaskValidator()
        {
            RuleFor(t => t.Name)
                .NotEmpty().WithMessage("Insira um nome para a tarefa")
                .MaximumLength(100).WithMessage("O nome da tarefa e muito grande");

            RuleFor(t => t.Description)
                .MaximumLength(500).WithMessage("O nome da tarefa e muito grande");

            RuleFor(t => t.Priority)
                .IsInEnum().WithMessage("Prioridade nao e valida");

            RuleFor(t => t.DueDate)
                .NotEmpty().WithMessage("Defina uma data para a conclusao da tarefa")
                .GreaterThanOrEqualTo(DateTime.Now.Date).WithMessage("Data para conclusao de tarefa nao pode ser anterior a data atual");

            RuleFor(t => t.Status)
                .IsInEnum().WithMessage("Prioridade nao e valida");
        }
    }
}
