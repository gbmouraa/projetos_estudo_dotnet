using AutoMapper;
using GerenciadorLivraria.Communication.Requests;
using GerenciadorLivraria.Communication.Responses;
using GerenciadorLivraria.Domain.Entities;

namespace GerenciadorLivraria.Application.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            RequestToEntity();
            EntityToResponse();
        }

        private void RequestToEntity()
        {
            CreateMap<RegisterBookRequest, BookEntity>();
            CreateMap<UpdateBookRequest, BookEntity>();
        }

        private void EntityToResponse()
        {
            CreateMap<BookEntity, BookResponse>();
            CreateMap<BookEntity, RegisterBookResponse>();
        }
    }
}
