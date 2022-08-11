using LuccaDevises.Exception;
using LuccaDevises.Ressource;
using LuccaDevises.Service;

try
{
    if (args.Length != 1)
        throw new CommandLineException(Constant.CommandLineNumberError);

    new FileService().Exists(args[0]);

    var graph = new GraphService(args[0]);
    Console.WriteLine(graph.CalculResult());
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
