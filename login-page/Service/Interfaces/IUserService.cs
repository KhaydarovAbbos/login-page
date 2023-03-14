using login_page.Entities.User;
using login_page.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace login_page.Service.Interfaces
{
    public interface IUserService
    {

        Task<User> UserLogin(UserLoginViewModel userLoginViewModel);
        
        Task<User> UserSignUp(UserSignUpViewModel userSignUpViewModel);

        Task<User> CheckUser(UserSignUpViewModel userSignUpViewModel);

    }
}
