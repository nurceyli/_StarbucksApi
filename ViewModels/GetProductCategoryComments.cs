using _StarbucksApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _StarbucksApi.ViewModels
{
    public class GetProductCategoryComments
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public DateTime AddTime { get; set; }
        public double Price { get; set; }
        public List<Comment> Comments { get; set; }
        public string CategoryName { get; set; }

    }
}
