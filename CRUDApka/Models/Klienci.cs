namespace CRUDApka.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Klienci")]
    public partial class Klienci
    {
		[Key]
        public int Id { get; set; }

		[Required(ErrorMessage = "Pole imiê jest wymagane")]
		[Display(Name="Imiê")]
        [StringLength(50)]
        public string Name { get; set; }

		[Required(ErrorMessage = "Pole nazwisko jest wymagane")]
		[Display(Name = "Nazwisko")]
		[StringLength(50)]
        public string Surname { get; set; }

		[Display(Name = "Rok urodzenia")]
        public int BirthYear { get; set; }
    }
}
