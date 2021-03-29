using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace ClientMissions.Data{
    [Serializable] public class SavableDialogData{
        [SerializeField] int clubIndex;
        [SerializeField] int dialogIndex;

        public SavableDialogData(int clubIndex, int dialogIndex){
            this.clubIndex = clubIndex;
            this.dialogIndex = dialogIndex;
        }

        public int ClubIndex => clubIndex;

        public int DialogIndex => dialogIndex;
    }
}