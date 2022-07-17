using DoPooo.ViewModel;
using DB.Entities;
using DoPooo.Encyption;

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
                Password = SHA256Encryption.EncryptText(userRegistrationViewModel.Password)
            };

            return user;
        }
    }
}
