using System;

namespace BaseGameLibrary
{
    public interface IParamFunc<TReturn>
    {
        TReturn Call();
    }
    //public interface IParamFunc<T1, TReturn> : IParamFunc<TReturn>
    //{
    //    TReturn Call(T1 par);
    //}
    //public interface IParamFunc<T1, T2, TReturn> : IParamFunc<T1, TReturn>
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
    public interface IParamAction<T1> : IParamAction
    {
        void Call(T1 par1);
    }
    public interface IParamAction<T1, T2> : IParamAction<T1>
    {
        void Call(T1 par1, T2 par2);
    }

    #region Funcs
    public abstract class ParamFuncBase<T1, TReturn> : IParamFunc<TReturn>
    {
        protected T1 parameter1;
        public abstract TReturn Call();
        //{
        //   return containedFunc(parameter1);
        //}

        public TReturn Call(T1 par1)
        {
            parameter1 = par1;
            return Call();
        }

        public ParamFuncBase(T1 parameter)
        {
            // containedFunc = func;
            parameter1 = parameter;
        }
    }


    public abstract class ParamFuncBase<T1, T2, TReturn> : ParamFuncBase<T1, TReturn>
    {
        protected T2 parameter2;

        protected ParamFuncBase(T1 par1, T2 par2)
            : base(par1)
        {
            parameter2 = par2;
        }

        public TReturn Call(T1 par1, T2 par2)
        {
            parameter2 = par2;
            return Call(par1);
        }
    }

    public abstract class ParamFuncBase<T1, T2, T3, TReturn> : ParamFuncBase<T1, T2, TReturn>
    {
        protected T3 parameter3;

        protected ParamFuncBase(T1 par1, T2 par2, T3 par3)
            : base(par1, par2)
        {
            parameter3 = par3;
        }

        public TReturn Call(T1 par1, T2 par2, T3 par3)
        {
            parameter3 = par3;
            return Call(par1, par2);
        }
    }

    public abstract class ParamFuncBase<T1, T2, T3, T4, TReturn> : ParamFuncBase<T1, T2, T3, TReturn>
    {
        protected T4 parameter4;

        protected ParamFuncBase(T1 par1, T2 par2, T3 par3, T4 par4)
            : base(par1, par2, par3)
        {
            parameter4 = par4;
        }

        public TReturn Call(T1 par1, T2 par2, T3 par3, T4 par4)
        {
            parameter4 = par4;
            return Call(par1, par2, par3);
        }
    }

    public abstract class ParamFuncBase<T1, T2, T3, T4, T5, TReturn> : ParamFuncBase<T1, T2, T3, T4, TReturn>
    {
        protected T5 parameter5;

        protected ParamFuncBase(T1 par1, T2 par2, T3 par3, T4 par4, T5 par5)
            : base(par1, par2, par3, par4)
        {
            parameter5 = par5;
        }

        public TReturn Call(T1 par1, T2 par2, T3 par3, T4 par4, T5 par5)
        {
            parameter5 = par5;
            return Call(par1, par2, par3, par4);
        }
    }

    public abstract class ParamFuncBase<T1, T2, T3, T4, T5, T6, TReturn> : ParamFuncBase<T1, T2, T3, T4, T5, TReturn>
    {
        protected T6 parameter6;

        protected ParamFuncBase(T1 par1, T2 par2, T3 par3, T4 par4, T5 par5, T6 par6)
            : base(par1, par2, par3, par4, par5)
        {
            parameter6 = par6;
        }

        public TReturn Call(T1 par1, T2 par2, T3 par3, T4 par4, T5 par5, T6 par6)
        {
            parameter6 = par6;
            return Call(par1, par2, par3, par4, par5);
        }
    }
    #region Leaves
    public sealed class ParamFunc<T1, TReturn> : ParamFuncBase<T1, TReturn>
    {
        readonly Func<T1, TReturn> containedFunc;
        public override TReturn Call()
            => containedFunc(parameter1);
        public ParamFunc(Func<T1, TReturn> containedFunc, T1 par1)
            : base(par1)
        {
            this.containedFunc = containedFunc;
        }
    }

