namespace CatalogService.Application.Dto
{
    public class ProductWithLinksDto : ProductDto
    {
        public LinksDto Links { get; set; } = new LinksDto();
    }
}
