using Domain.Enums;
using Domain.Repositories;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Client : IBase
    {
        public int Id => ClientId;

        [Key]
        public int ClientId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        [Required]
        public Gender Gender { get; set; }

        public DateTime BirthDate { get; set; }

        public string? Address { get; set; }

        [Required]
        public Country Country { get; set; }

        public string? PostalCode { get; set; }

        public string Email { get; set; }
    }
}
