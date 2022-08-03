using System;

namespace BaseGameLibrary
{
    public partial class Extensions
    {
        /// <summary>
        /// Shorthand for ParamAction constructor for readability purposes
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="func"></param>
        /// <param name="par1"></param>
        /// <returns></returns>
        public static ParamAction<T1> GetParamAction<T1>(Action<T1> func, T1 par1) => new ParamAction<T1>(func, par1);
        public static ParamAction<T1, T2> GetParamAction<T1, T2>(Action<T1, T2> func, T1 par1, T2 par2) => new ParamAction<T1, T2>(func, par1, par2); 
        public static ParamAction<T1, T2, T3> GetParamAction<T1, T2, T3>(Action<T1, T2, T3> func, T1 par1, T2 par2, T3 par3) => new ParamAction<T1, T2, T3>(func, par1, par2, par3);
    }
}
