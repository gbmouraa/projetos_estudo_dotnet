using GerenciadorTarefas.Application.Services.Validators;
using GerenciadorTarefas.Communication.Requests;
using GerenciadorTarefas.Communication.Responses;
using GerenciadorTarefas.Domain.Entities;
using GerenciadorTarefas.Domain.Enums;
using GerenciadorTarefas.Infrastructure.Database;

namespace GerenciadorTarefas.Application.Services
{
    public class TaskService
    {
        private readonly GerenciadorTarefasDbContext _dbContext;

        public TaskService(GerenciadorTarefasDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public CreateTaskResponseJson Create(CreateTaskRequestJson request)
        {
            CreateTaskValidation(request);

            TaskEntity task = new()
            {
                Id = new Guid(),
                Name = request.Name,
                Description = request.Description,
                Priority = (EnumTaskPriority)request.Priority,
                Status = EnumTaskStatus.pending,
                DueDate = request.DueDate,
                CreatedAt = DateTime.Now,
            };

            _dbContext.Tasks.Add(task);
            _dbContext.SaveChanges();

            return new CreateTaskResponseJson
            {
                Id = task.Id,
                Name = request.Name,
            };
        }

        #region Validators

        private void CreateTaskValidation(CreateTaskRequestJson task)
        {
            TaskValidator validator = new();

            var result = validator.Validate(task);

            if (!result.IsValid)
            {
                throw new Exception("Dados invalidos");
            }
        }

        #endregion

    }
}
