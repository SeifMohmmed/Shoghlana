namespace Shoghlana.API.Services.Implementations;

public class ChatServices
{

    private readonly Dictionary<string, string> Users = new Dictionary<string, string>();


    public bool AddUsersToList(string usertToAdd)
    {
        lock (Users)
        {
            foreach (var user in Users)
            {
                if (user.Key.ToLower() == usertToAdd.ToLower())
                {
                    return false;
                }
            }
        }
        Users.Add(usertToAdd, null);
        return true;
    }

    public void AddUserConnectionID(string user, string connectionID)
    {
        lock (Users)
        {
            if (Users.ContainsKey(user))
            {
                Users[user] = connectionID;
            }
        }
    }

    public string GetUserByConnectionID(string connectionID)
    {
        lock (Users)
        {
            return Users.Where(u => u.Value == connectionID).Select(u => u.Key)
                        .FirstOrDefault();

        }
    }

    public string GetConnectionByUser(string user)
    {
        lock (Users)
        {
            return Users.Where(u => u.Key == user).Select(u => u.Value)
                        .FirstOrDefault();
        }
    }

    public void RemoveUserFromList(string user)
    {
        lock (Users)
        {
            if (Users.ContainsKey(user))
            {
                Users.Remove(user);
            }
        }
    }

    public string[] GetOnlineUsers()
    {
        lock (Users)
        {
            return Users.OrderBy(x => x.Key).Select(x => x.Key).ToArray();
        }
    }

}
