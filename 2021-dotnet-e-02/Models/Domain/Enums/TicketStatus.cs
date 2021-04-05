namespace _2021_dotnet_e_02.Models.Enums
{
    public enum TicketStatus
    {
        /**
	 * The Created.
	 */
		// Ticket has just been created
        CREATED

        // A Technician has been assigned to the ticket
        ,
        /**
	 * In progress ticket status.
	 */
        IN_PROGRESS

        // The support engineer/technician has added information to the ticket but needs
        // input of the client before it can be completed
        ,
        /**
	 * Waiting on user information ticket status.
	 */
        WAITING_ON_USER_INFORMATION

        // The client has provided the information needed and the technician can proceed
        ,
        /**
	 * User information received ticket status.
	 */
        USER_INFORMATION_RECEIVED

        // The ticket requires a code change before it can be completed
        ,
        /**
	 * In development ticket status.
	 */
        IN_DEVELOPMENT

        // A solution for the ticket has been found
        ,
        /**
	 * Completed ticket status.
	 */
        COMPLETED

        // The customer did not need any further support for this ticket
        // Ticket was removed* by the customer/support engineer
        // *Data is still kept in the database
        ,
        /**
	 * Cancelled ticket status.
	 */
        CANCELLED
    }
}