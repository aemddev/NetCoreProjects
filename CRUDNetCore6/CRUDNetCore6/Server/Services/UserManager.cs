using CRUDNetCore6.Server.Models;
using CRUDNetCore6.Shared;
using Microsoft.EntityFrameworkCore;

namespace CRUDNetCore6.Server.Services
{
    public class UserManager : IUser
    {
        readonly NetCore6CrudContext dbContext = new();

        public UserManager(NetCore6CrudContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<User> DataUsers()
        {
            try
            {
                return dbContext.Users.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public void DeleteUser(int id)
        {
			try
			{
                User? user = dbContext.Users.Find(id);
                if (user != null)
                {
                    dbContext.Users.Remove(user);
                    dbContext.SaveChanges();
                }
                else
                {
                    throw new ArgumentNullException();
                }
			}
			catch (Exception ex)
			{
				throw new Exception(ex.ToString());
			}
		}

        public User GetUser(int id)
        {
            try
            {
                User? user = dbContext.Users.Find(id);
                if (user != null)
                {
                    return user;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public void NewUser(User user)
        {
			try
			{
				user.StartDate = DateTime.Now;
                dbContext.Users.Add(user);
                dbContext.SaveChanges();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.ToString());
			}
		}

        public void UpdateUser(User user)
        {
			try
			{
                dbContext.Entry(user).State = EntityState.Modified;
				dbContext.SaveChanges();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.ToString());
			}
		}
    }
}
