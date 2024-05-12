﻿namespace BusinessAccessLayer.DTOS
{
    public class Pagination
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string[]? OrderBy { get; set; }
        public string? Search { get; set; }
    }
}
