using System.Collections;

namespace LoginScene {
    public interface IDataBase {
        IEnumerator Login(string username, string password);
        IEnumerator SignUp(string username, string password);
    }
}