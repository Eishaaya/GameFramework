using System;
using System.Collections.Generic;
using System.Text;

namespace BaseGameLibrary
{
    public interface IParamFunc<TReturn>
    {
        TReturn Call();
    }
    public interface ISemiParamFunc<T1, TReturn>
    {
        TReturn Call(T1 par);
    }
    //public interface ISemiParamFunc<T1, T2, TReturn>
    //{
    //    TReturn Call(T1 par1, T2 par2);
    //}
    //public interface ISemiParamFunc<T1, T2, T3, TReturn>
    //{
    //    TReturn Call(T1 par1, T2 par2, T3 par3);
    //}

    public interface IParamAction
    {
        void Call();
    }
    public interface ISemiParamAction<T1>
    {
        void Call(T1 par1);
    }

    #region Funcs
    public struct ParamFunc<T1, TReturn> : IParamFunc<TReturn>
    {
        public T1 Parameter1 { get; set; }

        Func<T1, TReturn> containedFunc;

        public TReturn Call()
        {
            return containedFunc(Parameter1);
        }

        public ParamFunc(Func<T1, TReturn> func, T1 parameter)
        {
            containedFunc = func;
            Parameter1 = parameter;
        }
    }

    public struct ParamFunc<T1, T2, TReturn> : IParamFunc<TReturn>
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

    public struct SemiParamFunc<T1, T2, TReturn> : ISemiParamFunc<T1, TReturn>
    {
        T2 parameter2;
        Func<T1, T2, TReturn> containedFunc;

        public TReturn Call(T1 par)
        {
            return containedFunc(par, parameter2);
        }

        public SemiParamFunc(Func<T1, T2, TReturn> func, T2 par2)
        {
            containedFunc = func;
            parameter2 = par2;
        }
    }

    public struct ParamFunc<T1, T2, T3, TReturn> : IParamFunc<TReturn>
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

    public struct SemiParamFunc<T1, T2, T3, TReturn> : ISemiParamFunc<T1, TReturn>
    {
        T2 parameter2;
        T3 parameter3;
        Func<T1, T2, T3, TReturn> containedFunc;

        public TReturn Call(T1 par1)
        {
            return containedFunc(par1, parameter2, parameter3);
        }

        public SemiParamFunc(Func<T1, T2, T3, TReturn> func, T2 par2, T3 par3)
        {
            containedFunc = func;
            parameter2 = par2;
            parameter3 = par3;
        }
    }

    public struct ParamFunc<T1, T2, T3, T4, TReturn> : IParamFunc<TReturn>
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

    public struct SemiParamFunc<T1, T2, T3, T4, TReturn> : ISemiParamFunc<T1, TReturn>
    {
        T2 parameter2;
        T3 parameter3;
        T4 parameter4;
        Func<T1, T2, T3, T4, TReturn> containedFunc;

        public TReturn Call(T1 par1)
        {
            return containedFunc(par1, parameter2, parameter3, parameter4);
        }

        public SemiParamFunc(Func<T1, T2, T3, T4, TReturn> func, T2 par2, T3 par3, T4 par4)
        {
            containedFunc = func;
            parameter2 = par2;
            parameter3 = par3;
            parameter4 = par4;
        }
    }

    public struct ParamFunc<T1, T2, T3, T4, T5, TReturn> : IParamFunc<TReturn>
    {
        T1 parameter1;
        T2 parameter2;
        T3 parameter3;
        T4 parameter4;
        T5 parameter5;
        Func<T1, T2, T3, T4, T5, TReturn> containedFunc;

        public TReturn Call()
        {
            return containedFunc(parameter1, parameter2, parameter3, parameter4, parameter5);
        }

        public ParamFunc(Func<T1, T2, T3, T4, T5, TReturn> func, T1 par1, T2 par2, T3 par3, T4 par4, T5 par5)
        {
            containedFunc = func;
            parameter1 = par1;
            parameter2 = par2;
            parameter3 = par3;
            parameter4 = par4;
            parameter5 = par5;
        }
    }

    public struct SemiParamFunc<T1, T2, T3, T4, T5, TReturn> : ISemiParamFunc<T1, TReturn>
    {
        T2 parameter2;
        T3 parameter3;
        T4 parameter4;
        T5 parameter5;
        Func<T1, T2, T3, T4, T5, TReturn> containedFunc;

        public TReturn Call(T1 par1)
        {
            return containedFunc(par1, parameter2, parameter3, parameter4, parameter5);
        }

        public SemiParamFunc(Func<T1, T2, T3, T4, T5, TReturn> func, T2 par2, T3 par3, T4 par4, T5 par5)
        {
            containedFunc = func;
            parameter2 = par2;
            parameter3 = par3;
            parameter4 = par4;
            parameter5 = par5;
        }
    }

    public struct ParamFunc<T1, T2, T3, T4, T5, T6, TReturn> : IParamFunc<TReturn>
    {
        T1 parameter1;
        T2 parameter2;
        T3 parameter3;
        T4 parameter4;
        T5 parameter5;
        T6 parameter6;
        Func<T1, T2, T3, T4, T5, T6, TReturn> containedFunc;

        public TReturn Call()
        {
            return containedFunc(parameter1, parameter2, parameter3, parameter4, parameter5, parameter6);
        }

        public ParamFunc(Func<T1, T2, T3, T4, T5, T6, TReturn> func, T1 par1, T2 par2, T3 par3, T4 par4, T5 par5, T6 par6)
        {
            containedFunc = func;
            parameter1 = par1;
            parameter2 = par2;
            parameter3 = par3;
            parameter4 = par4;
            parameter5 = par5;
            parameter6 = par6;
        }
    }

