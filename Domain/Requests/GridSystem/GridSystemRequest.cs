using System.ComponentModel.DataAnnotations;
using GridStatusHub.Domain.Requests.GridCell;

namespace GridStatusHub.Domain.Requests.GridSystem 
{
    public class GridSystemRequest
    {
        public int Id { get; set; }  
        [StringLength(15, ErrorMessage = "Name cannot be longer than 15 characters.")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only alphanumeric characters are allowed.")]
        public string Name { get; set; } = string.Empty; 
        public DateTime? EstablishmentDate { get; set; } 
        public ICollection<GridCellRequest>? GridCells { get; set; }
    }
} 