    public sealed class ParamFunc<T1, T2, TReturn> : ParamFuncBase<T1, T2, TReturn>
    {
        readonly Func<T1, T2, TReturn> containedFunc;
        public override TReturn Call()
            => containedFunc(parameter1, parameter2);
        public ParamFunc(Func<T1, T2, TReturn> containedFunc, T1 par1, T2 par2)
            : base(par1, par2)
        {
            this.containedFunc = containedFunc;
        }
    }

    public sealed class ParamFunc<T1, T2, T3, TReturn> : ParamFuncBase<T1, T2, T3, TReturn>
    { 
        readonly Func<T1, T2, T3, TReturn> containedFunc;
        public override TReturn Call()
            => containedFunc(parameter1, parameter2, parameter3);
        public ParamFunc(Func<T1, T2, T3, TReturn> containedFunc, T1 par1, T2 par2, T3 par3)
            : base(par1, par2, par3)
        {
            this.containedFunc = containedFunc;
        }
    }

    public sealed class ParamFunc<T1, T2, T3, T4, TReturn> : ParamFuncBase<T1, T2, T3, T4, TReturn>
    {
        readonly Func<T1, T2, T3, T4, TReturn> containedFunc;
        public override TReturn Call()
            => containedFunc(parameter1, parameter2, parameter3, parameter4);
        public ParamFunc(Func<T1, T2, T3, T4, TReturn> containedFunc, T1 par1, T2 par2, T3 par3, T4 par4)
            : base(par1, par2, par3, par4)
        {
            this.containedFunc = containedFunc;
        }
    }
    public sealed class ParamFunc<T1, T2, T3, T4, T5, TReturn> : ParamFuncBase<T1, T2, T3, T4, T5, TReturn>
    {
        readonly Func<T1, T2, T3, T4, T5, TReturn> containedFunc;
        public override TReturn Call()
            => containedFunc(parameter1, parameter2, parameter3, parameter4, parameter5);
        public ParamFunc(Func<T1, T2, T3, T4, T5, TReturn> containedFunc, T1 par1, T2 par2, T3 par3, T4 par4, T5 par5)
            : base(par1, par2, par3, par4, par5)
        {
            this.containedFunc = containedFunc;
        }
    }

    public sealed class ParamFunc<T1, T2, T3, T4, T5, T6, TReturn> : ParamFuncBase<T1, T2, T3, T4, T5, T6, TReturn>
    {
        readonly Func<T1, T2, T3, T4, T5, T6, TReturn> containedFunc;
        public override TReturn Call()
            => containedFunc(parameter1, parameter2, parameter3, parameter4, parameter5, parameter6);
        public ParamFunc(Func<T1, T2, T3, T4, T5, T6, TReturn> containedFunc, T1 par1, T2 par2, T3 par3, T4 par4, T5 par5, T6 par6)
            : base(par1, par2, par3, par4, par5, par6)
        {
            this.containedFunc = containedFunc;
        }
    }
    #endregion

    #endregion
    //TODO: BEEG FUNC

    #region Actions
    public abstract class ParamActionBase<T1> : IParamAction
    {
        protected T1 parameter1;
        public abstract void Call();
        //{
        //   return containedFunc(parameter1);
        //}

        public void Call(T1 par1)
        {
            parameter1 = par1;
            Call();
        }

        public ParamActionBase(T1 parameter)
        {
            // containedFunc = func;
            parameter1 = parameter;
        }
    }


    public abstract class ParamActionBase<T1, T2> : ParamActionBase<T1>
    {
        protected T2 parameter2;

        protected ParamActionBase(T1 par1, T2 par2)
            : base(par1)
        {
            parameter2 = par2;
        }

        public void Call(T1 par1, T2 par2)
        {
            parameter2 = par2;
            Call(par1);
        }
    }

    public abstract class ParamActionBase<T1, T2, T3> : ParamActionBase<T1, T2>
    {
        protected T3 parameter3;

