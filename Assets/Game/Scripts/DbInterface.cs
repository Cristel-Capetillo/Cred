using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cred
{
    public interface IDataBase
    {
        void Login(string username, string password);
        
        void CreateAccount(string username, string password);

        
        
    }
}
