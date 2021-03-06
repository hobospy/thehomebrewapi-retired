﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace homebrewAppServerAPI.Domain.Models
{
    [DataContract]
    public class Ingredient
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public ETypeOfIngredient Type { get; set; }

        [Required]
        public double Amount { get; set; }

        [Required]
        [DataMember]
        public EUnitOfMeasure Unit { get; set; }

        public int RecipeStepID { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        [ForeignKey("RecipeStepID")]
        public RecipeStep RecipeStep { get; set; }

        //public int RecipeID { get; set; }
        //[JsonIgnore]
        //[IgnoreDataMember]
        //[ForeignKey("RecipeID")]
        //public Recipe Recipe { get; set; }
    }
}
