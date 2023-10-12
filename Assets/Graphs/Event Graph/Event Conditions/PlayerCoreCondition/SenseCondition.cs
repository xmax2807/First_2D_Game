using UnityEngine;
using ExtensionMethods;
using System.Linq;
using System.Collections.Generic;
using bf = System.Reflection.BindingFlags;

[System.Serializable]
public struct SenseConditionData
{
    [SerializeField] public bool ShouldTrue;
    [SerializeField] public string MethodName;
}
public class SenseCondition : CoreBoolCondition
{
    [SerializeField]
    private SenseConditionData[] Actions =
    ObjectExtension.GetMethods(typeof(PlayerCollisionSense), typeof(bool), null, bf.Instance | bf.Public)
    .Select((x) =>
        new SenseConditionData
        {
            MethodName = x.Name,
            ShouldTrue = false,
        })
    .ToArray();

    public override string Name => "SenseCondition";
    private readonly Dictionary<string, System.Reflection.MethodInfo> cacheMethods = new();

    protected override bool CheckCoreCondition(Core core)
    {
        bool result = IsTrue;
        foreach (SenseConditionData data in Actions)
        {
            if(!cacheMethods.TryGetValue(data.MethodName, out var method)){
                method = core.Senses.GetMethod(data.MethodName, typeof(bool), null, bf.Instance | bf.Public);
                cacheMethods.Add(data.MethodName, method);
            }
            result = result && (bool)method.Invoke(core.Senses, null) == data.ShouldTrue;
        }
        return result;
    }
}
