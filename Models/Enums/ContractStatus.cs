namespace _2021_dotnet_e_02.Models.Enums
{
    public enum ContractStatus
    {
        /**
	 * The In request.
	 */
		// Most recent update by Product Owner (Actemium)
        // All contracts created by the customer have status IN_REQUEST
        IN_REQUEST

        // After recieving "bestelbon" the support manager sets it to CURRENT
        ,
        /**
	 * Current contract status.
	 */
        CURRENT

        // End date of contract has been reached
        ,
        /**
	 * Expired contract status.
	 */
        EXPIRED

        // Cancelled the contract request or customer didn't pay
        ,
        /**
	 * Cancelled contract status.
	 */
        CANCELLED
    }
}