        protected ParamActionBase(T1 par1, T2 par2, T3 par3)
            : base(par1, par2)
        {
            parameter3 = par3;
        }

        public void Call(T1 par1, T2 par2, T3 par3)
        {
            parameter3 = par3;
            Call(par1, par2);
        }
    }

    public abstract class ParamActionBase<T1, T2, T3, T4> : ParamActionBase<T1, T2, T3>
    {
        protected T4 parameter4;

        protected ParamActionBase(T1 par1, T2 par2, T3 par3, T4 par4)
            : base(par1, par2, par3)
        {
            parameter4 = par4;
        }

        public void Call(T1 par1, T2 par2, T3 par3, T4 par4)
        {
            parameter4 = par4;
            Call(par1, par2, par3);
        }
    }

    public abstract class ParamActionBase<T1, T2, T3, T4, T5> : ParamActionBase<T1, T2, T3, T4>
    {
        protected T5 parameter5;

        protected ParamActionBase(T1 par1, T2 par2, T3 par3, T4 par4, T5 par5)
            : base(par1, par2, par3, par4)
        {
            parameter5 = par5;
        }

        public void Call(T1 par1, T2 par2, T3 par3, T4 par4, T5 par5)
        {
            parameter5 = par5;
            Call(par1, par2, par3, par4);
        }
    }

    public abstract class ParamActionBase<T1, T2, T3, T4, T5, T6> : ParamActionBase<T1, T2, T3, T4, T5>
    {
        protected T6 parameter6;

        protected ParamActionBase(T1 par1, T2 par2, T3 par3, T4 par4, T5 par5, T6 par6)
            : base(par1, par2, par3, par4, par5)
        {
            parameter6 = par6;
        }

        public void Call(T1 par1, T2 par2, T3 par3, T4 par4, T5 par5, T6 par6)
        {
            parameter6 = par6;
            Call(par1, par2, par3, par4, par5);
        }
    }
    #region Leaves
    public sealed class ParamAction<T1> : ParamActionBase<T1>
    {
        readonly Action<T1> containedAction;
        public override void Call()
            => containedAction(parameter1);
        public ParamAction(Action<T1> containedAction, T1 par1)
            : base(par1)
        {
            this.containedAction = containedAction;
        }
    }

    public sealed class ParamAction<T1, T2> : ParamActionBase<T1, T2>
    {
        readonly Action<T1, T2> containedAction;
        public override void Call()
            => containedAction(parameter1, parameter2);
        public ParamAction(Action<T1, T2> containedAction, T1 par1, T2 par2)
            : base(par1, par2)
        {
            this.containedAction = containedAction;
        }
    }

    public sealed class ParamAction<T1, T2, T3> : ParamActionBase<T1, T2, T3>
    {
        readonly Action<T1, T2, T3> containedAction;
        public override void Call()
            => containedAction(parameter1, parameter2, parameter3);
        public ParamAction(Action<T1, T2, T3> containedAction, T1 par1, T2 par2, T3 par3)
            : base(par1, par2, par3)
        {
            this.containedAction = containedAction;
        }
    }

    public sealed class ParamAction<T1, T2, T3, T4> : ParamActionBase<T1, T2, T3, T4>
    {
        readonly Action<T1, T2, T3, T4> containedAction;
        public override void Call()
            => containedAction(parameter1, parameter2, parameter3, parameter4);
        public ParamAction(Action<T1, T2, T3, T4> containedAction, T1 par1, T2 par2, T3 par3, T4 par4)
            : base(par1, par2, par3, par4)
        {
            this.containedAction = containedAction;
        }
    }
    public sealed class ParamAction<T1, T2, T3, T4, T5> : ParamActionBase<T1, T2, T3, T4, T5>
    {
        readonly Action<T1, T2, T3, T4, T5> containedAction;
        public override void Call()
            => containedAction(parameter1, parameter2, parameter3, parameter4, parameter5);
        public ParamAction(Action<T1, T2, T3, T4, T5> containedAction, T1 par1, T2 par2, T3 par3, T4 par4, T5 par5)
            : base(par1, par2, par3, par4, par5)
        {
            this.containedAction = containedAction;
        }
    }

