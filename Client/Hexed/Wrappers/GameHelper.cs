namespace Hexed.Wrappers
{
    internal class GameHelper
    {
        public static User GetCurrentUser()
        {
           return Backend.Instance.User;
        }
    }
}
