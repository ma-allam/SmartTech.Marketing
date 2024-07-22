
using SmartTech.Marketing.Core.Auth.User;

namespace SmartTech.Marketing.Core.Interfaces
{
    public interface ICurrentUserService2
    {
        void Load();
        ActiveContext activeContext { get; set; }
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
