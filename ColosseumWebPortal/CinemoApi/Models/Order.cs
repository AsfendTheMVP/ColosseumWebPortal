using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CinemoApi.Models
{
    public class Order
    {
    
        [Key]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(300, ErrorMessage = "The field {0} can contain maximun {1} characters length")]
        [Display(Name = "Movie Name")]
        public string MovieName { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(500, ErrorMessage = "The field {0} can contain maximun {1} characters length")]
        [Display(Name = "Customer Name ")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [Display(Name = "Booking Date")]
        public string BookingDate { get; set; }

        [Display(Name = "Ticket Quantity")]
        public string Qty { get; set; }


        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(100, ErrorMessage = "The field {0} can contain maximun {1} characters length")]
        [Display(Name = "Customer Phone")]

        public string Phone { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(100, ErrorMessage = "The field {0} can contain maximun {1} characters length")]
        [Display(Name = "Customer Email")]
        public string Email { get; set; }

        [Display(Name = "Total Due's")]
        public string TotalPayment { get; set; }

        


    }
}