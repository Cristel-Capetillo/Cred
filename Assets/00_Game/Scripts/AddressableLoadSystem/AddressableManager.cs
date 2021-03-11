using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Linq;
using UnityEditor;
using Object = System.Object;

namespace Cred.AddressableLoadSystem{
    public class AddressableManager : MonoBehaviour
    {
        //TODO: Sorting data (in this class?)
        //TODO: Implement assetLoading for 3d models client, wearables etc...(in this class?)
        //TODO: Check player level...
        //TODO: List of assetReferences to levels that holds ScriptableObjects...
        //TODO: Derive this lists from ScriptableObjects:
        [SerializeField]List<AssetLabelReference> assetLabelReferences = new List<AssetLabelReference>();
        [SerializeField]List<Material> loadedAssets = new List<Material>();//TODO:Change dataType
        int _activeAsyncOperations;
        public List<AssetLabelReference> AssetLabelReferences => assetLabelReferences;
        public int ListCount => loadedAssets.Count;

        //TODO:Implement a start method...
        public void PrepareLoadingMultipleAssetGroups(IEnumerable<AssetLabelReference> labelReferences){
            foreach (var labelReference in labelReferences){ 
                LoadAssetGroup(labelReference);
            }
        }
        //TODO: Change DataType
        public void LoadAssetGroup(AssetLabelReference assetLabelReference){
            Addressables.LoadAssetsAsync<Material>(assetLabelReference, asset => {
                if (asset == null) return;
                loadedAssets.Add(asset);
                Debug.Log($"Adding: {asset.name}");
            }).Completed += OnComplete;
            _activeAsyncOperations++;
        }
        //TODO: Change DataType, Send data to broker>Inventory!
        void OnComplete(AsyncOperationHandle<IList<Material>> obj){
            if(_activeAsyncOperations > 0)
                _activeAsyncOperations--;
            Debug.Log(_activeAsyncOperations <= 0 ? $"Send Loaded assets: full list {loadedAssets.Count} or latest list {obj.Result.Count}" : $"Active async operations: {_activeAsyncOperations}");
        }
    }
    //TODO: Remove TODOS and debug logs... :)
}
