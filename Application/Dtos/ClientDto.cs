using Domain.Enums;
using Domain.Repositories;
using System.ComponentModel.DataAnnotations;

namespace Application.Dtos
{
    public class ClientDto : IBase
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string? Address { get; set; }
        public Country Country { get; set; }
        public string? PostalCode { get; set; }
        public string Email { get; set; }
    }
}
