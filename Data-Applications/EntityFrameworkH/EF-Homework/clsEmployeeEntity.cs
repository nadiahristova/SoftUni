using System;
using System.Collections.Generic;
using System.Linq;

using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;

namespace EF_Homework
{
    [Table(Name = "Employee")]
    public class clsEmployeeEntity
    {       
        private string _ProjectName;
        private string _PDescription;
        private DateTime _StartDate;

        [Column(Name = "Name", DbType = "nvarchar(50)")]
        public string Name
        {
            set
            {
                _ProjectName = value;
            }
            get
            {
                return _ProjectName;
            }
        }

        [Column(Name = "Description", DbType = "ntext")]
        public string Description
        {
            set
            {
                _PDescription = value;
            }
            get
            {
                return _PDescription;
            }
        }

        [Column(DbType = "smalldatetime")]
        public DateTime StartDate
        {
            set
            {
                _StartDate = value;
            }
            get
            {
                return _StartDate;
            }
        }
       
    }
}
