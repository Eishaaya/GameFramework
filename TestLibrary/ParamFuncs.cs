using System;
using System.Collections.Generic;
using System.Text;

namespace BaseGameLibrary
{
    public interface IParamFunc<TReturn>
    {
        TReturn Call();
    }

    //public class ParamFunc<TReturn> : IParamFunc<TReturn>
    //{
    //    object[] parameters;
    //    Func<object[], TReturn> containedFunc;

    //    public TReturn Call()
    //    {
    //        return containedFunc(parameters);
    //    }

    //    public ParamFunc(Func<object[], TReturn> func, params object[] funcParams)
    //    {
    //        containedFunc = func;
    //        parameters = funcParams;
    //    }
    //}

    public class ParamFunc<T1, TReturn> : IParamFunc<TReturn>
    {
        T1 parameter1;
        Func<T1, TReturn> containedFunc;

        public TReturn Call()
        {
            return containedFunc(parameter1);
        }

        public ParamFunc(Func<T1, TReturn> func, T1 parameter)
        {
            containedFunc = func;
            parameter1 = parameter;
        }
    }

    public class ParamFunc<T1, T2, TReturn> : IParamFunc<TReturn>
    {
        T1 parameter1;
        T2 parameter2;
        Func<T1, T2, TReturn> containedFunc;

        public TReturn Call()
        {
            return containedFunc(parameter1, parameter2);
        }

        public ParamFunc(Func<T1, T2, TReturn> func, T1 par1, T2 par2)
        {
            containedFunc = func;
            parameter1 = par1;
            parameter2 = par2;
        }
    }

    public class ParamFunc<T1, T2, T3, TReturn> : IParamFunc<TReturn>
    {
        T1 parameter1;
        T2 parameter2;
        T3 parameter3;
        Func<T1, T2, T3, TReturn> containedFunc;

        public TReturn Call()
        {
            return containedFunc(parameter1, parameter2, parameter3);
        }

        public ParamFunc(Func<T1, T2, T3, TReturn> func, T1 par1, T2 par2, T3 par3)
        {
            containedFunc = func;
            parameter1 = par1;
            parameter2 = par2;
            parameter3 = par3;
        }
    }
}
