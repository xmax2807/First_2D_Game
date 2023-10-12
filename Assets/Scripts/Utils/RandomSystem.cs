using System;
using System.Collections.Generic;
using System.Linq;

public static class RandomSystem{
    public static T[] TakeRandomElements<T>(T[] originList, int amount){
        T[] Result = new T[amount];

        HashSet<int> AvailableIndexes = new();
        Random rand = new();

        amount = Math.Min(amount, originList.Length);
        for(int i = 0; i < amount; i++){
            int index;
            do{
                index = rand.Next(0, originList.Length);
            }
            while(AvailableIndexes.Contains(index));

            Result[i] = originList[index];
            AvailableIndexes.Add(index);
        }
        return Result;
    }
}