using System.Runtime.Serialization;

namespace _2021_dotnet_e_02.Models.Enums
{
    public enum LoginStatus
    {
        /**
	 * The Success.
	 */
		// Correct username and password combination for an ACTIVE userAccount
        [EnumMember(Value = "Success")] 
        SUCCESS

        // Incorrect password for existing username
        // or
        // Correct username and password combination for BLOCKED/INACTIVE userAccount
        ,
        /**
	 * Failed login status.
	 */
        [EnumMember(Value = "Failed")] 
        FAILED

	
    }
}