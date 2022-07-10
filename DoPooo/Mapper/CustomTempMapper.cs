using DoPooo.ViewModel;
using DB.Entities;

namespace DoPooo.Mapper
{
    public class CustomTempMapper : ICustomTempMapper
    {
        public User MapToUser(UserRegistrationViewModel userRegistrationViewModel)
        {
            var user = new User()
            {
                Surname = userRegistrationViewModel.LastName,
                Name = userRegistrationViewModel.FirstName,
                Email = userRegistrationViewModel.Email,
                Password = userRegistrationViewModel.Password
            };

            return user;
        }
    }
}
