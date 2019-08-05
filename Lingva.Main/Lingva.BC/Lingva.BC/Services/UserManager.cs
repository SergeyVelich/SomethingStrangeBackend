using Lingva.Additional.Mapping.DataAdapter;
using Lingva.BC.Contracts;
using Lingva.DAL.Repositories;

namespace Lingva.BC.Services
{
    public class UserManager : IUserManager
    {
        private readonly IRepository _userRepository;
        private readonly IDataAdapter _dataAdapter;
           
        public UserManager(IRepository userRepository, IDataAdapter dataAdapter)
        {
            _userRepository = userRepository;
            _dataAdapter = dataAdapter;
        }    
    }
}

