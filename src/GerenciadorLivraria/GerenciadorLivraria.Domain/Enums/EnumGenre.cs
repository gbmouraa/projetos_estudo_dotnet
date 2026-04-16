using System.Runtime.Serialization;

// usar para validacao de generos
namespace GerenciadorLivraria.Domain.Enums
{
    public enum EnumGenre
    {
        [EnumMember(Value = "Ficção")]
        Ficcao,

        [EnumMember(Value = "Romance")]
        Romance,

        [EnumMember(Value = "Mistério")]
        Misterio,

        [EnumMember(Value = "Filosofia")]
        Filosofia,
    }
}
