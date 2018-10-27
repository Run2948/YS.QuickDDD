
using System;
using System.ServiceProcess;

namespace Quick.Core.Services
{
    public class ServiceManager
    {
        /// <summary>
        /// 停止服务
        /// </summary>
        public static bool StopService(string serviceName)
        {
            ServiceController service = new ServiceController(serviceName);
            try
            {
                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Stopped);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 开启服务
        /// </summary>
        public static bool StartService(string serviceName)
        {
            ServiceController service = new ServiceController(serviceName);
            try
            {
                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 重启服务
        /// </summary>
        public static bool ReStartService(string serviceName)
        {
            ServiceController service = new ServiceController(serviceName);
            try
            {
                if (service.CanStop)
                {
                    service.Stop();
                    service.WaitForStatus(ServiceControllerStatus.Stopped);
                }
                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 判断服务是否已开启
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public static bool ServiceIsRunning(string serviceName)
        {
            ServiceController service = new ServiceController(serviceName);
            try
            {
                if (service.Status == ServiceControllerStatus.Running)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}