    public sealed class ParamAction<T1, T2, T3, T4, T5, T6> : ParamActionBase<T1, T2, T3, T4, T5, T6>
    {
        readonly Action<T1, T2, T3, T4, T5, T6> containedAction;
        public override void Call()
            => containedAction(parameter1, parameter2, parameter3, parameter4, parameter5, parameter6);
        public ParamAction(Action<T1, T2, T3, T4, T5, T6> containedAction, T1 par1, T2 par2, T3 par3, T4 par4, T5 par5, T6 par6)
            : base(par1, par2, par3, par4, par5, par6)
        {
            this.containedAction = containedAction;
        }
    }
    #endregion

    #endregion

    //public struct ParamFunc<T1, T2> : IParamFunc<T1, T2, TReturn>
    //{
    //    T1 parameter1;
    //    T2 parameter2;
    //   readonly Func<T1, T2, TReturn> containedFunc;

    //    public TReturn Call()
    //    {
    //        return containedFunc(parameter1, parameter2);
    //    }

    //    public TReturn Call(T1 par1, T2 par2)
    //    {
    //        parameter1 = par1;
    //        return Call(par1);
    //    }

    //    public TReturn Call(T1 par)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public ParamFunc(Func<T1, T2, TReturn> func, T1 par1, T2 par2)
    //    {
    //        containedFunc = func;
    //        parameter1 = par1;
    //        parameter2 = par2;
    //    }
    //}

    //public struct UniParamFunc<T1, T2, TReturn> : IParamFunc<T1, TReturn>
    //{
    //    T2 parameter2;
    //   readonly Func<T1, T2, TReturn> containedFunc;

    //    public TReturn Call(T1 par)
    //    {
    //        return containedFunc(par, parameter2);
    //    }

    //    public UniParamFunc(Func<T1, T2, TReturn> func, T2 par2)
    //    {
    //        containedFunc = func;
    //        parameter2 = par2;
    //    }
    //}


    //public struct ParamFunc<T1, T2, T3, TReturn> : IParamFunc<TReturn>
    //{
    //    T1 parameter1;
    //    T2 parameter2;
    //    T3 parameter3;
    //   readonly Func<T1, T2, T3, TReturn> containedFunc;

    //    public TReturn Call()
    //    {
    //        return containedFunc(parameter1, parameter2, parameter3);
    //    }

    //    public ParamFunc(Func<T1, T2, T3, TReturn> func, T1 par1, T2 par2, T3 par3)
    //    {
    //        containedFunc = func;
    //        parameter1 = par1;
    //        parameter2 = par2;
    //        parameter3 = par3;
    //    }
    //}



    //public struct UniParamFunc<T1, T2, T3, TReturn> : IParamFunc<T1, TReturn>
    //{
    //    T2 parameter2;
    //    T3 parameter3;
    //   readonly Func<T1, T2, T3, TReturn> containedFunc;

    //    public TReturn Call(T1 par1)
    //    {
    //        return containedFunc(par1, parameter2, parameter3);
    //    }

    //    public UniParamFunc(Func<T1, T2, T3, TReturn> func, T2 par2, T3 par3)
    //    {
    //        containedFunc = func;
    //        parameter2 = par2;
    //        parameter3 = par3;
    //    }
    //}

    //public struct SemiParamFunc<T1, T2, T3, TReturn> : IParamFunc<T1, T2, TReturn>
    //{
    //    T3 parameter3;
    //   readonly Func<T1, T2, T3, TReturn> containedFunc;

    //    public TReturn Call(T1 par1, T2 par2)
    //    {
    //        return containedFunc(par1, par2, parameter3);
    //    }

    //    public SemiParamFunc(Func<T1, T2, T3, TReturn> func, T3 par3)
    //    {
    //        containedFunc = func;
    //        parameter3 = par3;
    //    }
    //}

    //public struct ParamFunc<T1, T2, T3, T4, TReturn> : IParamFunc<TReturn>
    //{
    //    T1 parameter1;
    //    T2 parameter2;
    //    T3 parameter3;
    //    T4 parameter4;
    //   readonly Func<T1, T2, T3, T4, TReturn> containedFunc;

