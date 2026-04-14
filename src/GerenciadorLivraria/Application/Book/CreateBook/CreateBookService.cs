namespace GerenciadorLivraria.Application.Book.CreateBook
{
    public class CreateBookService
    {
        public CreateBookResult Execute(CreateBookModel model)
        {
            Validate(model);

            // dbContext
            // var bookEntity

            return new CreateBookResult{ Id = "Um ID", Title = model.Title };
        }

        public void Validate(CreateBookModel model)
        {
            var validator = new CreateBookValidator();
            var result = validator.Validate(model);

            if (!result.IsValid)
            {
                // criar exception persolisada que retorne uma lista de erros
                throw new Exception("Dados invalidos");
            }
        }
    }
}
