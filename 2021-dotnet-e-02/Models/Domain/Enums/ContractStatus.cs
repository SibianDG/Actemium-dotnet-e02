using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace _2021_dotnet_e_02.Models.Enums
{
    public enum ContractStatus
    {
        /**
	 * The In request.
	 */
		// Most recent update by Product Owner (Actemium)
        // All contracts created by the customer have status IN_REQUEST
        [EnumMember(Value = "In request")]
        IN_REQUEST

        // After recieving "bestelbon" the support manager sets it to CURRENT
        ,
        /**
	 * Current contract status.
	 */
        [EnumMember(Value = "Current")]
        CURRENT

        // End date of contract has been reached
        ,
        /**
	 * Expired contract status.
	 */
        [EnumMember(Value = "Expired")]
        EXPIRED

        // Cancelled the contract request or customer didn't pay
        ,
        /**
	 * Cancelled contract status.
	 */
        [EnumMember(Value = "Cancelled")]
        CANCELLED
    }
}