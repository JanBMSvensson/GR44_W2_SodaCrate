
using GR44_W2_SodaCrate;
using System.Net.Http.Headers;

BeverageStore Store = new();

SodaCrate? SC = null;

ConsoleMenu MainMenu = new("Main menu");
MainMenu.Add(new ConsoleMenuItem(() => SC is null ? "Select a soda crate" : $"Replace '{SC.Name}' ({SC.SodaBottleCount()} bottles)", SelectSodaCrate),
             new ConsoleMenuItem(() => "Add soda bottles", AddSodaBottle, () => SC?.IsFull ?? true), // Deactivate if no soda crate is selected or if it is full
             new ConsoleMenuItem(() => "Remove soda bottles", RemoveSodaBottle, () => SC?.IsEmpty ?? true));
             //new ConsoleMenuItem(() => "Calculate price", () => WriteLine("Bye"), () => SC is null));

ConsoleMenuItem? menuSelection = null;
do
{
    Clear();
    MainMenu.WriteMenuTitle();
    MainMenu.WriteMenu();

    menuSelection = MainMenu.Wait();

} while (menuSelection is not null);




void SelectSodaCrate()
{
    ConsoleMenu menu = new ("Soda crate selection menu");

    foreach (var item in Store.Products.Where(x => x.GetType() == typeof(SodaCrate))) // Find all products of sub type SodaCrate
        menu.Add(new ConsoleMenuItem(() => $"{item.Name.PadRight(20)}{item.Price.ToString("N2").PadLeft(10)}{item.DepositFee.ToString("N2").PadLeft(10)}", () => SC = (SodaCrate)item));

    menu.Add(new ConsoleMenuItem(() => "None", () => SC = null, () => SC is null));

    Clear();
    menu.WriteMenuTitle();
    WriteLine("".PadRight(4) + "Soda crate".PadRight(20) + "Price Kr".PadLeft(10) + "Deposit".PadLeft(10));
    menu.WriteMenu();
    menu.Wait();
}

void AddSodaBottle()
{
    if (SC is null)
        throw new Exception("3984756398475");

    ConsoleMenu menu = new($"Add soda bottles to your '{SC.Name}'");

    foreach (var item in Store.Products.Where(x => x.GetType() == typeof(SodaBottle)).OrderBy(x => x.Name)) // Find all products of sub type SodaCrate
        menu.Add(new ConsoleMenuItem(() => $"{item.Name.PadRight(20)}{item.Price.ToString("N2").PadLeft(10)}{item.DepositFee.ToString("N2").PadLeft(10)}", () => SC.AddSodaBottles(1, (SodaBottle)item), () => SC.IsFull ));

    ConsoleMenuItem? SelectedItem = null;
    do
    {
        Clear();
        menu.WriteMenuTitle();
        WriteLine("".PadRight(4) + "Soda bottle".PadRight(20) + "Price Kr".PadLeft(10) + "Deposit".PadLeft(10));
        menu.WriteMenu();
        WriteLine();

        SC.Visualize();
        SelectedItem = menu.Wait();
    } while (SelectedItem is not null);
       
}

void RemoveSodaBottle()
{
    if (SC is null)
        throw new Exception("3984756398475");

    ConsoleMenu menu = new($"Remove soda bottles from your '{SC.Name}'");

    foreach (var item in Store.Products.Where(x => x.GetType() == typeof(SodaBottle)).OrderBy(x => x.Name)) // Find all products of sub type SodaCrate
        menu.Add(new ConsoleMenuItem(() => item.Name, () => SC.RemoveSodaBottles(1, (SodaBottle)item), () => SC.SodaBottleCount((SodaBottle)item) == 0));

    ConsoleMenuItem? SelectedItem = null;
    do
    {
        Clear();
        menu.WriteMenuTitle();
        menu.WriteMenu();
        WriteLine();

        SC.Visualize();
        SelectedItem = menu.Wait();

    } while (SelectedItem is not null);

}




