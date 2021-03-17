using System;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem {
    public class SavePreview : MonoBehaviour {
    }


    //How to save dictionaries example (TValue has to be of type object:
    public class SaveDictionary : MonoBehaviour, ISavable<Dictionary<string, object>> {
        Dictionary<string, object> saveDictionary = new Dictionary<string, object>();
        SaveHandler saveHandler;

        void Start() {
            saveHandler = new SaveHandler(name);
            saveHandler.Save(this);
            saveHandler.Load(this);
        }

        public Dictionary<string, object> ToBeSaved()
            => saveDictionary;


        public void OnLoad(Dictionary<string, object> value)
            => saveDictionary = value;
    }

    //How to save lists example (has to of type object):
    public class SaveList : MonoBehaviour, ISavable<List<object>> {
        List<object> saveList = new List<object>();
        SaveHandler saveHandler;

        void Start() {
            saveHandler = new SaveHandler(name);
            saveHandler.Save(this);
            saveHandler.Load(this);
        }

        public List<object> ToBeSaved()
            => saveList;


        public void OnLoad(List<object> value)
            => saveList = value;
    }
}