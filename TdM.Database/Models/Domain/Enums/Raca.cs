using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TdM.Database.Models.Domain.Enums
{
    public enum Raca
    {

        Dwarf,
        Elf,
        Halfling,
        Human,
        Dragonborn,
        Gnome,
        [Display (Name="Half-Elf")]
        Halfelf,
        [Display(Name = "Half-Orc")]
        Halforc,
        Tiefling,
        Aarakocra,
        Genasi,
        Goliath,
        Tabaxi,
        Kobold,
        Kenku,
        Tortle,
        Harengon,
        Shifter,
        Githyanki,
        Githzerai,
        Bugbear,
        Warforged,
        Owlin,
        Aasimar,

        //Lahan
        Karna,
        Humano,
        Geera,
        Torval,
        Yazu,
        Lilin,
        Sidon,
        Taito,
        Liliden

    }
}
