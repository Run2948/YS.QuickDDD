using System;
using Quick.Data.Entities.Enum;
using Quick.Data.Entities.Sys;
using Quick.Domain.Dto;

namespace Quick.Web.Jobs
{
    public interface IHangfireBackJob
    {
        void FlushException(Exception ex);
        void FlushInetAddress(Interview interview);
        void FlushUnhandledAddress();
        void LoginRecord(UserDto userInfo, string ip,LoginType type);
        void InterviewTrace(Guid uid, string url);
    }
}