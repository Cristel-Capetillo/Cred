using System.Collections.Generic;
using Clothing;
using Clothing.Inventory;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Utilities;

namespace AddressableLoadSystem {
    public class AddressableManager : MonoBehaviour {
        //TODO: Sorting data (in this class?)
        //TODO: Implement assetLoading for 3d models client, wearables etc...(in this class?)
        //TODO: Check player level...
        //TODO: List of assetReferences to levels that holds ScriptableObjects...
        //TODO: Derive this lists from ScriptableObjects:
        [SerializeField] List<AssetLabelReference> assetLabelReferences = new List<AssetLabelReference>();
        [SerializeField] List<CombinedWearables> loadedAssets = new List<CombinedWearables>(); //TODO:Change dataType
        int _activeAsyncOperations;
        public List<AssetLabelReference> AssetLabelReferences => assetLabelReferences;
        public int ListCount => loadedAssets.Count;

        void Start() {
            PrepareLoadingMultipleAssetGroups(assetLabelReferences);
        }

        void PrepareLoadingMultipleAssetGroups(IEnumerable<AssetLabelReference> labelReferences) {
            foreach (var labelReference in labelReferences) {
                LoadAssetGroup(labelReference);
            }
        }

        void LoadAssetGroup(AssetLabelReference assetLabelReference) {
            Addressables.LoadAssetsAsync<CombinedWearables>(assetLabelReference, asset => {
                if (asset == null) return;
                loadedAssets.Add(asset);
                // Debug.Log($"Adding: {asset}");
            }).Completed += OnComplete;
            _activeAsyncOperations++;
        }

        void OnComplete(AsyncOperationHandle<IList<CombinedWearables>> obj) {
            if (_activeAsyncOperations > 0)
                _activeAsyncOperations--;
            var temp = (List<CombinedWearables>) obj.Result;
            EventBroker.Instance().SendMessage(new EventCombinedWearable(temp));
            Debug.Log(_activeAsyncOperations <= 0 ? $"Send Loaded assets: full list {loadedAssets.Count} or latest list {obj.Result.Count}" : $"Active async operations: {_activeAsyncOperations}");
        }
    }

    //TODO: Remove TODOS and debug logs... :)
}