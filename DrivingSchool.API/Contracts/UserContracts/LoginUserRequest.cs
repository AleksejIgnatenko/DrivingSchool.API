namespace DrivingSchool.API.Contracts.UserContracts
{
    public record LoginUserRequest(
        string Email,
        string Password
        );
}
