﻿namespace SimpleDiplomBackend.Application.Shared.Interface
{
    public interface IGitHubService
    {
        Task<string> LoadAccountAsync(string username);
    }
}