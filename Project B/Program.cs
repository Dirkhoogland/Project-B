using Project_B.DataAcces; 
using Project_B.Presentation;

namespace Project_B
{
    public class Project_B
    {
        static void Main()
        {
            DataAccess.Database();
            lay_out lay_out = new lay_out();
            lay_out.ToonMenu();
        }
    }
}