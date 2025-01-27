namespace Projekt
{

    public class Upgr : ObservableObject
    {
        public string Name { get; set; }
        private double _count;
        private int _boughtCount;
        protected Account _account;
        private double _cost;
        public double InitialCost;
        protected double _costMultiplier;
        public double Count
        {
            get { return _count; }
            set
            {
                _count = value;
                OnPropertyChanged();
            }
        }
        public double Cost
        {
            get { return _cost; }
            set
            {
                _cost = value;
                OnPropertyChanged();
            }
        }
        public int BoughtCount
        {
            get { return _boughtCount; }
            set
            {
                _boughtCount = value;
                OnPropertyChanged();
            }
        }
        public virtual bool Buy()
        {
            if (_account.ClickMoney < Cost)
            {
                return false;
            }
            _account.ClickMoney -= Cost;
            Cost *= _costMultiplier;
            Count++;
            BoughtCount++;
            return true;
        }
        public void ResetPerformed()
        {
            Count = 0;
            BoughtCount = 0;
            Cost = InitialCost;
        }

        public Upgr(string name, Account account, double cost, double costMultiplier)
        {
            Name = name;
            _account = account;
            InitialCost = cost;
            Cost = cost;
            _costMultiplier = costMultiplier;
            Count = 0;
        }

    }

    public class ClickUpgrade : Upgr, IAddOnClick
    {
        public IAddOnClick child;
        public override bool Buy()
        {
            if (_account.TickMoney < Cost)
            {
                return false;
            }
            _account.TickMoney -= Cost;
            Cost *= _costMultiplier;
            Count++;
            BoughtCount++;
            return true;
        }

        public void AddOnClick(double value)
        {
            Count += value;
        }
        public void ClickPerformed()
        {
            child?.AddOnClick(Count);
        }
        public ClickUpgrade(string name, Account account, double cost, double costMultiplier) : base(name, account, cost, costMultiplier)
        {
        }
    }
    public class TickUpgrade : Upgr, IAddOnTick
    {
        public IAddOnTick child;
        public override bool Buy()
        {
            if (_account.ClickMoney < Cost)
            {
                return false;
            }
            _account.ClickMoney -= Cost;
            Cost *= _costMultiplier;
            Count++;
            BoughtCount++;
            return true;
        }
        public void AddOnTick(double value)
        {
            Count += value;
        }
        public void TickPerformed()
        {
            child?.AddOnTick(Count);
        }
        public TickUpgrade(string name, Account account, double cost, double costMultiplier) : base(name, account, cost, costMultiplier)
        {
        }
    }
}
