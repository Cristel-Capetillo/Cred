using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Cred.AddressableLoadSystem{
    public class AddressableManager : MonoBehaviour
    {
        //TODO: Change to asset label ref
        [SerializeField]List<AssetReference> assetReferences = new List<AssetReference>();
        public int AssetGroupReferencesCount => assetReferences.Count;//TODO: For testing remove this!
        
        public void LoadAddressableDataBase(){
            throw new Exception("Not implemented yet!");
        }
    }

}
