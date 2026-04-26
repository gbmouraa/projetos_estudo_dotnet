using GerenciadorTarefas.Application.Services.Validators;
using GerenciadorTarefas.Communication.Requests;
using GerenciadorTarefas.Communication.Responses;
using GerenciadorTarefas.Domain.Entities;
using GerenciadorTarefas.Domain.Enums;
using GerenciadorTarefas.Exceptions;
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

        #region Metodos

        public CreateTaskResponseJson Create(TaskRequestJson request)
        {
            TaskValidation(request);

            TaskEntity task = new()
            {
                Id = new Guid(),
                Name = request.Name,
                Description = request.Description,
                Priority = request.Priority,
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

        public List<TaskResponseJson> GetAll()
        {
            var tasks = _dbContext.Tasks
                                    .Select(t => new TaskResponseJson
                                    {
                                        Id = t.Id,
                                        Name = t.Name,
                                        Description = t.Description,
                                        Priority = t.Priority.ToString(),
                                        Status = t.Status.ToString(),
                                        DueDate = t.DueDate,
                                    }).ToList();

            return tasks;
        }

        public TaskResponseJson GetById(Guid taskId)
        {
            var task = _dbContext.Tasks.FirstOrDefault(t => t.Id == taskId);

            if (task == null)
                throw new NotFoundException("Tarefa nao encontrada");

            return new TaskResponseJson
            {
                Id = taskId,
                Name = task.Name,
                Description = task.Description,
                Priority = task.Priority.ToString(),
                Status = task.Status.ToString(),
                DueDate = task.DueDate,
            };
        }

        public UpdateTaskResponseJson Update(Guid taskId, TaskRequestJson request)
        {
            var task = _dbContext.Tasks.FirstOrDefault(t => t.Id == taskId);

            if (task == null)
                throw new NotFoundException("Tarefa nao encontrada");

            TaskValidation(request);

            task.Name = request.Name;
            task.Description = request.Description;
            task.Priority = request.Priority;
            task.Status = request.Status;
            task.DueDate = request.DueDate;
            task.UpdatedAt = DateTime.Now;

            _dbContext.Tasks.Update(task);
            _dbContext.SaveChanges();

            return new UpdateTaskResponseJson { Id = taskId, Name = task.Name };
        }

        public void Delete(Guid taskId)
        {
            var task = _dbContext.Tasks.FirstOrDefault(t => t.Id == taskId);

            if (task == null)
                throw new NotFoundException("Tarefa nao encontrada");

            _dbContext.Remove(task);
            _dbContext.SaveChanges();
        }

        #endregion


        #region Validators

        private void TaskValidation(TaskRequestJson task)
        {
            TaskValidator validator = new();

            var result = validator.Validate(task);

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errors);
            }
        }

        #endregion

    }
}
