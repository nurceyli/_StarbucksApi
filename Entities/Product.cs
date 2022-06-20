using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace _StarbucksApi.Entities
{
    public class Product
    {
        public Product()
        {
            Comments = new List<Comment>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public DateTime AddTime { get; set; }
        public List<Comment> Comments { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