    //    public TReturn Call()
    //    {
    //        return containedFunc(parameter1, parameter2, parameter3, parameter4);
    //    }

    //    public ParamFunc(Func<T1, T2, T3, T4, TReturn> func, T1 par1, T2 par2, T3 par3, T4 par4)
    //    {
    //        containedFunc = func;
    //        parameter1 = par1;
    //        parameter2 = par2;
    //        parameter3 = par3;
    //        parameter4 = par4;
    //    }
    //}

    //public struct UniParamFunc<T1, T2, T3, T4, TReturn> : IParamFunc<T1, TReturn>
    //{
    //    T2 parameter2;
    //    T3 parameter3;
    //    T4 parameter4;
    //   readonly Func<T1, T2, T3, T4, TReturn> containedFunc;

    //    public TReturn Call(T1 par1)
    //    {
    //        return containedFunc(par1, parameter2, parameter3, parameter4);
    //    }

    //    public UniParamFunc(Func<T1, T2, T3, T4, TReturn> func, T2 par2, T3 par3, T4 par4)
    //    {
    //        containedFunc = func;
    //        parameter2 = par2;
    //        parameter3 = par3;
    //        parameter4 = par4;
    //    }
    //}

    //public struct SemiParamFunc<T1, T2, T3, T4, TReturn> : IParamFunc<T1, T2, TReturn>
    //{
    //    T3 parameter3;
    //    T4 parameter4;
    //   readonly Func<T1, T2, T3, T4, TReturn> containedFunc;

    //    public TReturn Call(T1 par1, T2 par2)
    //    {
    //        return containedFunc(par1, par2, parameter3, parameter4);
    //    }

    //    public SemiParamFunc(Func<T1, T2, T3, T4, TReturn> func, T3 par3, T4 par4)
    //    {
    //        containedFunc = func;
    //        parameter3 = par3;
    //        parameter4 = par4;
    //    }
    //}

    //public struct ParamFunc<T1, T2, T3, T4, T5, TReturn> : IParamFunc<TReturn>
    //{
    //    T1 parameter1;
    //    T2 parameter2;
    //    T3 parameter3;
    //    T4 parameter4;
    //    T5 parameter5;
    //   readonly Func<T1, T2, T3, T4, T5, TReturn> containedFunc;

    //    public TReturn Call()
    //    {
    //        return containedFunc(parameter1, parameter2, parameter3, parameter4, parameter5);
    //    }

    //    public ParamFunc(Func<T1, T2, T3, T4, T5, TReturn> func, T1 par1, T2 par2, T3 par3, T4 par4, T5 par5)
    //    {
    //        containedFunc = func;
    //        parameter1 = par1;
    //        parameter2 = par2;
    //        parameter3 = par3;
    //        parameter4 = par4;
    //        parameter5 = par5;
    //    }
    //}

    //public struct UniParamFunc<T1, T2, T3, T4, T5, TReturn> : IParamFunc<T1, TReturn>
    //{
    //    T2 parameter2;
    //    T3 parameter3;
    //    T4 parameter4;
    //    T5 parameter5;
    //   readonly Func<T1, T2, T3, T4, T5, TReturn> containedFunc;

    //    public TReturn Call(T1 par1)
    //    {
    //        return containedFunc(par1, parameter2, parameter3, parameter4, parameter5);
    //    }

    //    public UniParamFunc(Func<T1, T2, T3, T4, T5, TReturn> func, T2 par2, T3 par3, T4 par4, T5 par5)
    //    {
    //        containedFunc = func;
    //        parameter2 = par2;
    //        parameter3 = par3;
    //        parameter4 = par4;
    //        parameter5 = par5;
    //    }
    //}

    //public struct SemiParamFunc<T1, T2, T3, T4, T5, TReturn> : IParamFunc<T1, T2, TReturn>
    //{
    //    T3 parameter3;
    //    T4 parameter4;
    //    T5 parameter5;
    //   readonly Func<T1, T2, T3, T4, T5, TReturn> containedFunc;

