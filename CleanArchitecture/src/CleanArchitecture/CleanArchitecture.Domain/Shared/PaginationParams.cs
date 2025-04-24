namespace CleanArchitecture.Domain.Shared
{
    public record PaginationParams
    {
        private const int MaxPageSize = 50;

        public int PageNumber { get; set; } = 1;

        private int _PageSize = 2;

        public int PageSize
        {
            get => _PageSize;
            set => _PageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public string? OrderBy { get; set; }

        public bool OrderAsc { get; set; } = true;

        public string? Search { get; set; }
    }
}
