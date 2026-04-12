using System.Runtime.Serialization;

namespace GerenciadorLivraria.Domain.Enums
{
    public enum EnumGenre
    {
        [EnumMember(Value = "Ficção")]
        Ficcao,

        [EnumMember(Value = "Romance")]
        Romance,

        [EnumMember(Value = "Mistério")]
        MIsterio,

        [EnumMember(Value = "Filosofia")]
        Filosofia,
    }
}