    //    public TReturn Call(T1 par1, T2 par2)
    //    {
    //        return containedFunc(par1, par2, parameter3, parameter4, parameter5);
    //    }

    //    public SemiParamFunc(Func<T1, T2, T3, T4, T5, TReturn> func, T3 par3, T4 par4, T5 par5)
    //    {
    //        containedFunc = func;
    //        parameter3 = par3;
    //        parameter4 = par4;
    //        parameter5 = par5;
    //    }
    //}

    //public struct ParamFunc<T1, T2, T3, T4, T5, T6, TReturn> : IParamFunc<TReturn>
    //{
    //    T1 parameter1;
    //    T2 parameter2;
    //    T3 parameter3;
    //    T4 parameter4;
    //    T5 parameter5;
    //    T6 parameter6;
    //   readonly Func<T1, T2, T3, T4, T5, T6, TReturn> containedFunc;

    //    public TReturn Call()
    //    {
    //        return containedFunc(parameter1, parameter2, parameter3, parameter4, parameter5, parameter6);
    //    }

    //    public ParamFunc(Func<T1, T2, T3, T4, T5, T6, TReturn> func, T1 par1, T2 par2, T3 par3, T4 par4, T5 par5, T6 par6)
    //    {
    //        containedFunc = func;
    //        parameter1 = par1;
    //        parameter2 = par2;
    //        parameter3 = par3;
    //        parameter4 = par4;
    //        parameter5 = par5;
    //        parameter6 = par6;
    //    }
    //}

    //public struct UniParamFunc<T1, T2, T3, T4, T5, T6, TReturn> : IParamFunc<T1, TReturn>
    //{
    //    T2 parameter2;
    //    T3 parameter3;
    //    T4 parameter4;
    //    T5 parameter5;
    //    T6 parameter6;

    //   readonly Func<T1, T2, T3, T4, T5, T6, TReturn> containedFunc;

    //    public TReturn Call(T1 par1)
    //    {
    //        return containedFunc(par1, parameter2, parameter3, parameter4, parameter5, parameter6);
    //    }

    //    public UniParamFunc(Func<T1, T2, T3, T4, T5, T6, TReturn> func, T2 par2, T3 par3, T4 par4, T5 par5, T6 par6)
    //    {
    //        containedFunc = func;
    //        parameter2 = par2;
    //        parameter3 = par3;
    //        parameter4 = par4;
    //        parameter5 = par5;
    //        parameter6 = par6;
    //    }
    //}

    //public struct SemiParamFunc<T1, T2, T3, T4, T5, T6, TReturn> : IParamFunc<T1, T2, TReturn>
    //{
    //    T3 parameter3;
    //    T4 parameter4;
    //    T5 parameter5;
    //    T6 parameter6;

    //   readonly Func<T1, T2, T3, T4, T5, T6, TReturn> containedFunc;

    //    public TReturn Call(T1 par1, T2 par2)
    //    {
    //        return containedFunc(par1, par2, parameter3, parameter4, parameter5, parameter6);
    //    }

    //    public SemiParamFunc(Func<T1, T2, T3, T4, T5, T6, TReturn> func, T3 par3, T4 par4, T5 par5, T6 par6)
    //    {
    //        containedFunc = func;
    //        parameter3 = par3;
    //        parameter4 = par4;
    //        parameter5 = par5;
    //        parameter6 = par6;
    //    }
    //}

    //#endregion

    //#region Actions
    //public struct ParamAction<T1> : IParamAction
    //{
    //    T1 parameter1;
    //   readonly Action<T1> containedFunc;

    //    public void Call()
    //    {
    //        containedFunc(parameter1);
    //    }

    //    public ParamAction(Action<T1> func, T1 parameter)
    //    {
    //        containedFunc = func;
    //        parameter1 = parameter;
    //    }
    //}

    //public struct ParamAction<T1, T2> : IParamAction
    //{
    //    T1 parameter1;
    //    T2 parameter2;
    //   readonly Action<T1, T2> containedFunc;

