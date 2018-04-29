using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Students_and_Courses
{
    interface IStudentCollection
    {
        bool Add(Person student, string course);
        void Print();
    }
}
