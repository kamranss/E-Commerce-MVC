﻿namespace AllUp2.Services.EmailService
{
    public interface IEmailService
    {
        void Send(string to, string subject, string body);
    }
}
