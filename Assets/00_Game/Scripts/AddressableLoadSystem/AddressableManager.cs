using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = System.Object;

namespace Cred.AddressableLoadSystem{
    public class AddressableManager : MonoBehaviour
    {
        [SerializeField]List<Material> materialList = new List<Material>();
        AsyncOperationHandle callBack;
        public bool IsLoaded => callBack.IsDone;
        public List<Material> MaterialList => materialList;

        public int AssetGroupReferencesCount => materialList.Count;//TODO: For testing remove this!
        
        public void LoadAssetPackageAsync(string reference){
            callBack = Addressables.LoadAssetsAsync<Material>(reference, op => {
                var asset = op;
                if (asset != null){
                    materialList.Add(asset);
                }
            });
        }
        
    }

}