    //    public void Call()
    //    {
    //        containedFunc(parameter1, parameter2);
    //    }

    //    public ParamAction(Action<T1, T2> func, T1 par1, T2 par2)
    //    {
    //        containedFunc = func;
    //        parameter1 = par1;
    //        parameter2 = par2;
    //    }
    //}

    //public struct UniParamAction<T1, T2> : IParamAction<T1>
    //{
    //    T2 parameter2;
    //   readonly Action<T1, T2> containedFunc;

    //    public void Call(T1 par1)
    //    {
    //        containedFunc(par1, parameter2);
    //    }

    //    public UniParamAction(Action<T1, T2> func, T2 par2)
    //    {
    //        containedFunc = func;
    //        parameter2 = par2;
    //    }
    //}

    //public struct ParamAction<T1, T2, T3> : IParamAction
    //{
    //    T1 parameter1;
    //    T2 parameter2;
    //    T3 parameter3;
    //   readonly Action<T1, T2, T3> containedFunc;

    //    public void Call()
    //    {
    //        containedFunc(parameter1, parameter2, parameter3);
    //    }

    //    public ParamAction(Action<T1, T2, T3> func, T1 par1, T2 par2, T3 par3)
    //    {
    //        containedFunc = func;
    //        parameter1 = par1;
    //        parameter2 = par2;
    //        parameter3 = par3;
    //    }
    //}

    //public struct UniParamAction<T1, T2, T3> : IParamAction<T1>
    //{
    //    T2 parameter2;
    //    T3 parameter3;
    //   readonly Action<T1, T2, T3> containedFunc;

    //    public void Call(T1 par1)
    //    {
    //        containedFunc(par1, parameter2, parameter3);
    //    }

    //    public UniParamAction(Action<T1, T2, T3> func, T2 par2, T3 par3)
    //    {
    //        containedFunc = func;
    //        parameter2 = par2;
    //        parameter3 = par3;
    //    }
    //}
    //public struct SemiParamAction<T1, T2, T3> : IParamAction<T1, T2>
    //{
    //    T3 parameter3;
    //   readonly Action<T1, T2, T3> containedFunc;

    //    public void Call(T1 par1, T2 par2)
    //    {
    //        containedFunc(par1, par2, parameter3);
    //    }

    //    public SemiParamAction(Action<T1, T2, T3> func, T3 par3)
    //    {
    //        containedFunc = func;
    //        parameter3 = par3;
    //    }
    //}


    //public struct ParamAction<T1, T2, T3, T4> : IParamAction
    //{
    //    T1 parameter1;
    //    T2 parameter2;
    //    T3 parameter3;
    //    T4 parameter4;
    //   readonly Action<T1, T2, T3, T4> containedFunc;

    //    public void Call()
    //    {
    //        containedFunc(parameter1, parameter2, parameter3, parameter4);
    //    }

    //    public ParamAction(Action<T1, T2, T3, T4> func, T1 par1, T2 par2, T3 par3, T4 par4)
    //    {
    //        containedFunc = func;
    //        parameter1 = par1;
    //        parameter2 = par2;
    //        parameter3 = par3;
    //        parameter4 = par4;
    //    }
    //}

    //public struct SemiParamAction<T1, T2, T3, T4> : IParamAction<T1>
    //{
    //    T2 parameter2;
    //    T3 parameter3;
    //    T4 parameter4;
    //   readonly Action<T1, T2, T3, T4> containedFunc;

    //    public void Call(T1 par1)
    //    {
    //        containedFunc(par1, parameter2, parameter3, parameter4);
    //    }

    //    public SemiParamAction(Action<T1, T2, T3, T4> func, T2 par2, T3 par3, T4 par4)
    //    {
    //        containedFunc = func;
    //        parameter2 = par2;
    //        parameter3 = par3;
    //        parameter4 = par4;
    //    }
    //}

    //public struct ParamAction<T1, T2, T3, T4, T5> : IParamAction
    //{
    //    T1 parameter1;
    //    T2 parameter2;
    //    T3 parameter3;
    //    T4 parameter4;
    //    T5 parameter5;
    //   readonly Action<T1, T2, T3, T4, T5> containedFunc;