    public struct SemiParamFunc<T1, T2, T3, T4, T5, T6, TReturn> : ISemiParamFunc<T1, TReturn>
    {
        T2 parameter2;
        T3 parameter3;
        T4 parameter4;
        T5 parameter5;
        T6 parameter6;

        Func<T1, T2, T3, T4, T5, T6, TReturn> containedFunc;

        public TReturn Call(T1 par1)
        {
            return containedFunc(par1, parameter2, parameter3, parameter4, parameter5, parameter6);
        }

        public SemiParamFunc(Func<T1, T2, T3, T4, T5, T6, TReturn> func, T2 par2, T3 par3, T4 par4, T5 par5, T6 par6)
        {
            containedFunc = func;
            parameter2 = par2;
            parameter3 = par3;
            parameter4 = par4;
            parameter5 = par5;
            parameter6 = par6;
        }
    }

    #endregion

    #region Actions
    public struct ParamAction<T1> : IParamAction
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

    public struct ParamAction<T1, T2> : IParamAction
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

    public struct SemiParamAction<T1, T2> : ISemiParamAction<T1>
    {
        T2 parameter2;
        Action<T1, T2> containedFunc;

        public void Call(T1 par1)
        {
            containedFunc(par1, parameter2);
        }

        public SemiParamAction(Action<T1, T2> func, T2 par2)
        {
            containedFunc = func;
            parameter2 = par2;
        }
    }

    public struct ParamAction<T1, T2, T3> : IParamAction
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

    public struct SemiParamAction<T1, T2, T3> : ISemiParamAction<T1>
    {
        T2 parameter2;
        T3 parameter3;
        Action<T1, T2, T3> containedFunc;

        public void Call(T1 par1)
        {
            containedFunc(par1, parameter2, parameter3);
        }

        public SemiParamAction(Action<T1, T2, T3> func, T2 par2, T3 par3)
        {
            containedFunc = func;
            parameter2 = par2;
            parameter3 = par3;
        }
    }

    public struct ParamAction<T1, T2, T3, T4> : IParamAction
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

    public struct SemiParamAction<T1, T2, T3, T4> : ISemiParamAction<T1>
    {
        T2 parameter2;
        T3 parameter3;
        T4 parameter4;
        Action<T1, T2, T3, T4> containedFunc;

        public void Call(T1 par1)
        {
            containedFunc(par1, parameter2, parameter3, parameter4);
        }

        public SemiParamAction(Action<T1, T2, T3, T4> func, T2 par2, T3 par3, T4 par4)
        {
            containedFunc = func;
            parameter2 = par2;
            parameter3 = par3;
            parameter4 = par4;
        }
    }

    public struct ParamAction<T1, T2, T3, T4, T5> : IParamAction
    {
        T1 parameter1;
        T2 parameter2;
        T3 parameter3;
        T4 parameter4;
        T5 parameter5;
        Action<T1, T2, T3, T4, T5> containedFunc;

        public void Call()
        {
            containedFunc(parameter1, parameter2, parameter3, parameter4, parameter5);
        }

        public ParamAction(Action<T1, T2, T3, T4, T5> func, T1 par1, T2 par2, T3 par3, T4 par4, T5 par5)
        {
            containedFunc = func;
            parameter1 = par1;
            parameter2 = par2;
            parameter3 = par3;
            parameter4 = par4;
            parameter5 = par5;
        }
    }

    public struct SemiParamAction<T1, T2, T3, T4, T5> : ISemiParamAction<T1>
    {
        T2 parameter2;
        T3 parameter3;
        T4 parameter4;
        T5 parameter5;
        Action<T1, T2, T3, T4, T5> containedFunc;

        public void Call(T1 par1)
        {
            containedFunc(par1, parameter2, parameter3, parameter4, parameter5);
        }

        public SemiParamAction(Action<T1, T2, T3, T4, T5> func, T2 par2, T3 par3, T4 par4, T5 par5)
        {
            containedFunc = func;
            parameter2 = par2;
            parameter3 = par3;
            parameter4 = par4;
            parameter5 = par5;
        }
    }

    public struct ParamAction<T1, T2, T3, T4, T5, T6> : IParamAction
    {
        T1 parameter1;
        T2 parameter2;
        T3 parameter3;
        T4 parameter4;
        T5 parameter5;
        T6 parameter6;
        Action<T1, T2, T3, T4, T5, T6> containedFunc;

        public void Call()
        {
            containedFunc(parameter1, parameter2, parameter3, parameter4, parameter5, parameter6);
        }

        public ParamAction(Action<T1, T2, T3, T4, T5, T6> func, T1 par1, T2 par2, T3 par3, T4 par4, T5 par5, T6 par6)
        {
            containedFunc = func;
            parameter1 = par1;
            parameter2 = par2;
            parameter3 = par3;
            parameter4 = par4;
            parameter5 = par5;
            parameter6 = par6;
        }
    }

    public struct SemiParamAction<T1, T2, T3, T4, T5, T6> : ISemiParamAction<T1>
    {
        T2 parameter2;
        T3 parameter3;
        T4 parameter4;
        T5 parameter5;
        T6 parameter6;
        Action<T1, T2, T3, T4, T5, T6> containedFunc;

        public void Call(T1 par1)
        {
            containedFunc(par1, parameter2, parameter3, parameter4, parameter5, parameter6);
        }

        public SemiParamAction(Action<T1, T2, T3, T4, T5, T6> func, T2 par2, T3 par3, T4 par4, T5 par5, T6 par6)
        {
            containedFunc = func;
            parameter2 = par2;
            parameter3 = par3;
            parameter4 = par4;
            parameter5 = par5;
            parameter6 = par6;
        }
    }
    #endregion
}
