using DB.Entities;
using DoPooo.ViewModel;

namespace DoPooo.Mapper
{
    public interface ICustomTempMapper
    {
        public User MapToUser(UserRegistrationViewModel userRegistrationViewModel);
    }
}