    //    public void Call()
    //    {
    //        containedFunc(parameter1, parameter2, parameter3, parameter4, parameter5);
    //    }

    //    public ParamAction(Action<T1, T2, T3, T4, T5> func, T1 par1, T2 par2, T3 par3, T4 par4, T5 par5)
    //    {
    //        containedFunc = func;
    //        parameter1 = par1;
    //        parameter2 = par2;
    //        parameter3 = par3;
    //        parameter4 = par4;
    //        parameter5 = par5;
    //    }
    //}

    //public struct UniParamAction<T1, T2, T3, T4, T5> : IParamAction<T1>
    //{
    //    T2 parameter2;
    //    T3 parameter3;
    //    T4 parameter4;
    //    T5 parameter5;
    //   readonly Action<T1, T2, T3, T4, T5> containedFunc;

    //    public void Call(T1 par1)
    //    {
    //        containedFunc(par1, parameter2, parameter3, parameter4, parameter5);
    //    }

    //    public UniParamAction(Action<T1, T2, T3, T4, T5> func, T2 par2, T3 par3, T4 par4, T5 par5)
    //    {
    //        containedFunc = func;
    //        parameter2 = par2;
    //        parameter3 = par3;
    //        parameter4 = par4;
    //        parameter5 = par5;
    //    }
    //}

    //public struct SemiParamAction<T1, T2, T3, T4, T5> : IParamAction<T1, T2>
    //{
    //    T3 parameter3;
    //    T4 parameter4;
    //    T5 parameter5;
    //   readonly Action<T1, T2, T3, T4, T5> containedFunc;

    //    public void Call(T1 par1, T2 par2)
    //    {
    //        containedFunc(par1, par2, parameter3, parameter4, parameter5);
    //    }

    //    public SemiParamAction(Action<T1, T2, T3, T4, T5> func, T3 par3, T4 par4, T5 par5)
    //    {
    //        containedFunc = func;
    //        parameter3 = par3;
    //        parameter4 = par4;
    //        parameter5 = par5;
    //    }
    //}

    //public struct ParamAction<T1, T2, T3, T4, T5, T6> : IParamAction
    //{
    //    T1 parameter1;
    //    T2 parameter2;
    //    T3 parameter3;
    //    T4 parameter4;
    //    T5 parameter5;
    //    T6 parameter6;
    //   readonly Action<T1, T2, T3, T4, T5, T6> containedFunc;

    //    public void Call()
    //    {
    //        containedFunc(parameter1, parameter2, parameter3, parameter4, parameter5, parameter6);
    //    }

    //    public ParamAction(Action<T1, T2, T3, T4, T5, T6> func, T1 par1, T2 par2, T3 par3, T4 par4, T5 par5, T6 par6)
    //    {
    //        containedFunc = func;
    //        parameter1 = par1;
    //        parameter2 = par2;
    //        parameter3 = par3;
    //        parameter4 = par4;
    //        parameter5 = par5;
    //        parameter6 = par6;
    //    }
    //}

    //public struct SemiParamAction<T1, T2, T3, T4, T5, T6> : IParamAction<T1>
    //{
    //    T2 parameter2;
    //    T3 parameter3;
    //    T4 parameter4;
    //    T5 parameter5;
    //    T6 parameter6;
    //   readonly Action<T1, T2, T3, T4, T5, T6> containedFunc;

    //    public void Call(T1 par1)
    //    {
    //        containedFunc(par1, parameter2, parameter3, parameter4, parameter5, parameter6);
    //    }

    //    public SemiParamAction(Action<T1, T2, T3, T4, T5, T6> func, T2 par2, T3 par3, T4 par4, T5 par5, T6 par6)
    //    {
    //        containedFunc = func;
    //        parameter2 = par2;
    //        parameter3 = par3;
    //        parameter4 = par4;
    //        parameter5 = par5;
    //        parameter6 = par6;
    //    }
    //}

}
