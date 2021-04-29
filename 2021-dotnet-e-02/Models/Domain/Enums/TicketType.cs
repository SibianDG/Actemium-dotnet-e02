using System.Runtime.Serialization;

namespace _2021_dotnet_e_02.Models.Enums
{
    public enum TicketType
    {
        /**
     * Hardware ticket type.
     */
        [EnumMember(Value = "Hardware")] 
        HARDWARE

        ,
        /**
     * Software ticket type.
     */
        [EnumMember(Value = "Software")] 
        SOFTWARE

        ,
        /**
     * Infrastructure ticket type.
     */
        [EnumMember(Value = "Infrastructure")] 
        INFRASTRUCTURE

        ,
        /**
     * Database ticket type.
     */
        [EnumMember(Value = "Database")] 
        DATABASE

        ,
        /**
     * Network ticket type.
     */
        [EnumMember(Value = "Network")] 
        NETWORK

        ,
        /**
     * Other ticket type.
     */
        [EnumMember(Value = "Other")] 
        OTHER
    }
}