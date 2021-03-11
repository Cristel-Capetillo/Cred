using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cred.Scripts
{
    public interface IDataBase
    {
        IEnumerator Login(string username, string password);
        IEnumerator SignUp(string username, string password);

        
        
    }
}
