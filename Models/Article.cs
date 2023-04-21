namespace CRUD_Articulos_y_Precios.Models;

public class Article {
    public int Id { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string? Category { get; set; }
}