using AiursoftBase.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AiursoftBase.Models
{
    public enum Category
    {
        Development = 0,
        AppForAiur = 1,
        AppForPages = 2,
        Books = 3,
        Business = 4,
        Communication = 5,
        Education = 6,
        Entertainment = 7,
        Fashion = 8,
        Finance = 9,
        FoodAndDrink = 10,
        Games = 11
    }
    public enum Platform
    {
        CrossPlatform = 0,
        Android = 1,
        iOS = 2,
        WindowsDesktop = 3,
        WindowsUWP = 4,
        Linux = 5,
        Mac = 6,
        Web = 7
    }
}
