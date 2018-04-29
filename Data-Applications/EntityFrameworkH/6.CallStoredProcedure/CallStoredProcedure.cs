namespace CallStoredProcedure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using EF_Homework;
    public class CallStoredProcedure
    {
        static void Main(string[] args)
        {
            var db = new SoftUniEntities();

            db.GetProjectsOfEmployee("Rob", "Walters");
        }
    }
}
