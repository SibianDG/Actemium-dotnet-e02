using System.Runtime.Serialization;

namespace _2021_dotnet_e_02.Models.Enums
{
    public enum KbItemType
    {
        /**
        * Hardware kb item type.
        */
        [EnumMember(Value = "Hardware")]
        HARDWARE

    ,
        /**
         * Software kb item type.
         */
        [EnumMember(Value = "Software")]
        SOFTWARE

    ,
        /**
         * Infrastructure kb item type.
         */
        [EnumMember(Value = "Infrastructure")]
        INFRASTRUCTURE

    ,
        /**
         * Database kb item type.
         */
        [EnumMember(Value = "Database")]
        DATABASE

    ,
        /**
         * Network kb item type.
         */
        [EnumMember(Value = "Network")]
        NETWORK

    ,
        /**
         * Other kb item type.
         */
        [EnumMember(Value = "Other")]
        OTHER

    ,
        /**
         * kb item has been archived (logical delete)
         * visible for support manager, invisible for the others
         */
        [EnumMember(Value = "Archived")]
        ARCHIVED
    }
}
