using login_page.Entities.User;
using login_page.Infrastructure.IRepository;
using login_page.Service.Interfaces;
using login_page.ViewModels;
using System.Threading.Tasks;

namespace login_page.Service.Services
{
    public class UserService : IUserService
    {
        internal IUserRepository UserRepository;

        public UserService(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        public Task<User> CheckUser(UserSignUpViewModel userSignUpViewModel)
        {
            return UserRepository.GetAsync(x => x.Login == userSignUpViewModel.Login);
        }

        public Task<User> UserLogin(UserLoginViewModel userLoginViewModel)
        {
            return UserRepository.GetAsync(x => x.Login == userLoginViewModel.Login && x.Password == userLoginViewModel.Password);
        }

        public Task<User> UserSignUp(UserSignUpViewModel userSignUpViewModel)
        {
            User user = new User
            {
                Login = userSignUpViewModel.Login,
                Password = userSignUpViewModel.Password,
            };

            return UserRepository.CreateAsync(user);
        }
    }
}
