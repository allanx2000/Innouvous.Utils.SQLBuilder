using Innouvous.Utils.SQLBuilder.Framework;
using Innouvous.Utils.SQLBuilder.Writers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innouvous.Utils.SQLBuilder.Tests
{
    /// <summary>
    /// Samples project
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            ISQLQueryWriter writer = new SimpleWriter();

            string response;

            response = SelectTest.TestComplexSelect(writer);
            Console.WriteLine(response);
            Console.WriteLine();
            
            response = SelectTest.TestCustomSelectString(writer);
            Console.WriteLine(response);
            Console.WriteLine();

            response = InsertTest.TestInsertWithKeys(writer);
            Console.WriteLine(response);
            Console.WriteLine();

            response = InsertTest.TestInsert(writer);
            Console.WriteLine(response);
            Console.WriteLine();

            response = UpdateTest.TestUpdate(writer);
            Console.WriteLine(response);
            Console.WriteLine();

            response = DeleteTest.Delete(writer);
            Console.WriteLine(response);
            Console.WriteLine();


            response = DeleteTest.DeleteAll(writer);
            Console.WriteLine(response);
            Console.WriteLine();

            Console.ReadLine();
        }

    }
}
