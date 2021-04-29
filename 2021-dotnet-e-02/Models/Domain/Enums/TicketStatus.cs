using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace _2021_dotnet_e_02.Models.Enums
{
    public enum TicketStatus
    {
        /**
	 * The Created.
	 */
		// Ticket has just been created
        [EnumMember(Value = "Created")] 
        CREATED

        // A Technician has been assigned to the ticket
        ,
        /**
	 * In progress ticket status.
	 */
        [EnumMember(Value = "In progress")]
        IN_PROGRESS

        // The support engineer/technician has added information to the ticket but needs
        // input of the client before it can be completed
        ,
        /**
	 * Waiting on user information ticket status.
	 */
        [EnumMember(Value = "Waiting on user information")]
        WAITING_ON_USER_INFORMATION

        // The client has provided the information needed and the technician can proceed
        ,
        /**
	 * User information received ticket status.
	 */
        [EnumMember(Value = "User information received")]
        USER_INFORMATION_RECEIVED

        // The ticket requires a code change before it can be completed
        ,
        /**
	 * In development ticket status.
	 */
        [EnumMember(Value = "In development")]
        IN_DEVELOPMENT

        // A solution for the ticket has been found
        ,
        /**
	 * Completed ticket status.
	 */
        [EnumMember(Value = "Completed")]
        COMPLETED

        // The customer did not need any further support for this ticket
        // Ticket was removed* by the customer/support engineer
        // *Data is still kept in the database
        ,
        /**
	 * Cancelled ticket status.
	 */
        [EnumMember(Value = "Cancelled")]
        CANCELLED
    }
}