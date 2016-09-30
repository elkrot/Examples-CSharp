using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Patterns
{
    class Program
    {

        static void Main(string[] args)
        {
            /* Creational Patterns - Порождающие паттерны */

            #region Порождающие паттерны
             /* Factory Method */
         Developer dev = new PanelDeveloper("ООО КирпичСтрой");
        House house2 = dev.Create();
         
        dev = new WoodDeveloper("Частный застройщик");
        House house = dev.Create();


/* Abstract Factory */
        Hero elf = new Hero(new ElfFactory());
        elf.Hit();
        elf.Run();

        Hero voin = new Hero(new VoinFactory());
        voin.Hit();
        voin.Run();
 /* Singleton */

        Computer comp = new Computer();
        comp.Launch("Windows 8.1");
        Console.WriteLine(comp.OS.Name);

        // у нас не получится изменить ОС, так как объект уже создан    
        comp.OS = OS.getInstance("Windows 10");
        Console.WriteLine(comp.OS.Name);
/* Prototype */
        Circle figure = new Circle(30, 50, 60);
        // применяем глубокое копирование
        Circle clonedFigure = figure.DeepCopy() as Circle;
        figure.Point.X = 100;
        figure.GetInfo();
        clonedFigure.GetInfo();
/* Builder */
        // содаем объект пекаря
        Baker baker = new Baker();
        // создаем билдер для ржаного хлеба
        BreadBuilder builder = new RyeBreadBuilder();
        // выпекаем
        baker.Bake(builder);
        Bread ryeBread = builder.Bread;
        Console.WriteLine(ryeBread.ToString());
        // оздаем билдер для пшеничного хлеба
        builder = new WheatBreadBuilder();
        baker.Bake(builder);
        Bread wheatBread = builder.Bread;
        Console.WriteLine(wheatBread.ToString());
            #endregion

            /* Behavior patterns - Паттерны поведения */
            #region Strategy
        Car auto = new Car(4, "Volvo", new PetrolMove());
            auto.Move();
            auto.Movable = new ElectricMove();
            auto.Move();        
        #endregion

            #region Observer
        Stock stock = new Stock();
        Bank bank = new Bank("ЮнитБанк", stock);
        Broker broker = new Broker("Иван Иваныч", stock);
        // имитация торгов
        stock.Market();
        // брокер прекращает наблюдать за торгами
        broker.StopTrade();
        // имитация торгов
        stock.Market();
            #endregion

            #region Command 
        Pult pult = new Pult();
        TV tv = new TV();
        pult.SetCommand(new TVOnCommand(tv));
        pult.PressButton();
        pult.PressUndo();

        Microwave microwave = new Microwave();
        // 5000 - время нагрева пищи
        pult.SetCommand(new MicrowaveCommand(microwave, 5000));
        pult.PressButton();
#endregion

            #region Command2
               Patterns.Command2.TV tv2 = new Patterns.Command2.TV();
        Patterns.Command2.Volume volume = new Patterns.Command2.Volume();
        Patterns.Command2.MultiPult mPult = new Patterns.Command2.MultiPult();
        mPult.SetCommand(0, new Patterns.Command2.TVOnCommand(tv2));
        mPult.SetCommand(1, new Patterns.Command2.VolumeCommand(volume));
        // включаем телевизор
        mPult.PressButton(0);
        // увеличиваем громкость
        mPult.PressButton(1);
        mPult.PressButton(1);
        mPult.PressButton(1);
        // действия отмены
        mPult.PressUndoButton();
        mPult.PressUndoButton();
        mPult.PressUndoButton();
        mPult.PressUndoButton();
        #endregion

            #region Template Method

        School school = new School();
        University university = new University();
        school.Learn();
        university.Learn();

        #endregion

            #region Iterator
        Library library = new Library();
        Reader reader = new Reader();
        reader.SeeBooks(library);
        #endregion

            #region State
        Water water = new Water(new LiquidWaterState());
        water.Heat();
        water.Frost();
        water.Frost();
        #endregion

            #region ChainOfResponsibility
        Receiver receiver = new Receiver(false, true, true);
         
        PaymentHandler bankPaymentHandler = new BankPaymentHandler();
        PaymentHandler moneyPaymentHnadler = new MoneyPaymentHandler();
        PaymentHandler paypalPaymentHandler = new PayPalPaymentHandler();
        bankPaymentHandler.Successor = paypalPaymentHandler;
        paypalPaymentHandler.Successor = moneyPaymentHnadler;
 
        bankPaymentHandler.Handle(receiver);
        #endregion

            #region Interpreter
        Context context = new Context();
        // определяем набор переменных
        int x = 5;
        int y = 8;
        int z = 2;
         
        // добавляем переменные в контекст
        context.SetVariable("x", x);
        context.SetVariable("y", y);
        context.SetVariable("z", z);
        // создаем объект для вычисления выражения x + y - z
        IExpression expression = new SubtractExpression(
            new AddExpression(
                new NumberExpression("x"), new NumberExpression("y")
            ),
            new NumberExpression("z")
        );
 
        int result = expression.Interpret(context);
        Console.WriteLine("результат: {0}", result);
        #endregion

            #region Mediator
        ManagerMediator mediator = new ManagerMediator();
        Colleague customer = new CustomerColleague(mediator);
        Colleague programmer = new ProgrammerColleague(mediator);
        Colleague tester = new TesterColleague(mediator);
        mediator.Customer = customer;
        mediator.Programmer = programmer;
        mediator.Tester = tester;
        customer.Send("Есть заказ, надо сделать программу");
        programmer.Send("Программа готова, надо протестировать");
        tester.Send("Программа протестирована и готова к продаже");
        #endregion

            #region Memento
        Herox hero = new Herox();
        hero.Shout(); // делаем выстрел, осталось 9 патронов
        GameHistory game = new GameHistory();
             
        game.History.Push(hero.SaveState()); // сохраняем игру
 
        hero.Shout(); //делаем выстрел, осталось 8 патронов
 
        hero.RestoreState(game.History.Pop());
 
        hero.Shout(); //делаем выстрел, осталось 8 патронов
 

        #endregion

            #region Visitor
        var structure = new Bankx();
        structure.Add(new Person { Name = "Иван Алексеев", Number = "82184931" });
        structure.Add(new Company {Name="Microsoft", RegNumber="ewuir32141324", Number="3424131445"});
        structure.Accept(new HtmlVisitor());
        structure.Accept(new XmlVisitor());
        #endregion

        /*Структурные паттерны*/

        #region Decorator
        Pizza pizza1 = new ItalianPizza();
        pizza1 = new TomatoPizza(pizza1); // итальянская пицца с томатами
        Console.WriteLine("Название: {0}", pizza1.Name);
        Console.WriteLine("Цена: {0}", pizza1.GetCost());
 
        Pizza pizza2 = new ItalianPizza();
        pizza2 = new CheesePizza(pizza2);// итальянская пиццы с сыром
        Console.WriteLine("Название: {0}", pizza2.Name);
        Console.WriteLine("Цена: {0}", pizza2.GetCost());
 
        Pizza pizza3 = new BulgerianPizza();
        pizza3 = new TomatoPizza(pizza3);
        pizza3 = new CheesePizza(pizza3);// болгарская пиццы с томатами и сыром
        Console.WriteLine("Название: {0}", pizza3.Name);
        Console.WriteLine("Цена: {0}", pizza3.GetCost());
        #endregion

        #region Adapter
         // путешественник
        Patterns.Adapter.Driver driver = new Patterns.Adapter.Driver();
        // машина
        Patterns.Adapter.Auto autoz = new Patterns.Adapter.Auto();
        // отправляемся в путешествие
        driver.Travel(autoz);
        // встретились пески, надо использовать верблюда
        Patterns.Adapter.Camel camel = new Patterns.Adapter.Camel();
        // используем адаптер
        Patterns.Adapter.ITransport camelTransport = new Patterns.Adapter.CamelToTransportAdapter(camel);
        // продолжаем путь по пескам пустыни
        driver.Travel(camelTransport);
        #endregion

        #region Facade
        TextEditor textEditor = new TextEditor();
        Compiller compiller = new Compiller();
        CLR clr = new CLR();
         
        VisualStudioFacade ide = new VisualStudioFacade(textEditor, compiller, clr);
         
        Programmer programmerz = new Programmer();
        programmerz.CreateApplication(ide);
        #endregion

        #region Composite
         Component fileSystem = new Directory("Файловая система");
        // определяем новый диск
        Component diskC = new Directory("Диск С");
        // новые файлы
        Component pngFile = new File("12345.png");
        Component docxFile = new File("Document.docx");
        // добавляем файлы на диск С
        diskC.Add(pngFile);
        diskC.Add(docxFile);
        // добавляем диск С в файловую систему
        fileSystem.Add(diskC);
        // выводим все данные
        fileSystem.Print();
        Console.WriteLine();
        // удаляем с диска С файл
        diskC.Remove(pngFile);
        // создаем новую папку
        Component docsFolder = new Directory("Мои Документы");
        // добавляем в нее файлы
        Component txtFile = new File("readme.txt");
        Component csFile = new File("Program.cs");
        docsFolder.Add(txtFile);
        docsFolder.Add(csFile);
        diskC.Add(docsFolder);
         
        fileSystem.Print();
        #endregion

        #region Proxy
        /*  using(IBook book = new BookStoreProxy())
        {
            // читаем первую страницу
            Page page1 = book.GetPage(1);
            Console.WriteLine(page1.Text);
            // читаем вторую страницу
            Page page2 = book.GetPage(2);
            Console.WriteLine(page2.Text);
            // возвращаемся на первую страницу    
            page1 = book.GetPage(1);
            Console.WriteLine(page1.Text);
        }*/
        #endregion

        #region Bridge
        Programmerx freelancer = new FreelanceProgrammer(new CPPLanguage());
        freelancer.DoWork();
        freelancer.EarnMoney();
        // пришел новый заказ, но теперь нужен c#
        freelancer.Language = new CSharpLanguage();
        freelancer.DoWork();
        freelancer.EarnMoney();
        #endregion

        #region Flyweight
           double longitude = 37.61;
        double latitude = 55.74;
 
        HouseFactory houseFactory = new HouseFactory();
        for (int i = 0; i < 5;i++)
        {
            House panelHouse = houseFactory.GetHouse("Panel");
            if (panelHouse != null)
                panelHouse.Build(longitude, latitude);
            longitude += 0.1;
            latitude += 0.1;
        }
 
        for (int i = 0; i < 5; i++)
        {
            House brickHouse = houseFactory.GetHouse("Brick");
            if (brickHouse != null)
                brickHouse.Build(longitude, latitude);
            longitude += 0.1;
            latitude += 0.1;
        }
 
        #endregion


        Console.ReadLine();
        }
    }


}
