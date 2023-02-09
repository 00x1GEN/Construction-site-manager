using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace FaxProject
{
    class Program
    {
        static int Main()
        {
            List<Workers> worker = new List<Workers>();
            List<ConstructionS> constSite = new List<ConstructionS>();
            List<Expenses> exp = new List<Expenses>();
            byte select = 1, esc = 0;

            while (esc == 0)
            {
                Console.Clear();
                Console.WriteLine("Welcome to SiteManager v1.3.0\n");
                switch (select)
                {
                    case 1:
                        Console.Write(" "); Console.BackgroundColor = ConsoleColor.Blue; Console.WriteLine("Login"); Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine(" Exit");
                        break;
                    case 2:
                        Console.WriteLine(" Login");
                        Console.Write(" "); Console.BackgroundColor = ConsoleColor.Red; Console.WriteLine("Exit"); Console.BackgroundColor = ConsoleColor.Black;
                        break;
                    default:
                        break;
                }
                ConsoleKeyInfo input = Console.ReadKey();
                if (input.Key == ConsoleKey.DownArrow)
                {
                    select++;
                    if (select == 3) select = 1;
                }
                else if (input.Key == ConsoleKey.UpArrow)
                {
                    select--;
                    if (select == 0) select = 2;
                }
                else if (input.Key == ConsoleKey.Enter)
                {
                    esc = 1;
                    switch (select)
                    {
                        case 1:
                            worker = InWorkers();
                            constSite = InConstruction();
                            exp = InExpenses();
                            esc = MainMenu(Login(), worker, constSite, exp); //user: admin  pass: admin123
                            break;
                        case 2:
                            Console.Clear(); Console.WriteLine("Goodbye!");
                            return 0;
                        default:
                            break;
                    }
                }
            }
            return 0;
        }

        static string Login() /*--------------------------LOGIN SYSTEM-------------------------*/
        {
            string userIn = "", passIn = "";
            string[] u = File.ReadAllLines(@"DatB\user.txt");

            Console.Clear(); Console.WriteLine("Please login!\n\n\n");
            Console.Write("Username: "); userIn = Console.ReadLine();
            while (userIn != u[0])
            {
                Console.Clear();
                Console.WriteLine("Please login!\n\nUser does not exist!\n");
                Console.Write("Username: ");
                userIn = Console.ReadLine();
            }
            Console.Clear(); Console.WriteLine("Please login!\n\n\n");
            Console.Write("Password: "); passIn = Console.ReadLine();
            while (passIn != u[1])
            {
                Console.Clear();
                Console.WriteLine("Please login!\n\nIncorrect password!\n");
                Console.Write("Password: ");
                passIn = Console.ReadLine();
            }
            return u[0];
        }
        static List<Workers> InWorkers() /*-------------------------INPORT WORKERS-----------------------*/
        {
            List<Workers> worker = new List<Workers>();

            using (StreamReader sr = File.OpenText(@"DatB\workers.txt"))
            {
                string temp = "";
                while ((temp = sr.ReadLine()) != null)
                {
                    string[] workerFile = temp.Split(' ');

                    string name = workerFile[0];
                    string surname = workerFile[1];
                    int pay = Convert.ToInt32(workerFile[2]);
                    int id = Convert.ToInt32(workerFile[3]);

                    worker.Add(new Workers{name = name, surname = surname, pay = pay, id = id});
                }
            }
            return worker;
        }
        static List<ConstructionS> InConstruction() /*-----------------------INPORT CONSTRUCTION SITES-----------------------*/
        {
            List<ConstructionS> constSite = new List<ConstructionS>();

            using (StreamReader sr = File.OpenText(@"DatB\constSites.txt"))
            {
                string temp = "";
                while ((temp = sr.ReadLine()) != null)
                {
                    string[] constructionFile = temp.Split(' ');

                    List<int> worksHereInt = new List<int>();

                    string name = constructionFile[0];
                    int id = Convert.ToInt32(constructionFile[1]);
                    for (int i = 2; i < constructionFile.Length; i++)
                    {
                        worksHereInt.Add(Convert.ToInt32(constructionFile[i]));
                    }

                    constSite.Add(new ConstructionS { name = name, id = id, worksHere = worksHereInt });
                }
            }
            return constSite;
        }
        static List<Expenses> InExpenses() /*-----------------------INPORT EXPENSES-----------------------*/
        {
            List<Expenses> exp = new List<Expenses>();

            using (StreamReader sr = File.OpenText(@"DatB\expenses.txt"))
            {
                string temp = "";
                while ((temp = sr.ReadLine()) != null)
                {
                    string[] expenses = temp.Split(' ');

                    List<int> material = new List<int>();

                    DateTime date = DateTime.Parse(expenses[0]);
                    int id = Convert.ToInt32(expenses[1]);
                    for (int i = 2; i < expenses.Length; i++)
                    {
                        material.Add(Convert.ToInt32(expenses[i]));
                    }

                    exp.Add(new Expenses { date = date, id = id, material = material});
                }
            }
            return exp;
        }
        static byte MainMenu(string name, List<Workers> worker, List<ConstructionS> constSite, List<Expenses> exp) /*-------------------------MAIN MENU--------------------------*/
        {
            byte select = 1;
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Welcome {name}!\n");
                switch (select)
                {
                    case 1:
                        Console.Write(" "); Console.BackgroundColor = ConsoleColor.Blue; Console.WriteLine("Construction sites"); Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine(" Workers");
                        Console.WriteLine(" Change password");
                        Console.WriteLine(" Logout");
                        break;
                    case 2:
                        Console.WriteLine(" Construction sites");
                        Console.Write(" "); Console.BackgroundColor = ConsoleColor.Blue; Console.WriteLine("Workers"); Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine(" Change password");
                        Console.WriteLine(" Logout");
                        break;
                    case 3:
                        Console.WriteLine(" Construction sites");
                        Console.WriteLine(" Workers");
                        Console.Write(" "); Console.BackgroundColor = ConsoleColor.Blue; Console.WriteLine("Change password"); Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine(" Logout");
                        break;
                    case 4:
                        Console.WriteLine(" Construction sites");
                        Console.WriteLine(" Workers");
                        Console.WriteLine(" Change password");
                        Console.Write(" "); Console.BackgroundColor = ConsoleColor.Red; Console.WriteLine("Logout"); Console.BackgroundColor = ConsoleColor.Black;
                        break;
                    default:
                        break;
                }
                ConsoleKeyInfo input = Console.ReadKey();
                if (input.Key == ConsoleKey.DownArrow)
                {
                    select++;
                    if (select == 5) select = 1;
                }
                else if (input.Key == ConsoleKey.UpArrow)
                {
                    select--;
                    if (select == 0) select = 4;
                }
                else if (input.Key == ConsoleKey.Enter)
                {
                    switch (select)
                    {
                        case 1:
                            constSite = ConstSiteMenu(constSite, worker, exp);
                            break;
                        case 2:
                            worker = WorkerMenu(worker);
                            break;
                        case 3:
                            ChangePass();
                            return 0;
                        case 4:
                            return 0;
                        default:
                            break;
                    }
                }
            }
            return 1;
        }
        static List<ConstructionS> ConstSiteMenu(List<ConstructionS> constSite, List<Workers> worker, List<Expenses> exp) /*-------------------------SITE MENU---------------------------*/
        {
            List<int> filler = new List<int>();
            byte select = 1, exit = 0;
            string addCsName = "";
            int addCsId = 0;
            while (exit == 0)
            {
                Console.Clear();
                Console.WriteLine("Construction sites:\n");
                switch (select)
                {
                    case 1:
                        Console.Write(" "); Console.BackgroundColor = ConsoleColor.Blue; Console.WriteLine("Select"); Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine(" Add");
                        Console.WriteLine(" Remove");
                        Console.WriteLine(" Back");
                        break;
                    case 2:
                        Console.WriteLine(" Select");
                        Console.Write(" "); Console.BackgroundColor = ConsoleColor.Blue; Console.WriteLine("Add"); Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine(" Remove");
                        Console.WriteLine(" Back");
                        break;
                    case 3:
                        Console.WriteLine(" Select");
                        Console.WriteLine(" Add");
                        Console.Write(" "); Console.BackgroundColor = ConsoleColor.Blue; Console.WriteLine("Remove"); Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine(" Back");
                        break;
                    case 4:
                        Console.WriteLine(" Select");
                        Console.WriteLine(" Add");
                        Console.WriteLine(" Remove");
                        Console.Write(" "); Console.BackgroundColor = ConsoleColor.Red; Console.WriteLine("Back"); Console.BackgroundColor = ConsoleColor.Black;
                        break;
                    default:
                        break;
                }
                ConsoleKeyInfo input = Console.ReadKey();
                if (input.Key == ConsoleKey.DownArrow)
                {
                    select++;
                    if (select == 5) select = 1;
                }
                else if (input.Key == ConsoleKey.UpArrow)
                {
                    select--;
                    if (select == 0) select = 4;
                }
                else if (input.Key == ConsoleKey.Enter)
                {
                    switch (select)
                    {
                        case 1: /*-------------------SELECT SITE AND GO TO NEW MENU--------------------*/
                            int select2 = 1;
                            Console.Clear();
                            var csSelect = constSite.OrderBy(constSites => constSites.id);
                            foreach (ConstructionS cs in csSelect)
                            {
                                Console.WriteLine($"ID-{cs.id} {cs.name}");
                            }
                            Console.Write("\nEnter site ID you want so see: "); select2 = Convert.ToInt32(Console.ReadLine());
                            int realPos = 0;
                            foreach (ConstructionS cs in constSite) //fetching index in list since ID =/= index
                            {
                                if (cs.id == select2)
                                {
                                    break;
                                }
                                realPos++;
                            }
                            constSite = ManageSite(constSite, worker, realPos, exp);
                            break;
                        case 2: /*---------------------ADD SITE----------------------*/
                            Console.Clear(); Console.Write("Enter construction site name: "); addCsName = Console.ReadLine();
                            Console.Clear(); Console.Write("Enter construction site ID: "); addCsId = Convert.ToInt32(Console.ReadLine());
                            constSite.Add(new ConstructionS { name = addCsName, id = addCsId, worksHere = filler });
                            break;
                        case 3: /*---------------------REMOVE SITE-------------------*/
                            Console.Clear();
                            int removeCsId = 0;
                            var csId = constSite.OrderBy(constSites => constSites.id);
                            foreach (ConstructionS cs in csId)
                            {
                                Console.WriteLine($"ID-{cs.id} {cs.name}");
                            }
                            Console.Write("\nEnther the construction site ID you wish to remove: ");
                            removeCsId = Convert.ToInt32(Console.ReadLine());
                            for (int i = 0; i < constSite.Count; i++)
                            {
                                if (constSite[i].id == removeCsId)
                                {
                                    constSite.RemoveAt(i);
                                    break;
                                }
                            }
                            break;
                        case 4: /*---------------------BACK--------------------*/
                            exit = 1;
                            break;
                        default:
                            break;
                    }
                }
            }
            using (StreamWriter sw = File.CreateText(@"DatB\constSites.txt")) /*-------------------UPDATE CONSTRUCTION FILE-----------------*/
            {
                foreach (ConstructionS cs in constSite)
                {
                    sw.Write($"{cs.name} {cs.id}");
                    for (int i = 0; i < cs.worksHere.Count; i++)
                    {
                        foreach (Workers w in worker) 
                        {
                            if (w.id == cs.worksHere[i])
                            {
                                sw.Write($" {cs.worksHere[i]}");
                            }
                        }
                    }
                    sw.WriteLine();
                }
            }
            return constSite;
        }
        static List<ConstructionS> ManageSite(List<ConstructionS> constSite, List<Workers> worker, int selectedCs, List<Expenses> exp) /*---------------------SITE MANAGER--------------------*/
        {
            byte select = 1, exit = 0;
            decimal[] matCost = new decimal[3] {0.6m, 200, 5.5m}; //Set prices of materials example, not supposed to reflect real prices
            while (exit == 0)
            {
                Console.Clear();
                Console.WriteLine($"{constSite[selectedCs].name}:\n");
                switch (select)
                {
                    case 1:
                        Console.Write(" "); Console.BackgroundColor = ConsoleColor.Blue; Console.WriteLine("List workers"); Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine(" Add worker");
                        Console.WriteLine(" Remove worker");
                        Console.WriteLine(" List material expenses");
                        Console.WriteLine(" Add material expense");
                        Console.WriteLine(" Print expense report");
                        Console.WriteLine(" Back");
                        break;
                    case 2:
                        Console.WriteLine(" List workers");
                        Console.Write(" "); Console.BackgroundColor = ConsoleColor.Blue; Console.WriteLine("Add worker"); Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine(" Remove worker");
                        Console.WriteLine(" List material expenses");
                        Console.WriteLine(" Add material expense");
                        Console.WriteLine(" Print expense report");
                        Console.WriteLine(" Back");
                        break;
                    case 3:
                        Console.WriteLine(" List workers");
                        Console.WriteLine(" Add worker");
                        Console.Write(" "); Console.BackgroundColor = ConsoleColor.Blue; Console.WriteLine("Remove worker"); Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine(" List material expenses");
                        Console.WriteLine(" Add material expense");
                        Console.WriteLine(" Print expense report");
                        Console.WriteLine(" Back");
                        break;
                    case 4:
                        Console.WriteLine(" List workers");
                        Console.WriteLine(" Add worker");
                        Console.WriteLine(" Remove worker");
                        Console.Write(" "); Console.BackgroundColor = ConsoleColor.Blue; Console.WriteLine("List material expenses"); Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine(" Add material expense");
                        Console.WriteLine(" Print expense report");
                        Console.WriteLine(" Back");
                        break;
                    case 5:
                        Console.WriteLine(" List workers");
                        Console.WriteLine(" Add worker");
                        Console.WriteLine(" Remove worker");
                        Console.WriteLine(" List material expenses");
                        Console.Write(" "); Console.BackgroundColor = ConsoleColor.Blue; Console.WriteLine("Add material expense"); Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine(" Print expense report");
                        Console.WriteLine(" Back");
                        break;
                    case 6:
                        Console.WriteLine(" List workers");
                        Console.WriteLine(" Add worker");
                        Console.WriteLine(" Remove worker");
                        Console.WriteLine(" List material expenses");
                        Console.WriteLine(" Add material expense");
                        Console.Write(" "); Console.BackgroundColor = ConsoleColor.Blue; Console.WriteLine("Print expense report"); Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine(" Back");
                        break;
                    case 7:
                        Console.WriteLine(" List workers");
                        Console.WriteLine(" Add worker");
                        Console.WriteLine(" Remove worker");
                        Console.WriteLine(" List material expenses");
                        Console.WriteLine(" Add material expense");
                        Console.WriteLine(" Print expense report");
                        Console.Write(" "); Console.BackgroundColor = ConsoleColor.Red; Console.WriteLine("Back"); Console.BackgroundColor = ConsoleColor.Black;
                        break;
                    default:
                        break;
                }
                ConsoleKeyInfo input = Console.ReadKey();
                if (input.Key == ConsoleKey.DownArrow)
                {
                    select++;
                    if (select == 8) select = 1;
                }
                else if (input.Key == ConsoleKey.UpArrow)
                {
                    select--;
                    if (select == 0) select = 7;
                }
                else if (input.Key == ConsoleKey.Enter)
                {
                    switch (select)
                    {
                        case 1: /*-------------------LIST WORKERS ON SITE--------------------*/
                            Console.Clear(); Console.WriteLine("Workers:\n");
                            for (int i = 0; i < constSite[selectedCs].worksHere.Count; i++)
                            {
                                foreach (Workers w in worker)
                                {
                                    if (constSite[selectedCs].worksHere[i] == w.id)
                                    {
                                        Console.WriteLine($"ID-{w.id} {w.name} {w.surname}");
                                    }
                                }
                            }
                            Console.ReadKey();
                            break;
                        case 2: /*---------------------ADD WORKERS TO SITE----------------------*/
                            Console.Clear();
                            var sortWorkerId = worker.OrderBy(worker => worker.id);
                            foreach (Workers w in sortWorkerId)
                            {
                                Console.WriteLine($"ID-{w.id} {w.name} {w.surname}");
                            }
                            Console.Write("\nEnter worker ID to assign to this site: ");
                            int addId = Convert.ToInt32(Console.ReadLine());
                            constSite[selectedCs].worksHere.Add(addId);
                            break;
                        case 3: /*---------------------REMOVE WORKERS FROM SITE-------------------*/
                            Console.Clear(); Console.WriteLine("Workers:\n");
                            for (int i = 0; i < constSite[selectedCs].worksHere.Count; i++)
                            {
                                foreach (Workers w in worker)
                                {
                                    if (constSite[selectedCs].worksHere[i] == w.id)
                                    {
                                        Console.WriteLine($"ID-{w.id} {w.name} {w.surname}");
                                    }
                                }
                            }
                            Console.Write("Enter worker ID to remove from site: ");
                            int removeId = Convert.ToInt32(Console.ReadLine());
                            for (int i = 0; i < constSite[selectedCs].worksHere.Count; i++)
                            {
                                if (constSite[selectedCs].worksHere[i] == removeId)
                                {
                                    constSite[selectedCs].worksHere.RemoveAt(i);
                                    break;
                                }
                            }
                            break;
                        case 4: /*-----------------------LIST MAT PRICE---------------------*/
                            Console.Clear(); Console.WriteLine("Current epxenses by date:\n");
                            foreach (Expenses e in exp)
                            {
                                if (constSite[selectedCs].id == e.id)
                                {
                                    Console.WriteLine("{0} - {1}$ {2}$ {3}$",e.date.ToString("dd/MM/yyyy"), (e.material[0] * matCost[0]), (e.material[1] * matCost[1]), (e.material[2] * matCost[2]));
                                }
                            }
                            Console.ReadKey();
                            break;
                        case 5: /*---------------------ADD MAT-------------------*/
                            List<int> newAddMat = new List<int>();

                            Console.Clear(); Console.Write("Enter date (dd/mm/yyyy): ");
                            DateTime date = DateTime.Parse(Console.ReadLine());
                            Console.Clear(); Console.Write("Enter number of bricks used: ");
                            newAddMat.Add(Convert.ToInt32(Console.ReadLine()));
                            Console.Clear(); Console.Write("Enter m3 of concrete used: ");
                            newAddMat.Add(Convert.ToInt32(Console.ReadLine()));
                            Console.Clear(); Console.Write("Enter m of bars used: ");
                            newAddMat.Add(Convert.ToInt32(Console.ReadLine()));

                            exp.Add(new Expenses { date = date, id = (constSite[selectedCs].id), material = newAddMat });
                            exp = exp.OrderBy(a => a.id).ThenBy(a => a.date).ToList();
                            break;
                        case 6: /*---------------------PRINT REPORT-------------------*/
                            int brojac = 0, j = 0;
                            decimal totPrice = 0;
                            Console.Clear(); Console.Write("Enter start date (dd/mm/yyy): "); DateTime date1 = DateTime.Parse(Console.ReadLine());
                            Console.Clear(); Console.Write("Enter end date (dd/mm/yyy): "); DateTime date2 = DateTime.Parse(Console.ReadLine());
                            string name = @"Reports\" + constSite[selectedCs].name + "-" + date1.Day + date1.Month + date1.Year + "-" + date2.Day + date2.Month + date2.Year + @".txt";
                            while (!(date1 == exp[brojac].date && constSite[selectedCs].id == exp[brojac].id))
                            { brojac++; }
                            do
                            {
                                j = 0;
                                totPrice += (exp[brojac].material[j] * matCost[0]); j++;
                                totPrice += (exp[brojac].material[j] * matCost[1]); j++;
                                totPrice += (exp[brojac].material[j] * matCost[2]);
                                brojac++;
                            } while (exp[brojac-1].date != date2);
                            if (!File.Exists(name))
                            {
                                using (StreamWriter sw = File.CreateText(name))
                                {
                                    sw.Write("{0} $",totPrice);
                                }

                                Console.Clear(); Console.WriteLine("File saved at {0}!", name);
                            }
                            else
                            {
                                Console.Clear(); Console.WriteLine("File already exists!");
                            } 
                            Console.ReadKey();
                            break;
                        case 7: /*---------------------BACK--------------------*/
                            exit = 1;
                            break;
                        default:
                            break;
                    }
                }
            }
            using (StreamWriter sw = File.CreateText(@"DatB\expenses.txt")) /*------------------UPDATE EXPENSES FILE-----------------*/
            {
                foreach (Expenses e in exp)
                {
                    sw.Write($"{e.date:dd/MM/yyyy} {e.id}");
                    for (int i = 0; i < e.material.Count; i++)
                    {
                        sw.Write($" {e.material[i]}");
                    }
                    sw.WriteLine();
                }
            }
            return constSite;
        }
        static List<Workers> WorkerMenu(List<Workers> worker) /*---------------------------WORKER MENU-----------------------*/
        {
            byte select = 1, exit = 0;
            string addWName = "", addWSurname = "";
            int addWId = 0, addWPay = 0;
            while (exit == 0)
            {
                Console.Clear();
                Console.WriteLine("Workers:\n");
                switch (select)
                {
                    case 1:
                        Console.Write(" "); Console.BackgroundColor = ConsoleColor.Blue; Console.WriteLine("List"); Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine(" Add");
                        Console.WriteLine(" Remove");
                        Console.WriteLine(" Back");
                        break;
                    case 2:
                        Console.WriteLine(" List");
                        Console.Write(" "); Console.BackgroundColor = ConsoleColor.Blue; Console.WriteLine("Add"); Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine(" Remove");
                        Console.WriteLine(" Back");
                        break;
                    case 3:
                        Console.WriteLine(" List");
                        Console.WriteLine(" Add");
                        Console.Write(" "); Console.BackgroundColor = ConsoleColor.Blue; Console.WriteLine("Remove"); Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine(" Back");
                        break;
                    case 4:
                        Console.WriteLine(" List");
                        Console.WriteLine(" Add");
                        Console.WriteLine(" Remove");
                        Console.Write(" "); Console.BackgroundColor = ConsoleColor.Red; Console.WriteLine("Back"); Console.BackgroundColor = ConsoleColor.Black;
                        break;
                    default:
                        break;
                }
                ConsoleKeyInfo input = Console.ReadKey();
                if (input.Key == ConsoleKey.DownArrow)
                {
                    select++;
                    if (select == 5) select = 1;
                }
                else if (input.Key == ConsoleKey.UpArrow)
                {
                    select--;
                    if (select == 0) select = 4;
                }
                else if (input.Key == ConsoleKey.Enter)
                {
                    switch (select)
                    {
                        case 1: /*---------------------DISPLAY SORTED----------------------*/
                            byte select2 = 1;
                            while (true)
                            {
                                Console.Clear();
                                Console.WriteLine("Display by:\n");
                                switch (select2)
                                {
                                    case 1:
                                        Console.Write(" "); Console.BackgroundColor = ConsoleColor.Blue; Console.WriteLine("ID"); Console.BackgroundColor = ConsoleColor.Black;
                                        Console.WriteLine(" Name");
                                        Console.WriteLine(" Pay");
                                        break;
                                    case 2:
                                        Console.WriteLine(" ID");
                                        Console.Write(" "); Console.BackgroundColor = ConsoleColor.Blue; Console.WriteLine("Name"); Console.BackgroundColor = ConsoleColor.Black;
                                        Console.WriteLine(" Pay");
                                        break;
                                    case 3:
                                        Console.WriteLine(" ID");
                                        Console.WriteLine(" Name");
                                        Console.Write(" "); Console.BackgroundColor = ConsoleColor.Blue; Console.WriteLine("Pay"); Console.BackgroundColor = ConsoleColor.Black;
                                        break;
                                    default:
                                        break;
                                }
                                ConsoleKeyInfo input2 = Console.ReadKey();
                                if (input2.Key == ConsoleKey.DownArrow)
                                {
                                    select2++;
                                    if (select2 == 4) select2 = 1;
                                }
                                else if (input2.Key == ConsoleKey.UpArrow)
                                {
                                    select2--;
                                    if (select2 == 0) select2 = 3;
                                }
                                else if (input2.Key == ConsoleKey.Enter) /*-------------SORTING BY USER SELECTION--------------*/
                                {
                                    Console.Clear();
                                    if (select2 == 1)
                                    {
                                        var sortWorkerId = worker.OrderBy(worker => worker.id);
                                        foreach (Workers w in sortWorkerId)
                                        {
                                            Console.WriteLine($"ID-{w.id} {w.name} {w.surname} {w.pay} $");
                                        }
                                        Console.ReadKey(); break;
                                    }
                                    else if (select2 == 2)
                                    {
                                        var sortWorkerName = worker.OrderBy(worker => worker.name);
                                        foreach (Workers w in sortWorkerName)
                                        {
                                            Console.WriteLine($"ID-{w.id} {w.name} {w.surname} {w.pay} $");
                                        }
                                        Console.ReadKey(); break;
                                    }
                                    else
                                    {
                                        var sortWorkerPay = worker.OrderBy(worker => worker.pay);
                                        foreach (Workers w in sortWorkerPay)
                                        {
                                            Console.WriteLine($"ID-{w.id} {w.name} {w.surname} {w.pay} $");
                                        }
                                        Console.ReadKey(); break;
                                    }
                                }
                            }
                            break;
                        case 2: /*---------------------ADD WORKER----------------------*/
                            byte provWrkAdd1 = 1, provWrkAdd2 = 1;
                            while (provWrkAdd1 == 1)
                            {
                                provWrkAdd2 = 1; //U slucaju da ponavljamo unos
                                Console.Clear(); Console.Write("Enter worker name: "); addWName = Console.ReadLine();
                                Console.Clear(); Console.Write("Enter worker surname: "); addWSurname = Console.ReadLine();
                                Console.Clear(); Console.Write("Enter worker ID: "); addWId = Convert.ToInt32(Console.ReadLine());
                                Console.Clear(); Console.Write("Enter worker pay: "); addWPay = Convert.ToInt32(Console.ReadLine());
                                while (provWrkAdd2 == 1)
                                {
                                    Console.Clear(); Console.WriteLine($"ID-{addWId} {addWName} {addWSurname} {addWPay} $"); Console.WriteLine("\nPress ENTER to confirm or BACKSPACE to try again...");
                                    ConsoleKeyInfo provWrkKey = Console.ReadKey();
                                    if (provWrkKey.Key == ConsoleKey.Enter)
                                    {
                                        provWrkAdd1 = 0; provWrkAdd2 = 0;
                                    }
                                    else if (provWrkKey.Key == ConsoleKey.Backspace)
                                    {
                                        provWrkAdd2 = 0;
                                    }
                                }
                            }
                            worker.Add(new Workers { name = addWName, surname = addWSurname, pay = addWPay, id = addWId });
                            break;
                        case 3: /*---------------------REMOVE WORKER----------------------*/
                            Console.Clear();
                            int removeWId = 0;
                            var wrkId = worker.OrderBy(worker => worker.id);
                            foreach (Workers w in wrkId)
                            {
                                Console.WriteLine($"ID-{w.id} {w.name} {w.surname}");
                            }
                            Console.Write("\nEnther the workers ID you wish to remove: ");
                            removeWId = Convert.ToInt32(Console.ReadLine());
                            for (int i = 0; i < worker.Count; i++)
                            {
                                if (worker[i].id == removeWId)
                                {
                                    worker.RemoveAt(i);
                                    break;
                                }
                            }
                            break;
                        case 4: /*---------------------BACK----------------------*/
                            exit = 1;
                            break;
                        default:
                            break;
                    }
                }
            }
            using (StreamWriter sw = File.CreateText(@"DatB\workers.txt")) /*------------------UPDATE WORKER FILE-----------------*/
            {
                var sortWorkerId = worker.OrderBy(worker => worker.id);
                foreach (Workers w in sortWorkerId)
                {
                    sw.WriteLine($"{w.name} {w.surname} {w.pay} {w.id}");
                }
            }
            return worker;
        }
        static void ChangePass() /*-------------------------PASSWORD CHANGE--------------------------*/
        {
            string[] u = File.ReadAllLines(@"DatB\user.txt");
            string[] uNew = new string[u.Length]; string temp = "";
            uNew[0] = u[0];
            while (temp != uNew[1])
            {
                Console.Clear(); Console.Write("Enter new password: ");
                temp = Console.ReadLine();
                Console.Clear(); Console.Write("Confirm new password: ");
                uNew[1] = Console.ReadLine();

                if (temp != uNew[1]) 
                {
                    Console.Clear(); Console.WriteLine("Error!"); Console.ReadKey();
                }
            }
            using (StreamWriter sw = File.CreateText(@"DatB\user.txt")) 
            {
                sw.WriteLine(uNew[0]); sw.WriteLine(uNew[1]);
            }
            Console.Clear(); Console.WriteLine("Password changed successfully!\n\nYou will be logged out..."); Console.ReadKey();
        }
        struct Workers
        {
            public string name;
            public string surname;
            public int pay;
            public int id;
        }
        struct ConstructionS
        {
            public string name;
            public int id;
            public List<int> worksHere;
        }
        struct Expenses
        {
            public DateTime date;
            public int id;
            public List<int> material;
        }
    }
}