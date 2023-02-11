using System;

namespace Scripts.Stats
{
    [Serializable]
    public class Stat
    {
        public event Action<int> ValueChanged; 

        private int _maxValue;
        private int _value;

        public Stat(int value, int maxValue)
        {
            _value = value;
            _maxValue = maxValue;
        }

        public int Value
        {
            get => _value;
            set
            {
                if (value > _maxValue)
                    _value = _maxValue;
                else if (value < 0)
                    _value = 0;
                
                ValueChanged?.Invoke(_value);
            }
        }
    }
}