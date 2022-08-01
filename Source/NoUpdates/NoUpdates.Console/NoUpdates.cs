using System.Management;
using System.ServiceProcess;


Console.WriteLine("---------------------------------------------------------------------");
Console.WriteLine("");
Console.WriteLine("----------------------------NoUpdates--------------------------------");
Console.WriteLine("");
Console.WriteLine("  A tool to regularly check if Win Updates are running and stop them ");
Console.WriteLine("");
Console.WriteLine("---------------------------------------------------------------------");
Console.WriteLine("");

Console.WriteLine("");


void DisableUpdates()
{
    ServiceController[] scServices;
    scServices = ServiceController.GetServices();
    DateTime start = DateTime.Now;

    foreach (ServiceController scTemp in scServices)
    {
        if (scTemp.ServiceName.Equals("wuauserv"))
        {
            using (var m = new ManagementObject(string.Format("Win32_Service.Name=\"{0}\"", scTemp.ServiceName)))
            {
                m.InvokeMethod("ChangeStartMode", new object[] { "Disabled" });
            }

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
    Thread.Sleep(60 * 1000 * 10);
    Console.WriteLine("");
    Console.WriteLine("---------------------------------------------------------------------");
    Console.WriteLine("");
}