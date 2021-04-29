using System.Runtime.Serialization;

namespace _2021_dotnet_e_02.Models.Enums
{
    public enum EmployeeRole
    {
        /**
     * Administrator employee role.
     */
        [EnumMember(Value = "Administrator")]
        ADMINISTRATOR

        ,
        /**
     * Technician employee role.
     */
        [EnumMember(Value = "Technician")]
        TECHNICIAN

        ,
        /**
     * Support manager employee role.
     */
        [EnumMember(Value = "Support manager")]
        SUPPORT_MANAGER
    }
}