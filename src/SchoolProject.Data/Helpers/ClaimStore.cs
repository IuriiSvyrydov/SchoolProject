﻿using System.Security.Claims;

namespace SchoolProject.Data.Helpers;

public static class ClaimStore
{
    public static List<Claim> Claims = new()
    {
        new Claim("Create Student", "false"),
        new Claim("Edit Student", "false"),
        new Claim("Delete Student", "false"),

    };


}