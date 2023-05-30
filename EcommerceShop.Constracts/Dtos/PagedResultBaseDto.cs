namespace EcommerceShop.Contracts.Dtos
{
    public class PagedResultBaseDto
    {
        public int PageSize {get; set;}
        public int PageIndex {get; set;}
        public int TotalRecords {get; set;}
        public int TotalPages 
        {
            get
            {
                var pages = (double)TotalRecords / PageSize;
                return (int)Math.Ceiling(pages);
            }
        }
        
    }
}