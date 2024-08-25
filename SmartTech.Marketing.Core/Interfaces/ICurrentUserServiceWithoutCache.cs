using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTech.Marketing.Core.Interfaces
{
    public interface ICurrentUserServiceWithoutCache
    {
        bool IsAuthenticated { get; }

        string EmailAddress { get; }
        string FullName { get; }
        decimal SeUserId { get; }
        decimal SeAccountId { get; }
        decimal SeUserAccountId { get; }
        decimal SeCodeUserTypeId { get; }
        decimal EntityMainId { get; }
        decimal UserTableId { get; }
        string StudFacultyCode { get; }
        bool IsEnglish { get; }
        List<string> Permissions { get; }
    }
}
