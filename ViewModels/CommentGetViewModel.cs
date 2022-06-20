using _StarbucksApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _StarbucksApi.ViewModels
{
    public class CommentGetViewModel
    {

            public int Id { get; set; }
            public string UserName { get; set; }
            public string Text { get; set; }
            public string ProductName { get; set; }
            public string CategoryName { get; set; }
        
    }
}

