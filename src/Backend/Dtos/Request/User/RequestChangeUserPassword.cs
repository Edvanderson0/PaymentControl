﻿namespace PaymentControl.Dtos.Request.User
{
    public class RequestChangeUserPassword
    {
        public string Email { get; set; } = string.Empty;
        public string CurrentPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}
