﻿namespace SimpleDiplomBackend.Application.Features.Review.Queries.GetAll
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public string ShortName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public short? Rating { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Me { get; set; }
    }
}