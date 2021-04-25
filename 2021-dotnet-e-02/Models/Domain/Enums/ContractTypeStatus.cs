using System.Runtime.Serialization;

namespace _2021_dotnet_e_02.Models.Enums
{
    public enum ContractTypeStatus
    {
        /**
	 * The Active.
	 */
        [EnumMember(Value = "Active")]
        ACTIVE

        ,
        /**
	 * Inactive contract type status.
	 */
        [EnumMember(Value = "Inactive")]
        INACTIVE
	
    }
}