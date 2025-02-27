using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace VypocetStatiky.Models;

public class InputClass
{
    // Vstup 
    [Display(Name = "Cela cisla")] public string Vstup { get; set; }

    // Operace
    public string Operace { get; set; }
    [Display(Name = "Seradit")] public List<SelectListItem> MozneOperace { get; private set; }

    // List
    private List<int> CislaVListu = new();

    public InputClass()
    {
        Operace = "Vzestupne";
        MozneOperace =
        [
            new SelectListItem { Text = "Vzestupne", Value = "asc", Selected = true },
            new SelectListItem { Text = "Sestupne", Value = "desc" }
        ];
    }

    public List<int> DejCislaDoSeznamu()
    {
        var cislaVListu = Regex.Matches(Vstup, @"\d+")
            .Cast<Match>()
            .Select(m => int.Parse(m.Value))
            .ToList();
        CislaVListu = cislaVListu;
        return CislaVListu;
    }

    public int Secti()
    {
        return CislaVListu.Sum();
    }

    public int NejmensiCislo()
    {
        if (CislaVListu != null && CislaVListu.Any())
        {
            return CislaVListu.Min();
        }

        return 0;
    }

    public int NejvetsiCislo()
    {
        if (CislaVListu != null && CislaVListu.Any())
        {
            return CislaVListu.Max();
        }

        return 0;
    }

    public double Prumer()
    {
        if (CislaVListu != null && CislaVListu.Any())
        {
            return CislaVListu.Average();
        }

        return 0;
    }

    public List<int> Serazeni()
    {
        if (Operace == "asc")
        {
            CislaVListu = SerazenaCislaOdNejmensiho();
        }
        else
        {
            CislaVListu = SerazenaCislaOdNejvetsiho();
        }

        return CislaVListu;
    }

    private List<int> SerazenaCislaOdNejmensiho()
    {
        if (CislaVListu != null && CislaVListu.Any())
        {
            return CislaVListu.OrderBy(x => x).ToList();
        }

        return new List<int>();
    }


    private List<int> SerazenaCislaOdNejvetsiho()
    {
        if (CislaVListu != null && CislaVListu.Any())
        {
            return CislaVListu.OrderByDescending(x => x).ToList();
        }

        return new List<int>();
    }

    public string SerazenaCislaFormatted()
    {
        return string.Join(", ", Serazeni());
    }
}