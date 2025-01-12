﻿using DrivingSchool.Core.Enum;

namespace DrivingSchool.API.Contracts.UserContracts
{
    public record UsersRequest(
        string UserName,
        string Email,
        string Password
        );
}
