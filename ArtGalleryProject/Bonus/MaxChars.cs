using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ArtGalleryProject.Bonus
{
  //kontrollon nr max te karaktereve qe mund te kete nje fjale p.sh. nqs maxchars=10 nuk do te lejohen fjalet 
  //me me shume se 10 karaktere
  public class MaxChars:ValidationAttribute
  {
    private readonly int maxchars;
      
    public MaxChars(int nr):base("{0} permban fjale me me shume se "+nr.ToString()+" karaktere.")
    {
      maxchars = nr;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
      if (value != null)
      {
        var str= value.ToString();
        string[] strArray = str.Split(' ');
        for(int i=0; i<strArray.Length; i++)
        {
          if (strArray[i].Length > maxchars)
          {
            var error = FormatErrorMessage(validationContext.DisplayName);
            return new ValidationResult(error);
          }
        }

      }
      return ValidationResult.Success;
    }

  }
}