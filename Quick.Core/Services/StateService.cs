using System;
using System.ServiceProcess;

namespace Quick.Core.Services
{
    public class StateService
    {
        private static string SERVICE_NAME = "aspnet_state";

        public static bool StartService()
        {
            try
            {
                ServiceController service = new ServiceController(SERVICE_NAME);
                if (service.Status == ServiceControllerStatus.Running)
                    return true;
                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running);
                return true;
            }
            catch (Exception)
            {
                throw new Exception("提示-------> asp_state Service服务启动失败!（可能是权限不足,请手动开启stateservice服务）<---------提示");
            }
        }
    }
}