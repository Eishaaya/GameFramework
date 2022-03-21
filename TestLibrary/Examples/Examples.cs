
//DELEGATES


//public delegate void SettingChangedDelegate<T>(Screen screen, Setting<T> setting);

//public abstract class Setting<T>
//{
//    public event Action<Screen, Setting<T>> SomethingHappened;
//   // public event SettingChangedDelegate<T> SomethingElseHappened;

//    public T Value
//    {
//        get => Value;
//        set
//        {
//            OldValue = Value;
//            Value = value;                
//        }
//    }
//    public T OldValue { get; private set; }

//    public void Revert()
//    {
//        Value = OldValue;
//    }
//}