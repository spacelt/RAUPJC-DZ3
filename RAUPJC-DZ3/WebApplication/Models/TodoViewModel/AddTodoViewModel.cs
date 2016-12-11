using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models.TodoViewModel
{
    public class AddTodoViewModel
    {
        [Required, MinLength(1)]
        public string Text { get; set; }
    }
}
