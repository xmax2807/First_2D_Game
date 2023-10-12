using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace ExtensionMethods
{
    public static class StringExtension
    {

        public static string UpperAtFirst(this string input)
        {
            return input.UpperCaseAtIndex(0);
        }
        public static string UpperCaseAtIndex(this string input, int index)
        {
            if (string.IsNullOrEmpty(input)) return input;

            StringBuilder builder = new();
            builder.Append(input.Substring(0, index))
                .Append(input.Substring(index));

            return builder.ToString();
        }
    }
    public static class ObjectExtension
    {   
        public static List<MethodInfo> GetMethods(Type instanceType ,Type returnType, Type[] paramTypes = null, BindingFlags flags = BindingFlags.Default)
        {
            var list = instanceType.GetMethods(flags)
                .Where(m => m.ReturnType == returnType)
                .Select(m => new { m, Params = m.GetParameters() })
                .Where(x =>
                    {
                        return paramTypes == null ? // in case we want no params
                            x.Params.Length == 0 :
                            x.Params.Length == paramTypes.Length &&
                            x.Params.Select(p => p.ParameterType).ToArray().Equals(paramTypes);
                    })
                .Select(x => x.m)
                .ToList();
            return list;
        }

        public static MethodInfo GetMethod(this UnityEngine.Object instance, string name, Type returnType,Type[] paramTypes = null, BindingFlags flags = BindingFlags.Default){
            return instance.GetType().GetMethods(flags).Where((x)=> x.Name == name && x.ReturnType == returnType).FirstOrDefault();
        }
        public static List<MethodInfo> GetMethods(this UnityEngine.Object mb, Type returnType, Type[] paramTypes = null, BindingFlags flags = BindingFlags.Default)
        {
            return mb.GetType().GetMethods(flags)
                .Where(m => m.ReturnType == returnType)
                .Select(m => new { m, Params = m.GetParameters() })
                .Where(x =>
                    {
                        return paramTypes == null ? // in case we want no params
                            x.Params.Length == 0 :
                            x.Params.Length == paramTypes.Length &&
                            x.Params.Select(p => p.ParameterType).ToArray().Equals(paramTypes);
                    })
                .Select(x => x.m)
                .ToList();
        }
        public static List<MethodInfo> GetMethods(this GameObject go, Type returnType, Type[] paramTypes, BindingFlags flags)
        {
            var mbs = go.GetComponents<MonoBehaviour>();
            List<MethodInfo> list = new();
            foreach (var mb in mbs)
            {
                list.AddRange(mb.GetMethods(returnType, paramTypes, flags));
            }
            return list;
        }
    }
}
