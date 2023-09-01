﻿namespace SchoolProject.Core.Features.Authentication.Queries.Models;

public class ResetPasswordQuery : IRequest<Response<string>>
{
    public string Code { get; set; }
    public string Email { get; set; }

}