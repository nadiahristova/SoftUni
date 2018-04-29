namespace Workers.Entities
{
    using System;

    using Workers.Entities.Progenitor;

    class Worker : Human
    {
        private const byte WorkingDaysInAWeek = 5;

        private decimal weekSalary;
        private byte workHoursPerDay;

        public Worker(string firstName, string lastName, byte workHoursPerDay, decimal weekSalary) 
            : base(firstName, lastName)
        {
            this.WorkHoursPerDay = workHoursPerDay;
            this.WeekSalary = weekSalary;
        }

        public decimal WeekSalary
        {
            get { return this.weekSalary; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Salary cannot be negative.");

                this.weekSalary = value;
            }
        }

        public byte WorkHoursPerDay
        {
            get { return this.workHoursPerDay; }
            set
            {
                if (value > 9)
                    throw new ArgumentOutOfRangeException("Welcome to the middle ages.");

                this.workHoursPerDay = value;
            }
        }

        public decimal MoneyPerHour()
        {
            return this.WeekSalary / WorkHoursPerDay / this.WorkHoursPerDay;
        }

        public override string ToString()
        {
            return string.Format("Worker: {0}\n Money per hour paid: {1:f2} lv.", base.ToString(), this.MoneyPerHour());
        }
    }
}
