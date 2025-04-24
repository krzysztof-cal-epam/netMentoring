namespace CatalogService.Application.Dto
{
    public class LinkDto
    {
        public string Href { get; set; }
        public string Method { get; set; }
    }

    public class LinksDto
    {
        public LinkDto Self { get; set; }
        public LinkDto Update { get; set; }
        public LinkDto Delete { get; set; }
    }
}
