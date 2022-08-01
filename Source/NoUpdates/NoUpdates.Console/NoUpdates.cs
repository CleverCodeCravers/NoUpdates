using System.ServiceProcess;


Console.WriteLine("---------------------------------------------------------------------");
Console.WriteLine("");
Console.WriteLine("----------------------------NoUpdates--------------------------------");
Console.WriteLine("");
Console.WriteLine("  A tool to regularly check if Win Updates are running and stop them ");
Console.WriteLine("");
Console.WriteLine("---------------------------------------------------------------------");
Console.WriteLine("");

Console.Write("Please specify a timeout in minutes (default is 10 Minutes): ");
var userTimeout = Console.ReadLine();
Console.WriteLine("");

if (userTimeout == "") Console.WriteLine("Defaulting the Timeout to 10 Minutes!");


void DisableUpdates()
{
    ServiceController[] scServices;
    scServices = ServiceController.GetServices();
    DateTime start = DateTime.Now;

    foreach (ServiceController scTemp in scServices)
    {
        if (scTemp.ServiceName.Equals("wuauserv"))
        {
            Console.WriteLine("[" + String.Format("{0:g}", start) + "]" +  " The Windows Update service status is currently set to {0}",
                  scTemp.Status.ToString());

            if ((scTemp.Status.Equals(ServiceControllerStatus.Stopped)) ||
                 (scTemp.Status.Equals(ServiceControllerStatus.StopPending)))
            {
                Console.WriteLine("[" + String.Format("{0:g}", start) + "]" + " The Windows Update Service is set to Stopped, Skipping!");
                return;
            }
            else
            {
                // Stop the service if its status is not set to "Stopped".

                Console.WriteLine("[" + String.Format("{0:g}", start) + "]" +  " Stopping the Windows Update service...");
                scTemp.Stop();
            }

            // Refresh and display the current service status.
            scTemp.Refresh();
            Console.WriteLine("[" + String.Format("{0:g}", start) + "]" +  " The Windows Update service status is now set to {0}.",
                               scTemp.Status.ToString());
        }

    }

}


while (true)
{
    DisableUpdates();
    if (userTimeout != "")
    {
        Thread.Sleep(60 * 1000 * int.Parse(userTimeout));
    }
    else
    {
        Thread.Sleep(60 * 1000 * 10);
    }
    Console.WriteLine("");
    Console.WriteLine("---------------------------------------------------------------------");
    Console.WriteLine("");
}