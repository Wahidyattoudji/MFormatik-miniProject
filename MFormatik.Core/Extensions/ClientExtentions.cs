using MFormatik.Core.DTOs;
using MFormatik.Core.Models;

namespace MFormatik.Core.Extensions
{
    public static class ClientExtentions
    {
        public static ClientDTO ToDto(this Client client)
        {
            if (client == null) return null;
            return new ClientDTO
            {
                Id = client.Id,
                FullName = $"{client.FirstName} {client.LastName}";
            };
        }

        public static Client ToEntity(this ClientDTO dto)
        {
            if (dto == null) return null;
            var names = dto.FullName?.Split(' ');
            return new Client
            {
                Id = dto.Id,
                FirstName = names?.FirstOrDefault() ?? string.Empty,
                LastName = names?.Skip(1).FirstOrDefault() ?? string.Empty
            };
        }
    }
}
