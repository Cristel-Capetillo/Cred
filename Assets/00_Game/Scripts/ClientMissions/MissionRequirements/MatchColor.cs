﻿using System.Collections.Generic;
using System.Linq;
using ClientMissions.Data;
using ClientMissions.MissionRequirements;
using Clothing;

namespace Club.MissionRequirments{
    public class MatchColor: IMissionRequirement{
        public readonly ColorData ColorData;
        public MatchColor(ColorData colorData) {
            ColorData = colorData;
        }

        public bool PassedRequirement(CombinedWearables combinedWearables){
            return combinedWearables.wearable.Any(wearable => wearable.colorData == ColorData);
        }
        // public SavableRequirementData ToSavable(){
        //     return new SavableRequirementData(0, new List<int>(0));
        // }
        //
        // public static IMissionRequirement FromSavable(SavableRequirementData savableRequirementData){
        //     //return new MatchColor(generatorData.Colors[requirementData.RequirementsDataIndex[0]]));
        //     return null;
        // }

        public override string ToString(){
            return $"Any {ColorData.name.ToLower()} clothing.";
        }
    }
}