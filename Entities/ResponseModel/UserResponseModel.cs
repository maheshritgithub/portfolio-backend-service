using System.ComponentModel.DataAnnotations;

namespace Portfolio.Entities;

public class UserResponseModel : UserRequestModel
{
    public Guid Id { get; set; }
}
