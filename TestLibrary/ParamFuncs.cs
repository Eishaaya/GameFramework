using System;
using System.Collections.Generic;
using System.Text;

namespace BaseGameLibrary
{
    public interface IParamFunc<TReturn>
    {
        TReturn Call();
    }
    public interface IParamAction
    {
        void Call();
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
    #region funcs
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

    public class ParamFunc<T1, T2, T3, T4, TReturn> : IParamFunc<TReturn>
    {
        T1 parameter1;
        T2 parameter2;
        T3 parameter3;
        T4 parameter4;
        Func<T1, T2, T3, T4, TReturn> containedFunc;

        public TReturn Call()
        {
            return containedFunc(parameter1, parameter2, parameter3, parameter4);
        }

        public ParamFunc(Func<T1, T2, T3, T4, TReturn> func, T1 par1, T2 par2, T3 par3, T4 par4)
        {
            containedFunc = func;
            parameter1 = par1;
            parameter2 = par2;
            parameter3 = par3;
            parameter4 = par4;
        }
    }
    #endregion

    #region actions
    public class ParamAction<T1> : IParamAction
    {
        T1 parameter1;
        Action<T1> containedFunc;

        public void Call()
        {
            containedFunc(parameter1);
        }

        public ParamAction(Action<T1> func, T1 parameter)
        {
            containedFunc = func;
            parameter1 = parameter;
        }
    }

    public class ParamAction<T1, T2> : IParamAction
    {
        T1 parameter1;
        T2 parameter2;
        Action<T1, T2> containedFunc;

        public void Call()
        {
            containedFunc(parameter1, parameter2);
        }

        public ParamAction(Action<T1, T2> func, T1 par1, T2 par2)
        {
            containedFunc = func;
            parameter1 = par1;
            parameter2 = par2;
        }
    }

    public class ParamAction<T1, T2, T3> : IParamAction
    {
        T1 parameter1;
        T2 parameter2;
        T3 parameter3;
        Action<T1, T2, T3> containedFunc;

        public void Call()
        {
            containedFunc(parameter1, parameter2, parameter3);
        }

        public ParamAction(Action<T1, T2, T3> func, T1 par1, T2 par2, T3 par3)
        {
            containedFunc = func;
            parameter1 = par1;
            parameter2 = par2;
            parameter3 = par3;
        }
    }

    public class ParamAction<T1, T2, T3, T4> : IParamAction
    {
        T1 parameter1;
        T2 parameter2;
        T3 parameter3;
        T4 parameter4;
        Action<T1, T2, T3, T4> containedFunc;

        public void Call()
        {
            containedFunc(parameter1, parameter2, parameter3, parameter4);
        }

        public ParamAction(Action<T1, T2, T3, T4> func, T1 par1, T2 par2, T3 par3, T4 par4)
        {
            containedFunc = func;
            parameter1 = par1;
            parameter2 = par2;
            parameter3 = par3;
            parameter4 = par4;
        }
    }
    #endregion
}
