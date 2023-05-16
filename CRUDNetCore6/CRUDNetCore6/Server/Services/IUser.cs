using CRUDNetCore6.Shared;

namespace CRUDNetCore6.Server.Services
{
    public interface IUser
    {
        public List<User> DataUsers();
        public void NewUser (User user);
        public void DeleteUser (int id);
        public User GetUser (int id);
        public void UpdateUser (User user);

    }
}
