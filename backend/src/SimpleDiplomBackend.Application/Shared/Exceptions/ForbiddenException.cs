﻿namespace SimpleDiplomBackend.Application.Shared.Exceptions
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException(string message)
            : base(message)
        {
        }
    }
}